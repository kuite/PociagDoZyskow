import { Component, Input, OnInit } from "@angular/core";
import { UserDataForm } from "../../../../viewmodels/user/UserDataForm";
import { UserService } from "../../../../services/user-service";
import { TournamentMatchForm } from "../../../../viewmodels/esports/TournamentMatchForm";
import { ParticipantInfoForm } from "../../../../viewmodels/esports/ParticipantInfoForm";
import { TournamentRepository } from "../../../../http-access/tournament-repository";
import { NotificationsService } from "../../../notifications/notifications.service";
import { TournamentForm } from "../../../../viewmodels/esports/TournamentForm";
import { TournamentDetailsService } from "../tournament-details.service";


@Component({
  selector: "match-overview",
  templateUrl: "tournament-match-overview.html",
  styleUrls: ["./tournament-match-overview.scss"]
})


export class MatchOverviewComponent {
  @Input() match: TournamentMatchForm;
  @Input() isAdmin: boolean;

  public DisplayMatchForm: boolean = false;

  private userId: string = "";

  constructor(
    private notificationService: NotificationsService,
    private tournamentViewService: TournamentDetailsService,
    private tournamentRepo: TournamentRepository,
    private userService: UserService) 
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
    
  }

  ngOnInit(){

  } 

  public ToggleMatchForm()
  {
    this.DisplayMatchForm = !this.DisplayMatchForm;
  }

  public SubmitMatchWinner(player: ParticipantInfoForm)
  {
    let form = 
    {
      adminId: this.userId, 
      tournamentId: this.match.tournamentId,  
      matchId: this.match.id,  
      winnerName: player.inGameUsername,
      resultUrl: "" 
    };

    console.log(form);
    this.tournamentRepo.UpdateMatchResult(form).subscribe(
      (data: string) => {
        if (data == null)
        {
          this.notificationService.NotifySuccess("Match updated.", 6000);
          this.DisplayMatchForm = false;
          this.tournamentRepo.GetTournament(this.match.tournamentId,).subscribe(
            (data: TournamentForm) => {
              if (data != null)
              {
                console.log("this.tournamentViewService.RefreshMatchesView()");
                this.tournamentViewService.RefreshMatchesView();
              }
            },
            err => {
              console.log(err);
            }
          );
          
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


}
