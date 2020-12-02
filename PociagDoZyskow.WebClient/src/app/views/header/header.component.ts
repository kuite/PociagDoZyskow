import { Component, OnInit, isDevMode } from '@angular/core';
import { LoginModalService } from '../login-modal/login-modal.service';
import { RegisterModalService } from '../register-modal/register-modal.service';
import { AuthService } from '../../services/auth-service';
import { Subscription } from 'rxjs';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { Router } from '@angular/router';
import { UserService } from '../../services/user-service';
import { environment } from '../../../environments/environment.prod';

@Component({
  selector: 'yp-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public User: UserDataForm;
  public Username: string;
  public Balance: number = 0;
  public Languages: string[] = ["en", "pl"];
  public Language: string;

  public IsLogged = false;
  private IsDropdownExpanded: boolean = false;

  constructor(
    private router: Router,
    private loginModalService: LoginModalService,
    private registerModalService: RegisterModalService,
    private userService: UserService,
    private authService: AuthService)
  {

  }

  ngOnInit() {
    this.setLangFromUrl();
    
    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.IsLogged = true;
          this.User = data;
          this.Username = data.username;
          this.Balance = data.balance;
        }
      },
      (err) => {
        this.IsLogged = false;
      });
  }

  showLoginModal() {
    this.loginModalService.show();
  }

  showRegisterModal() {
    this.registerModalService.show();
  }

  Logout() {
    this.IsLogged = false;
    this.authService.Logout();
    let url = '/home';
    this.router.navigate([url]);
  }

  ChangeArrowDir() {
    this.IsDropdownExpanded = !this.IsDropdownExpanded;
  }

  private setLangFromUrl()
  {
    let url = window.location.href;
    console.log(url);
    let urlRoute = url.replace(this.router.url, "");
    let lang = urlRoute.substr(urlRoute.length - 2, 2); 
    console.log(lang);
    if (lang != null && lang != "00")
    {
      this.Language = lang;
      localStorage.setItem('Language', lang);
    }
    
  }

  public SetLanguage(language: string)
  {
    // this.Language = localStorage.getItem('Language');
    let currentLanguage = "/" + localStorage.getItem('Language') + "/";
    let newLanguage = "/" + language + "/";
    this.Language = language; 
    localStorage.setItem('Language', language);
    let x = window.location.href;
    console.log(x);
    let y = x.replace(currentLanguage, newLanguage);
    console.log(y);
    if(x != null)
    {
      window.location.href = y;
    }
  }

  public GetCurrentRoute() {
    return this.router.url;
  }

  public GoToHome()
  {
    let url = '/home';
    this.router.navigate([url]);
  }

  public GoToEsports()
  {
    let url = '/esports';
    this.router.navigate([url]);
  }

  public GoToCashier()
  {
    let url = '/cashier';
    this.router.navigate([url]);
  }

  public GoToProfile()
  {
    let url = '/profile';
    this.router.navigate([url]);
  }

}
