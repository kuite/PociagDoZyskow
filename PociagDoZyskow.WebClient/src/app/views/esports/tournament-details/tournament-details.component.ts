import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { environment } from "../../../../environments/environment";
import { UserDataForm } from "../../../viewmodels/user/UserDataForm";
import { UserService } from "../../../services/user-service";
import { NotificationsService } from "../../notifications/notifications.service";

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import { TournamentRepository } from "../../../http-access/tournament-repository";
import { TournamentForm } from "../../../viewmodels/esports/TournamentForm";
import { ParticipantInfoForm } from "../../../viewmodels/esports/ParticipantInfoForm";
import { PrizeInfoForm } from "../../../viewmodels/esports/PrizeInfoForm";
import { TournamentResultForm } from "../../../viewmodels/esports/TournamentResultForm";
import { TournamentMatchForm } from "../../../viewmodels/esports/TournamentMatchForm";
import { TournamentDetailsService } from "./tournament-details.service";

@Component({
  templateUrl: "tournament-details.component.html",
  styleUrls: ["./tournament-details.scss"]
})
export class TournamentDetailsComponent implements OnInit {
  public TimeLeft: number;
  public TimeLeftInDays: number;
  public TimeLeftInHours: number;
  public TimeLeftInMinutes: number;
  public IsAdmin: boolean = false;
  public IsSiteAdmin: boolean = false;
  public IsParticipant : boolean = false;
  

  public SensitiveInformations : string = "Link and password will be visible 3 minutes before tournament start.";
  public Title: string;
  public AdminUsername: string;
  public TournamentBannerPath: string;
  public Description: string;
  public Rules: string;
  public Category: string;
  public StartTime: Date;
  public Contact: string;
  public Format: string;
  public Buyin: number;
  public Participants: ParticipantInfoForm[];
  public Prizes: PrizeInfoForm[];
  public Results: TournamentResultForm[];
  public Matches: TournamentMatchForm[];
  public MaxParticipants: number;
  public CountParticipants: number;
  public RequiredInfoFields: { [name: string]: string } = { };
  public RequiredFields: string[];
  public InGameUsername: string;
  public Status: string;
  public Timezone: string;
  public Platform: string;
  public AddedMoneyPrize: number = 0;
  public TotalMoneyPool: number = 0;

  public DisplayCheckinForm: boolean = false;
  public DisplayFillFormNotification: boolean = false;
  public DisplayResultsForm: boolean = false;
  public DisplayResults: boolean = false;
  public DisplayLeaveForm: boolean = false;
  public DisplayParticipantsList: boolean = false;
  public DisplaySensitiveInformation: boolean = false;
  public DisplayFillResultsNotification: boolean = false;
  public DisplayCurrentStandings: boolean = false;

  public TempResults: TournamentResultForm[];
  public StagesMatches: { [stage: number]: TournamentMatchForm[] } = { };
  public Stages: number[] = [];
  public CurrentStandings: { [points: number]: string } = { };
  public IsClosedTournament: boolean = false;
  public IsOpenTournament: boolean = false;
  public IsMoneyTournament: boolean = false;
  public IsInProgress: boolean = false;
  public GeneralViewActive: boolean = true;
  public MatchesViewActive: boolean = false;
  public ParticipantsViewActive: boolean = false;
  public PrizesViewActive: boolean = false;
  public ResultsViewActive: boolean = false;
  public CurrentUserMatch: TournamentMatchForm;

  public EventId: string;
  public Tournament : TournamentForm;
  private hubConnection: HubConnection;
  
  

