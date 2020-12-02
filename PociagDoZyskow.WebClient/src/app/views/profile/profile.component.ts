import { Component } from '@angular/core';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { UserService } from '../../services/user-service';

@Component({
  selector: 'app-profile',
  templateUrl: 'profile.component.html',
  styleUrls: ['./profile.scss']
})

export class ProfileComponent {

  public Email: string = "";
  public UserName: string = "";
  public Balance: number;

  constructor(
    private userService: UserService
  ) 
  {
    this.userService.FetchUserData(); 
    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.Email = data.usermail;
          this.UserName = data.username;
          this.Balance = data.balance;
        }
        
      },
      err => {
        // console.log(err);
      }
    );
  }

}
