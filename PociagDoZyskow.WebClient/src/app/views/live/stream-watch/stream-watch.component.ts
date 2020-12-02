import { Component, Input, OnInit } from "@angular/core";
import { StreamForm } from "../../../viewmodels/stream/StreamForm";
import { DomSanitizer, SafeResourceUrl } from "@angular/platform-browser";
import { StreamService } from "../../../services/stream-service";
import { BetOpenForm } from "../../../viewmodels/bet/BetOpenForm";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { environment } from "../../../../environments/environment";
import { StreamWatchService } from "./stream-watch.service";
import { BetPlayedForm } from "../../../viewmodels/bet/BetPlayedForm";
import { UserDataForm } from "../../../viewmodels/user/UserDataForm";
import { BetService } from "../../../services/bet-service";
import { BetViewsService } from "../bet/bet-views.service";
import { UserService } from "../../../services/user-service";
import { NotificationsService } from "../../notifications/notifications.service";


@Component({
  templateUrl: "stream-watch.component.html",
  styleUrls: ["./stream-watch.scss"]
})
export class StreamWatchComponent implements OnInit {
  private hubConnection: HubConnection;
  private ActiveStream: StreamForm;
  private userData: UserDataForm;

  public IsAuthor: boolean = false;
  public AuthorCommission: number = 0;
  public Viewers: number = 0;
  public ActiveStreamId: number;
  public AuthorId: string;
  public AuthorUsername: string;
  // authorId: number;
  // isSubscribed: boolean;
  private Category: string;
  public DisplayStream: boolean = true;
  public StreamUrl: SafeResourceUrl;
  public Title = "";
  public Description: string;
  // allowUserBets: boolean;
  public BetsOpen: BetOpenForm[];
  public BetsPlayed: BetPlayedForm[];

  public BetActiveCourse: number;
  public BetActiveStake: number;
  public isOpenBetsSelected: boolean = false;
  public isBetsPlayedSelected: boolean = false;
  public chatExpanded: boolean = false;
  public expandBtnMsg: string = "Show chat";
  

  public DisplayNewBetOpenForm: boolean = false;

  constructor(
    route: ActivatedRoute,
    private router: Router,
    private streamService: StreamService,
    private betService: BetService,
    private sanitizer: DomSanitizer,
    private userService: UserService,
    private streamWatchService: StreamWatchService,
    private betViewsService: BetViewsService,
    private notificationService: NotificationsService
  ) 
  {
    const id: Observable<number> = route.params.map(p => p.id);
    id.subscribe(value => { this.ActiveStreamId = value; });

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

    this.streamService.GetStream(this.ActiveStreamId).subscribe(
      (data: StreamForm) => {
        this.loadDataStream(data);
        this.ActiveStream = data;
        this.IsAuthor = this.userService.UserId == data.authorId;
      },
      err => {
        // console.log(err);
      }
    );

    this.betViewsService.RemoveBet$.subscribe(
      (betId: string) => {
        this.BetsOpen = this.BetsOpen.filter(bp => {
          return bp.id != betId;
        });
      },
      err => {
        // console.log(err);
      }
    );
  }

  ngOnInit() {
    this.SelectOpenBets();
    
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.hubUrl + "/streamHub").build();
    this.hubConnection
      .start()
      // .then()
      .catch(err => console.log('Error while establishing connection to hub :('));

    this.hubConnection.on('UpdateStreamData', 
      (streamId: number, data: StreamForm) => {
        
        if (streamId == this.ActiveStreamId)
        {
          console.log("s commisino: " + data);
          this.AuthorCommission = data.authorCommission;
        }
      });  

    this.hubConnection.on('UpdateBetsOpen', 
      (streamId: number, data: BetOpenForm[]) => {
        if (streamId == this.ActiveStreamId)
        {
          //data: Open Bets pushed from .net service hub
          data.forEach( (item, index) => 
          {
            if (this.BetsOpen != null && this.BetsOpen.some(b => b.id == item.id))
            {
              this.betViewsService.UpdateBet(item);
            } else {
              this.BetsOpen.push(item);
            }
          });
        }
      });  

    // this.hubConnection.on('UpdateMyBets', 
    //   (streamId: number, data: BetPlayedForm[]) => {
    //     if (streamId == this.ActiveStreamId)
    //     {
    //       //todo
    //     }
    //   });  
      
    this.streamWatchService.DisplayNewBetOpenForm$.subscribe(
      (data: boolean) => {
        this.DisplayNewBetOpenForm = data;
      },
      err => {
        // console.log(err);
      }
    );
  }

  ngOnDestroy() {
    this.ActiveStream = null;
    this.hubConnection.stop();
  }

  public ShowNewBetOpenForm()
  {
    this.streamWatchService.ShowNewBetOpenForm();
  }

  public EndStream() 
  {
    this.streamService.EndStream(this.ActiveStreamId, this.userData.id).subscribe((response) => 
    { 
      if (response == "Ok")
      {
        // let streamId = response.id;
        this.notificationService.NotifySuccess("Stream ended, earnings will soon be credited to you.", 3500);
        let url = '/cashier/';
        this.router.navigate([url]);
      } else {
        this.notificationService.NotifyError("Something gone wrong.", 3500);
      }
    });
  }

  public GoToLive()
  {
    let url = '/live/';
    this.router.navigate([url]);
  }

  private fetchBetsOpen(streamId)
  {
    this.betService.GetBetsOpen(streamId).subscribe(
      (data: BetOpenForm[]) => {
        console.log(data);
        this.BetsOpen = data;
      },
      err => {
        // console.log(err);
      }
    );
  }

  private fetchBetsPlayed(streamId)
  {
    if (this.userData == null)
    {
      return;
    }
    this.betService.GetBetsPlayed(streamId, this.userData.id).subscribe(
      (data: BetPlayedForm[]) => {
        this.BetsPlayed = data;
      },
      err => {
        // console.log(err);
      }
    );
  }

  public SelectOpenBets() {
    this.isOpenBetsSelected = true;
    this.isBetsPlayedSelected = false;
    this.fetchBetsOpen(this.ActiveStreamId);
  }

  public SelectMyBets() {
    this.isOpenBetsSelected = false;
    this.isBetsPlayedSelected = true;
    this.fetchBetsPlayed(this.ActiveStreamId);
  }

  public ToggleChat() {
    this.chatExpanded = !this.chatExpanded;
    if (this.chatExpanded){
      this.expandBtnMsg = "Hide chat";
    }
    if (!this.chatExpanded){
      this.expandBtnMsg = "Show chat";
    }
  }

  public ToggleStream()
  {
    this.DisplayStream = !this.DisplayStream;
  }



  loadDataStream(stream: StreamForm) {
    //this.IsAuthor = this.userData.id == stream.authorId;
    // this.Id = data.id;
    this.AuthorUsername = stream.authorUsername;
    this.AuthorId = stream.authorId;
    // this.isSubscribed = stream.isSubscribed;
    this.Category = stream.category;
    // this.hostSite = stream.hostSite;
    this.StreamUrl = this.sanitizer.bypassSecurityTrustResourceUrl(stream.url);
    this.Title = stream.title;
    this.Description = stream.description;
    // this.allowUserBets = stream.allowUserBets;
  }



}
