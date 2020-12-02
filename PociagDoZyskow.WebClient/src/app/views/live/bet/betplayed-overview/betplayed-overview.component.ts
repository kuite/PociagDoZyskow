import { Component, Input, OnInit } from "@angular/core";
import { BetOpenForm } from "../../../../viewmodels/bet/BetOpenForm";
import { StreamService } from "../../../../services/stream-service";
import { BetPlayedForm } from "../../../../viewmodels/bet/BetPlayedForm";
import { UserDataForm } from "../../../../viewmodels/user/UserDataForm";
import { BetService } from "../../../../services/bet-service";
import { BetViewsService } from "../bet-views.service";
import { UserService } from "../../../../services/user-service";

@Component({
  selector: "betplayed-overview",
  templateUrl: "betplayed-overview.component.html",
  styleUrls: ["./betplayed-overview.scss"]
})



export class BetPlayedOverviewComponent {
  @Input() bet: BetPlayedForm;

  private userId: string = "";
  public BetOption: number = 0;
  public ActiveAmount: number = 1;
  public Title: string = "";
  public Option: number = 0;
  public OptionText: string = "";
  public CourseTaken: number = 0;
  public Amount: number = 0;
  public Status: string = "";
  public OptionACourse: number = 0;
  public OptionBCourse: number = 0;

  public PossibleWin: number = 1;

  private DisplayPlayBetForm: boolean = false;

  constructor(
    private userService: UserService
  ) 
  {
    this.userService.UserData.subscribe((data: UserDataForm) => {
      if (data != null) {
        this.userId = data.id;
      }
    });

    // this.betViewsService.UpdateBet$.subscribe((data: BetPlayedForm) => 
    // {
    //   if (data != null && data.id == this.bet.id) {
    //     if (this.bet.option == 1)
    //     {
    //       this.CourseTaken = this.bet.optionACourse;
    //     }
    //     if (this.bet.option == 2)
    //     {
    //       this.CourseTaken = this.bet.optionBCourse;
    //     }
    //   }
    // });

  }

  ngOnInit(){
    // this.OptionACourse = this.bet.optionACourse;
    // this.OptionBCourse = this.bet.optionBCourse;
    // if (this.bet.option == 1)
    // {
    //   this.CourseTaken = this.bet.optionACourse;
    // }
    // if (this.bet.option == 2)
    // {
    //   this.CourseTaken = this.bet.optionBCourse;
    // }
    this.Title = this.bet.title;
    this.Status = this.bet.status;
    if (this.bet.status == "Created")
    {
      this.Status = "Waiting for match"
    }
    this.Amount = Math.round(this.bet.amount);
    // this.Option = this.bet.option;
    this.OptionText = this.bet.optionSelectedText;

    //this.OptionText = this.bet.optionText;
  }

  ActivateBetOption(optionNum, course) {
    // this.BetOption = optionNum;
    // this.HintCourse = course;
    // this.PossibleWin = this.ActiveAmount * this.HintCourse;
  }


  TogglePlayBetForm()
  {
    this.DisplayPlayBetForm = !this.DisplayPlayBetForm;
  }

  CancellBet(bet) {

  }

}
