import { Component, Input, OnInit } from "@angular/core";
import { UserDataForm } from "../../../../viewmodels/user/UserDataForm";
import { UserService } from "../../../../services/user-service";
import { TournamentMatchForm } from "../../../../viewmodels/esports/TournamentMatchForm";
import { TournamentForm } from "../../../../viewmodels/esports/TournamentForm";


@Component({
  selector: "tournament-brackets-view",
  templateUrl: "tournament-brackets-view.html",
  styleUrls: ["./tournament-brackets-view.scss"]
})


export class TournamentBracketsViewComponent {
  @Input() tournament: TournamentForm;
  @Input() isAdmin: boolean;

  public Matches: TournamentMatchForm[];
  public StagesMatches: { [stage: number]: TournamentMatchForm[] } = { };
  public Stages: number[] = [];

  private userId: string = "";

  constructor(
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
    this.Matches = this.tournament.matches;
    this.LoadTournamentData();
  } 


  private LoadTournamentData()
  {
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
    this.Stages = newstages;
    this.StagesMatches = newStagesMatches;
    console.log(this.StagesMatches);
  }



}
