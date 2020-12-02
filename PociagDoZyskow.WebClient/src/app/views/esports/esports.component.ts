import { Component, OnInit } from '@angular/core';
import { TournamentRepository } from '../../http-access/tournament-repository';
import { TournamentForm } from '../../viewmodels/esports/TournamentForm';
import { Router } from '@angular/router';


@Component({
  selector: 'esports',
  templateUrl: 'esports.component.html',
  styleUrls: ['./esports.scss']
})

export class EsportsComponent implements OnInit {
  public Events = [];
  public AllEvents = [];
  public Category = "Any";
  public GameCategories: string[] = ["ALL", "Chess", "QuakeLive", "Starcraft2", "Warcraft3", "Heroes3", "CSGO"];
  public Status ="Any";
  public Statuses: string[] = ["ALL", "Open", "InProgress", "Closed"];

  constructor(
    private eventRepository : TournamentRepository,
    private router: Router)
  {
    // this.Events = 
  }

  ngOnInit() 
  {
    this.eventRepository.GetOpenEvents().subscribe(
      (data: TournamentForm[]) => {
        this.Events =  data;
        this.AllEvents = data;
      }
    );

  }

  public FilterWithGame(game: string)
  {
    this.Category = game;
    console.log(this.Category);
  }

  public FilterWitStatus(status: string)
  {
    this.Status = status;
    console.log(this.Category);
  }

  public ApplyFilter()
  {
    this.Events = this.AllEvents.filter(event => event.Status == this.Status);
  }
}