  private userData: UserDataForm;
  

  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private tournamentViewService: TournamentDetailsService,
    private eventRepository: TournamentRepository,
    private userService: UserService,
    private notificationService: NotificationsService
  ) 
  {
    let id: Observable<string> = route.params.map(p => p.id);
    id.subscribe(value => { 
      this.EventId = value; 
      this.eventRepository.GetTournament(this.EventId).subscribe(
        (data: TournamentForm) => {
          if (data != null)
          {
            this.Tournament = data;
            console.log(this.Tournament);
            this.UpdateView();
          }
        },
        err => {
          // console.log(err);
        }
      );
    });

    this.tournamentViewService.RefreshMatchesView$.subscribe(
      (data: boolean) => {
        if (data)
        {
          this.eventRepository.GetTournament(this.EventId).subscribe(
            (data: TournamentForm) => {
              if (data != null)
              {
                this.Tournament = data;
                this.UpdateView();
                this.ShowMatchesView();
              }
            },
            err => {
              // console.log(err);
            }
          );
        }
        
      },
      err => {
        // console.log(err);
      }
    );

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

    this.hubConnection = new HubConnectionBuilder().withUrl(environment.hubUrl + "/esportsHub").build();
    this.hubConnection
      .start()
      // .then()
      .catch(err => console.log('Error while establishing connection to hub :('));

    this.hubConnection.on('UpdateEventData', 
      (eventId: string, data: TournamentForm) => {
        if (eventId == this.EventId)
        {
          this.Tournament = data;
          let newdatestring = data.startTime.toString().replace("Z", "");
          this.Tournament.startTime = this.convertUTCDateToLocalDate(new Date(newdatestring));

          this.UpdateView();
        }
      });
      
  }

  ngAfterInit()
  {
    // this.UpdateView();
  }

  ngOnDestroy() {
    this.hubConnection.stop();
  }

  public ToggleParticipantsList()
  {
    this.DisplayParticipantsList = !this.DisplayParticipantsList;
  }

  public ToggleResultsForm()
  {
    if (!this.DisplayResultsForm)
    {
      this.TempResults = this.Results;
    }
    this.DisplayResultsForm = !this.DisplayResultsForm;
  }

  public ToggleCheckinForm()
  {
    this.DisplayCheckinForm = !this.DisplayCheckinForm;
  }

  public ToggleLeaveForm()
  {
    this.DisplayLeaveForm = !this.DisplayLeaveForm;
  }

  public LeaveTournament()
  {
    let form = 
    {
      userId: this.userData.id,
      tournamentId: this.EventId,
    };
    
    this.eventRepository.LeaveTournament(form).subscribe(
      (data: string) => {
        if (data == null)
        {
          this.notificationService.NotifySuccess("You left this tournament", 6000);
          this.IsParticipant = false;
          this.DisplayLeaveForm = false;
          this.userService.FetchUserData();
        }
        else
        {
          this.notificationService.NotifyInfo(data, 6000);
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  public DeleteTournament() 
  {
    let form = 
    {
      userId: this.userData.id,
      tournamentId: this.EventId,
    };
    this.eventRepository.DeleteTournament(form).subscribe(
      (data: string) => {
        let url = '/esports/'
        this.router.navigate([url]);
        console.log(data);
      }
    );
    
  }

  public JoinEvent()
  {
    let filledForm: boolean = true;
    console.log("JoinEvent");
    // this.RequiredFields.forEach(element => {
    //   if (this.RequiredInfoFields[element] == "")
    //   {
    //     filledForm = false;
    //   }
    // });
    if (this.InGameUsername == "" ||this.InGameUsername == null)
    {
      filledForm = false;
    }

    this.DisplayFillFormNotification = !filledForm;
    if (this.DisplayFillFormNotification == true)
    {
      return;
    }
    if (this.userData == null)
    {
      this.notificationService.NotifyInfo("Log in to join.", 4000);
    }
    else if (this.userData.balance < this.Buyin)
    {
      
      this.notificationService.NotifyInfo("Too low balance, go to cashier for more YouPlay coins.", 8000);
    }
    else
    {
      let filledRequiredInfo = "";
      if (this.RequiredFields != null && this.RequiredFields.length > 0)
      {
        this.RequiredFields.forEach(element => {
          filledRequiredInfo += this.RequiredInfoFields[element] + ";";
          this.RequiredInfoFields[element] = "";
        });
      }

      let join = 
      {
        userId: this.userData.id,
        tournamentId: this.EventId,
        filledRequiredInfo: filledRequiredInfo,
        inGameUsername: this.InGameUsername
      };
      
      this.eventRepository.JoinEvent(join).subscribe(
        (data: string) => {
          if (data == null)
          {
            this.notificationService.NotifySuccess("Successfully joined.", 6000);
            this.DisplayCheckinForm = false;
            this.IsParticipant = true;
            this.userService.FetchUserData();
          }
          else
          {
            this.notificationService.NotifyInfo(data, 6000);
          }
        },
        err => {
          console.log(err);
        }
      );

    }
  }

  public UpdateResults()
  {
    let disperror = false;
    // this.TempResults.forEach(element => {
    //   if (element.inGameUsername == "" || element.inGameUsername == null)
    //   {
    //     disperror = true;
    //   }
    // });
    // this.DisplayFillResultsNotification = disperror;
    // if (this.DisplayFillResultsNotification)
    // {
    //   return;
    // }

    let form = 
    {
      userId: this.userData.id,
      tournamentId: this.EventId,
      results: this.TempResults
    };
    // console.log(form);

    this.eventRepository.UpdateTournamentResults(form).subscribe(
      (data: string) => {
        if (data == null)
        {
          this.notificationService.NotifySuccess("Tournament results updated.", 6000);
          this.DisplayResultsForm = false;
          // this.tournamentViewService.RefreshView();
          // this.ShowMatchesView();
        }
        else
        {
          this.notificationService.NotifyInfo(data, 6000);
        }
        console.log("UpdateResults");
        console.log(data);
      },
      err => {
        console.log(err);
      }
    );

  }

  private UpdateView()
  {
    this.TournamentBannerPath = environment.apiUrl + this.Tournament.iconPath;
    
    this.Rules = this.Tournament.rules;
    this.Platform = this.Tournament.platform;
    this.Category = this.Tournament.category;
    this.Contact = this.Tournament.contact;
    this.StartTime = this.Tournament.startTime;
    this.Description = this.Tournament.description;
    this.Title = this.Tournament.title;
    this.AdminUsername = this.Tournament.adminUsername;
    this.Format = this.Tournament.format;
    this.Buyin = this.Tournament.buyIn;
    this.Participants = this.Tournament.participantsInfo;
    console.log(this.Participants);
    this.Prizes = this.Tournament.prizesInfo;
    this.Results = this.Tournament.results;
    this.Matches = this.Tournament.matches;
    this.Results.sort;
    this.MaxParticipants = this.Tournament.participantsMaxCount;
    this.CountParticipants = this.Participants.length;
    if (this.Tournament.requiredInfo != null)
    {
      let fields = this.Tournament.requiredInfo.split(';');
      this.RequiredFields = fields.filter(function(el) 
      { 
        if (el != "")
          return el; 
      });
    }

    this.Status = this.Tournament.status;
    this.Timezone = this.Tournament.timezone;
    this.IsMoneyTournament = this.Tournament.isMoneyTournament;
    this.AddedMoneyPrize = this.Tournament.addedMoneyPrize;
    this.TotalMoneyPool = this.Tournament.addedMoneyPrize;
    if (this.Tournament.isBuyinAddedToPool)
    {
      let commision = 0.2;
      this.TotalMoneyPool += this.Participants.length * (this.Buyin * (1 - commision));
    }
    if (this.Tournament.status == "Closed")
    {
      this.IsClosedTournament = true;
    }
    if (this.Tournament.status == "Open")
    {
      this.IsOpenTournament = true;
      this.IsInProgress = false;
    }
    if (this.Tournament.status == "InProgress")
    {
      this.IsOpenTournament = false;
      this.IsInProgress = true;
    }
    if (this.Tournament.status == "Processed")
    {
      this.IsOpenTournament = false;
      this.IsClosedTournament = true;
      this.IsInProgress = false;
      this.DisplayResults = true;
    }
    // let newdatestring = this.Tournament.startTime.toString().replace("Z", "");
    // this.Tournament.startTime = this.convertUTCDateToLocalDate(new Date(newdatestring));
    // this.StartTime = this.Tournament.startTime;
    
    this.TimeLeft = new Date(this.Tournament.startTime).valueOf() - new Date().valueOf();
    this.TimeLeftInDays = Math.floor(this.TimeLeft / (1000 * 3600 * 24));
    this.TimeLeftInHours = Math.floor(this.TimeLeft / (1000 * 3600));
    this.TimeLeftInMinutes = Math.floor(this.TimeLeft / (1000 * 60));
    this.IsAdmin = false;
    
    if (this.userData != null)
    {
      if (this.userData.username == "admin")
      {
        this.IsSiteAdmin = true;
      }
      let adminid = this.Tournament.adminId;
      this.IsAdmin = adminid == this.userData.id;
    }
    let newStagesMatches = { };
    let newstages = [];
    this.Matches.forEach(match => {
      if (!newstages.some(ns => ns == match.stage))
      {
        newstages.push(match.stage);
      }

      if (newStagesMatches[match.stage] == null)
      {
        newStagesMatches[match.stage] = [];
        newStagesMatches[match.stage].push(match);
      }
      else
      {
        newStagesMatches[match.stage].push(match);
      }
      if (newStagesMatches[match.stage][0].players[0] == null)
      {
        newStagesMatches[match.stage] = [];
      }
    });
    newstages = newstages.sort((n1,n2) => n1 - n2);
    this.Stages = newstages.reverse();
    this.StagesMatches = newStagesMatches;
    
    //Displaying
    if (this.userData != null && this.userData != undefined)
    {
      let tempdata = this.userData;
      let matches = this.Matches;
      let usermatches: TournamentMatchForm[];
      let usermatch: TournamentMatchForm;
      let displeaveform = false;
      let dispsensitiveinform = false;
      let roundnum = 1;
      
      this.Participants.forEach(function(el) 
      {
        if (el.userId == tempdata.id)
        {
          usermatches = matches.filter(match => match.players.some(p => p.userId == tempdata.id));
          usermatches.forEach(match => {
            if (match.stage >= roundnum)
            {
              roundnum = match.stage;
              usermatch = match;
            }
          });
          displeaveform = true;
          dispsensitiveinform = true;
        }

      });
      
      this.CurrentUserMatch = usermatch;
      usermatches = null;
      tempdata = null;
      if (this.IsAdmin == true)
      {
        displeaveform = false;
      }
      this.IsParticipant = displeaveform;
      this.DisplaySensitiveInformation = dispsensitiveinform;
    }
  }

  public ShowGeneralView()
  {
    this.ResultsViewActive = false;
    this.PrizesViewActive = false;
    this.MatchesViewActive = false;
    this.ParticipantsViewActive = false;
    this.GeneralViewActive = true;
  }

  public ShowMatchesView()
  {
    this.ResultsViewActive = false;
    this.GeneralViewActive = false;
    this.PrizesViewActive = false;
    this.ParticipantsViewActive = false;
    this.MatchesViewActive = true;
  }

  public ShowParticipantsView()
  {
    this.ResultsViewActive = false;
    this.PrizesViewActive = false;
    this.GeneralViewActive = false;
    this.MatchesViewActive = false;
    this.ParticipantsViewActive = true;
  }

  public ShowPrizesView()
  {
    this.ResultsViewActive = false;
    this.GeneralViewActive = false;
    this.MatchesViewActive = false;
    this.ParticipantsViewActive = false;
    this.PrizesViewActive = true;
  }

  public ShowResultsView()
  {
    this.PrizesViewActive = false;
    this.MatchesViewActive = false;
    this.ParticipantsViewActive = false;
    this.GeneralViewActive = false;
    this.ResultsViewActive = true;
    if (this.Format == "PointsBasedPairing")
    {
      this.DisplayCurrentStandings = true;
    }
  }

  private convertUTCDateToLocalDate(date) 
  {
    var newDate = new Date(date.getTime()+date.getTimezoneOffset()*60*1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;   
  }


}
