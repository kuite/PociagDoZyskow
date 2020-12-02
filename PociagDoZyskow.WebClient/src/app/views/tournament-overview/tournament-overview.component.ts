import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { TournamentForm } from '../../viewmodels/esports/TournamentForm';


@Component({
  selector: 'tournament-overview',
  templateUrl: 'tournament-overview.component.html',
  styleUrls: ['./tournament-overview.scss']
})



export class TournamentOverviewComponent implements OnInit {
  @Input() tournament: TournamentForm;

  public IsAdmin:boolean = false;
  public IsInProgress: boolean = false;
  public IsClosed: boolean = false;
  public PlayersJoinedCount: number;
  public TournamentIconPath: string;
  public TotalPrizePool: number;

  public TimeLeft: number;
  public TimeLeftInDays: number;
  public TimeLeftInHours: number;
  public TimeLeftInMinutes: number;

  constructor(
    private router: Router) 
  { 
  }

  ngOnInit() 
  {
    this.TimeLeft = new Date(this.tournament.startTime).valueOf() - new Date().valueOf();
    this.TimeLeftInDays = Math.floor(this.TimeLeft / (1000 * 3600 * 24));
    this.TimeLeftInHours = Math.floor(this.TimeLeft / (1000 * 3600));
    this.TimeLeftInMinutes = Math.floor(this.TimeLeft / (1000 * 60));

    this.PlayersJoinedCount = this.tournament.participantsInfo.length;
    this.TournamentIconPath = environment.apiUrl + this.tournament.iconPath;

    this.TotalPrizePool = this.tournament.addedMoneyPrize;
    if (this.tournament.isBuyinAddedToPool)
    {
      this.TotalPrizePool += this.tournament.participantsInfo.length * this.tournament.buyIn;
    }
    if (this.tournament.status == "Closed" || this.tournament.status == "Processed")
    {
      this.IsClosed = true;
    }
  }

  public ViewEvent(evnt) 
  {
    let url = '/esports/tournament/' + evnt.id;
    this.router.navigate([url]);
  }

}

