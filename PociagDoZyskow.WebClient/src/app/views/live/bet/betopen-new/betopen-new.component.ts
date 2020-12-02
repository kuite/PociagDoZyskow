import { Component, Input, OnInit } from "@angular/core";
import { StreamWatchService } from "../../stream-watch/stream-watch.service";
import { StreamService } from "../../../../services/stream-service";
import { StreamForm } from "../../../../viewmodels/stream/StreamForm";
import { UserDataForm } from "../../../../viewmodels/user/UserDataForm";
import { BetService } from "../../../../services/bet-service";
import { NotificationsService } from "../../../notifications/notifications.service";
import { BetOpenForm } from "../../../../viewmodels/bet/BetOpenForm";
import { BetOptionForm } from "../../../../viewmodels/bet/BetOptionForm";
import { UserService } from "../../../../services/user-service";



@Component({
  selector: "betopen-new-form",
  templateUrl: "betopen-new.component.html",
  styleUrls: ["./betopen-new.scss"]
})


export class BetOpenNewFormComponent {

  private userId: string = "";

  public NewOptionText: string = "";
  public NewOptionOdd: number = null;
  public Title: string = "";

  public IsTitleVal: boolean = true;
  public BetOptions: BetOptionForm[] = [];

  private _streamId: number = 0;
  private _displayNewBetOpenForm: boolean = false;
  private isOddsValid: boolean = false;

  constructor(
    private notificationService: NotificationsService,
    private betService: BetService,
    private userService: UserService,
    private streamWatchService: StreamWatchService
  ) {
    this.userService.UserData.subscribe((data: UserDataForm) => {
      if (data != null) {
        this.userId = data.id;
      }
    });

    this.streamWatchService.DisplayNewBetOpenForm$.subscribe(
      (data: boolean) => {
        this._displayNewBetOpenForm = data;
      },
      err => {
        // console.log(err);
      }
    );
  }

  ngOnInit()
  {
    // this.Title = this.bet.title;
  }

  AddOption()
  {
    let isBetOptValid = true;
    this.BetOptions.forEach(opt => {
      if (opt.optionText === this.NewOptionText)
      {
        console.log('bet istnieje');
        isBetOptValid = false;
      }
    });
    
    if (isBetOptValid && this.NewOptionText != "" && !isNaN(this.NewOptionOdd) && Number(this.NewOptionOdd) > 0)
    {
      console.log(this.NewOptionText + ' , ' + this.NewOptionOdd);
      let bet = 
      {
        id: "",
        betOpenId: "",
        optionText: this.NewOptionText,
        odd: this.NewOptionOdd,
        amount: 0, 
        betsCount: 0
      };
      this.BetOptions.push(bet);
      this.RecalculateOdds(this.NewOptionText);

      this.NewOptionText = "";
      this.NewOptionOdd = null;
    }
    else
    {
      //tutaj podswietlasz na czerwono option text input i rzucasz errorem (notification) ze nie mozna dodac identycznego beta
    }
  }


  CreateNewBetOpen() 
  {
    this.SetValidationDefaults();
    let isValidForm = this.ValidateAddBet();

    let openBetForm = {
      streamId: this._streamId,
      authorId: this.userId,
      title: this.Title,
      betOptions: this.BetOptions
    };

    if(isValidForm)
    {
      this.betService.CreateBetOpen(openBetForm).subscribe(
        (response: string) => {
          if (response != null) {
            // console.log(response);
          }
        },
        err => {
          // console.log(err);
        }
      );
      this.notificationService.NotifySuccess("Bet was created", 3500);
      this.CancellForm();
    }
  }

  public ValidateAddBet(): boolean
  {

    return true; // validation
  }

  public SetValidationDefaults()
  {
    this.IsTitleVal = true;
  }
  
  public IncrementBetOptionOdd(optiontext: string)
  {
    console.log('increment ' + optiontext);
    for (let i = 0; i < this.BetOptions.length; i++)
    {
      if (optiontext == this.BetOptions[i].optionText)
      {
        this.BetOptions[i].odd = this.BetOptions[i].odd * 1.05;
      }
    }
    this.RecalculateOdds(optiontext);
  }

  public DecrementBetOptionOdd(optiontext: string)
  {
    console.log('decrement ' + optiontext);
    for (let i = 0; i < this.BetOptions.length; i++)
    {
      if (optiontext == this.BetOptions[i].optionText && this.BetOptions[i].odd > 1.1)
      {
        this.BetOptions[i].odd = this.BetOptions[i].odd * 0.95;
      }
    }
    this.RecalculateOdds(optiontext);
  }

  public RecalculateOdds(focusBet: string)
  {
    console.log('this.BetOptions.length = ' + this.BetOptions.length);
    if (this.BetOptions.length < 2)
    {
      return;
    }

    console.log('nalezy kalkulwoac');
    let chanceSum = 0;
    this.BetOptions.forEach(betopt => {
      chanceSum += 1 / betopt.odd;
    });
    console.log('chanceSum przed korekcja: ' + chanceSum);
    if (chanceSum < 1)
    {
      console.log('za duze oddsy');
      while (chanceSum < 1)
      {
        chanceSum = 0;
        for (let i = 0; i < this.BetOptions.length; i++)
        {
          if (focusBet != this.BetOptions[i].optionText)
          {
            this.BetOptions[i].odd = this.BetOptions[i].odd * 0.9;
          }
          
          chanceSum += 1 / this.BetOptions[i].odd;
        }
      }
    }
    else if (chanceSum > 1.08)
    {
      console.log('za male oddsy');
      while (chanceSum > 1.08)
      {
        chanceSum = 0;
        for (let i = 0; i < this.BetOptions.length; i++)
        {
          if (focusBet != this.BetOptions[i].optionText)
          {
            this.BetOptions[i].odd = this.BetOptions[i].odd * 1.08;
          }
          
          chanceSum += 1 / this.BetOptions[i].odd;
        }
      }
    }
    console.log('ostateczne chanceSum: ' + chanceSum);
  }

  CancellForm() {
    this.streamWatchService.CancellNewBetOpenForm();
  }

}
