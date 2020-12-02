import { Component, Input, OnInit } from "@angular/core";
import { BetOpenForm } from "../../../../viewmodels/bet/BetOpenForm";
import { UserDataForm } from "../../../../viewmodels/user/UserDataForm";
import { BetViewsService } from "../bet-views.service";
import { BetService } from "../../../../services/bet-service";
import { NotificationsService } from "../../../notifications/notifications.service";
import { UserService } from "../../../../services/user-service";
import { BetOptionForm } from "../../../../viewmodels/bet/BetOptionForm";


@Component({
  selector: "betopen-overview",
  templateUrl: "betopen-overview.component.html",
  styleUrls: ["./betopen-overview.scss"]
})



export class BetOpenOverviewComponent {
  @Input() bet: BetOpenForm;
  @Input() isbetAuthor: boolean;

  private selectedBetOptionId: string;

  private userId: string = "";
  public SelectedBetText: string = "";
  public ActiveAmount: number;
  public IsActiveAmountValid: boolean = true;
  public AuthorId: string = "";

  public Title: string = "";
  public Comment: string = "";
  public BetOpenId: string = "";

  public Calculation: number = 0;
  public ActiveCourse: number = 1;
  public PossibleWin: number = 1;

  public DisplayBetDetails: boolean = false;
  public DisplayAdminBetForm: boolean = false;
  public DisplayPlayBetForm: boolean = false;
  public DisplayBetActionForm: boolean = false;

  //
  public BetOptions: BetOptionForm[];

  constructor(
    private betViewsService: BetViewsService,
    private betService: BetService,
    private userService: UserService,
    private notificationService: NotificationsService) 
  {
    this.userService.UserData.subscribe((data: UserDataForm) => {
      if (data != null) {
        this.userId = data.id;
      }
      if (data == null)
      {
        this.userId = null;
      }
    });

    // this.betViewsService.HideBetDetailsForm$.subscribe((data: boolean) => {
    //   if (data == true) {
    //     this.DiscardPlayBetForm();
    //   }
    // });

    this.betViewsService.UpdateBet$.subscribe((data: BetOpenForm) => {
      if (data != null && data.id == this.bet.id) {
        this.BetOptions = data.betOptions;
      }
    });
    
  }

  ngOnInit(){

    this.AuthorId = this.bet.authorId;
    this.Title = this.bet.title;
    this.BetOpenId = this.bet.id;
    // console.log('bet-options: ' + this.bet.betOptions);
    this.BetOptions = this.bet.betOptions;
  } 

  public ToggleBetDetailsForm()
  {
    this.DisplayBetDetails = !this.DisplayBetDetails;
    if (this.DisplayBetDetails == false)
    {
      this.selectedBetOptionId = "";
      this.DisplayBetActionForm = false;
      this.ActiveCourse = 1;
    }
  }

  public ToggleBetActionForm(opt: BetOptionForm)
  {

    if (opt.id == this.selectedBetOptionId)
    {
      console.log("chowam");
      this.selectedBetOptionId = "";
      this.DisplayBetActionForm = false;
      this.ActiveCourse = 1;
    }
    else
    {
      console.log("pokazuje");
      this.selectedBetOptionId = opt.id;
      this.SelectedBetText = opt.optionText;
      this.DisplayBetActionForm = true;
      this.ActiveCourse = opt.odd;

      if (this.isbetAuthor)
      {
        this.DisplayAdminBetForm = true;
        this.DisplayPlayBetForm = false;
      }
      else
      {
        this.DisplayPlayBetForm = true;
        this.DisplayAdminBetForm = false;
      }
    }
  }

  public DiscardPlayBetForm()
  {
    this.DisplayBetActionForm = false;
    this.selectedBetOptionId = "";
    this.DisplayBetDetails = false;
  }

  public PlayBet() {
    console.log("PlayBet this.userId = " + this.userId);
    if (this.selectedBetOptionId == "")
    {
      this.notificationService.NotifyInfo("Select option to bet", 3500);
      return;
    }
    if (this.userId === undefined || this.userId == null || this.userId == "")
    {
      this.notificationService.NotifyInfo("Please login to play.", 3500);
      return;
    }
    if (this.ActiveAmount === undefined || this.ActiveAmount === null || this.ActiveAmount === NaN || this.ActiveAmount < 1)
    {
      this.notificationService.NotifyInfo("Bet amount must be greater than 1 USD.", 3500);
      return;
    }

    let userBalance = 0;

    this.userService.UserData.subscribe(
        (data: UserDataForm) => { userBalance = data.balance }
    );

    if (this.ActiveAmount > userBalance)
    {
      this.ActiveAmount = userBalance;
    }
    if (userBalance == 0)
    {
      this.notificationService.NotifyError("You dont have assets to play.", 3500);
      return;
    }

    let playBetForm = {
      streamId: this.bet.streamId,
      betOpenId: this.bet.id,
      userId: this.userId,
      amount: this.ActiveAmount,
      betOptionId: this.selectedBetOptionId
    };

    this.betService.PlayBet(playBetForm).subscribe(
      (response: string) => {
        if (response != null) {
          console.log('response from server: ' + response);
          this.userService.FetchUserData();
          this.notificationService.NotifySuccess("Bet was played for " + this.ActiveAmount + " USD.", 3500);
          this.ActiveAmount = null;
        }
      },
      err => {
        // console.log(err);
      }
    );
  }

  public CloseBet() 
  {
    let closeBetForm = {
      betOpenId: this.bet.id,
      closingUserId: this.userId,
      wonBetOptionId: this.selectedBetOptionId,
      comment: this.Comment,
      createSameBet: false
    }
    this.betService.CloseBet(closeBetForm).subscribe(
      (response: string) => {
        if (response != null) {
          this.betViewsService.RemoveBetOpen(this.bet.id);
        }
      },
      err => {
        // console.log(err);
      }
    );
  }

  public InputAmountUpdated()
  {
    let stake = this.ActiveAmount;
    this.betViewsService.ActiveAmount = this.ActiveAmount;
    this.Calculation = this.ActiveCourse * stake;
  }

}
