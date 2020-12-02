import { Component, Input, OnInit } from "@angular/core";
import { UserDataForm } from "../../../viewmodels/user/UserDataForm";
import { UserService } from "../../../services/user-service";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import { TournamentRepository } from "../../../http-access/tournament-repository";
import { TournamentForm } from "../../../viewmodels/esports/TournamentForm";
import { NotificationsService } from "../../notifications/notifications.service";
import { Router } from "@angular/router";

@Component({
  templateUrl: "tournament-create.component.html",
  styleUrls: ["./tournament-create.scss"]
})
export class TournamentCreateComponent implements OnInit {
  public Title: string;
  public EventBannerPath: string;
  public DescriptionCount: number = 0;
  public Description: string = 
  "When tournament starts, system create matches based on tournament format. Then, players have 45 minutes for each rund. " +
  "Winner of a match should report result to tournament owner. Owner will be avaliable at discord https://discord.gg/NUZGY5w";
  public Rules: string = "Common sense is number one rule. Any disputes are resolved by tournament admin and his decisions are final.";
  public PlatformNames: string[] = ["PC", "xbox", "play station"];
  public Platform: string;
  public Category: string;
  public GameCategories: string[] = ["Chess", "QuakeLive", "Starcraft2", "Warcraft3", "Heroes3", "CSGO"];
  public StartDate: Date;
  public Hours: number[] = [];
  public Hour: number;
  public Minutes: number[] = [0, 30];
  public Minute: number;
  public Timezones: string[] = ["UTC -5 New York, Bogota", "-4", "-3"];
  public Timezone: string;
  public Contact: string;
  public Format: string;
  public TournamentFormat: string[] = ["FFA", "SingleElimination", "PointsBasedPairing"];
  public Buyin: number = 6;
  public AddedPoolFromAdmin: number;
  public IsBuyinAddedToPool: boolean;
  public MaxParticipants: number = 50;
  public MaxRounds: number = 6;

  private userData: UserDataForm;
  private IsFilled: boolean = true;

  constructor(
    private eventRepository: TournamentRepository,
    private userService: UserService,
    private notificationService: NotificationsService,
    private router: Router
  ) 
  {
    this.StartDate = new Date();
    this.StartDate.setSeconds(0);

    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.userData = data;
        }
        
      },
      err => {
        // console.log(err);
      }
    );
  }

  ngOnInit() {
    let timezoneOffset = new Date().getTimezoneOffset() / -60;

    if(timezoneOffset >= 0)
    {
      this.Timezone = "UTC +" + String(timezoneOffset);
    }
    else
    {
      this.Timezone = "UTC -" + String(timezoneOffset);
    }

    for(let i=0; i<=23; i++) 
    { 
      this.Hours.push(i);
    }
  }

  ngOnDestroy() {
  }

  public CreateTournament()
  {
    if (!this.IsFilled)
    {
      this.notificationService.NotifyInfo("Please fill required inputs.", 4000);
      return;
    }
    let starttime = new Date(this.StartDate);
    starttime.setHours(this.Hour);
    starttime.setMinutes(this.Minute);

    if (this.userData == null)
    {
      this.notificationService.NotifyInfo("Please login.", 4000);
      return;
    }

    let newTournamentForm = {
      title: this.Title,
      format: this.Format,
      platform: this.Platform,
      game: this.Category,
      contact: this.Contact,
      adminUsername: this.userData.username,
      rules: this.Rules,
      description: this.Description,
      authorId: this.userData.id,
      buyIn: this.Buyin,
      addedPoolFromAdmin: this.AddedPoolFromAdmin,
      isBuyinAddedToPool: this.IsBuyinAddedToPool,
      participantsMaxCount: this.MaxParticipants,
      maxRoundsLimit: this.MaxRounds,
      startTime: starttime,
      timeZone: this.Timezone
    }
    
    this.eventRepository.CreateTournament(newTournamentForm).subscribe(
      (data: TournamentForm[]) => {
        console.log(data);
        this.notificationService.NotifySuccess("Tournament created.", 4000);
        let url = '/esports';
        this.router.navigate([url]);
      }
    );
  }

  public GoToEsport()
  {
    
  }

  public ChangePlatform(platform: string)
  {
    this.Platform = platform;
  }

  public ChangeGame(game: string)
  {
    this.Category = game;
    console.log(this.Category);
  }

  public ChangeFormat(format: string)
  {
    this.Format = format;
  }

  public UpdateDescription()
  {
    this.DescriptionCount = this.Description.length;
  }

  public SetHours(hours: string)
  {
    let newHours: number = Number(hours);
    this.Hour = newHours;
    // if (typeof newHours === "number") 
    // {
    //   this.StartDate.setHours(newHours);
    // }
  }

  public SetMinutes(minutes: string)
  {
    let newMinutes: number = Number(minutes);
    this.Minute = newMinutes;
    // if (typeof newMinutes === "number") 
    // {
    //   new Date(this.StartDate).setMinutes(newMinutes);
    // }
  }

  public SetZone(zone: string)
  {
    this.Timezone = zone;
  }

}
