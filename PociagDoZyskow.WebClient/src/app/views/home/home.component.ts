import { Component, OnInit } from '@angular/core';
import { LoginModalService } from '../login-modal/login-modal.service';
import { RegisterModalService } from '../register-modal/register-modal.service';
import { AuthService } from '../../services/auth-service';
import { Subscription } from 'rxjs';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { UserService } from '../../services/user-service';
import { Router } from '@angular/router';
import { TournamentForm } from '../../viewmodels/esports/TournamentForm';
import { TournamentRepository } from '../../http-access/tournament-repository';


@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.scss']
})
export class HomeComponent implements OnInit {
  public User: UserDataForm;
  public Username: string;
  public FeaturedTournaments = [];

  public IsLogged = false;

  constructor(
    private registerModalService: RegisterModalService,
    private eventRepository : TournamentRepository,
    private router: Router,
    private userService: UserService) 
  {

  }

  ngOnInit() {
    this.userService.FetchUserData(); 
    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.IsLogged = true;
          this.User = data;
          this.Username = data.username;
        }
        else
        {
          this.IsLogged = false;
        }
        
      },
      err => {
        // console.log(err);
      }
    );
  
    this.IsLogged = this.userService.UserId != null && this.userService.UserId != undefined;

    this.eventRepository.GetOpenEvents().subscribe(
      (data: TournamentForm[]) => {
        let besttournament = data[0];
        data.forEach(turek => {
          let totalprize = turek.addedMoneyPrize;
          if (turek.isBuyinAddedToPool && turek.participantsInfo != null)
          {
            totalprize += turek.buyIn * turek.participantsInfo.length
          }
          let currenthighest = besttournament.addedMoneyPrize;

          if (besttournament.isBuyinAddedToPool && besttournament.participantsInfo != null)
          {
            currenthighest += besttournament.buyIn * besttournament.participantsInfo.length
          }

          if (turek.status == "Open" && totalprize > currenthighest)
          {
            besttournament = turek;
          }
        });
        this.FeaturedTournaments.push(besttournament);
      }
    );
  }

  public ShowRegisterModal() {
    this.registerModalService.show();
  } 

  GoToEsports()
  {
    let url = '/esports';
    this.router.navigate([url]);
  }

  GoToBetStreams()
  {
    let url = '/live';
    this.router.navigate([url]);
  }

  public GoToCreateTournament()
  {
    let url = '/esports/create';
    this.router.navigate([url]);
  }
 
}
