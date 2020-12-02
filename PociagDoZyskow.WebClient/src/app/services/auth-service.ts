import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Observable } from "rxjs";

import { RegisterUserForm } from "../viewmodels/account/RegisterUserForm";
import { UserService } from "./user-service";
import { AccountRepository } from "../http-access/account-repository";
import { JwtForm } from "../viewmodels/account/JwtForm";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  public _isLoggedUpdated = new Subject();
  public IsLogged = this._isLoggedUpdated.asObservable();

  constructor(
    private userService: UserService,
    private router: Router,
    private accountRepository: AccountRepository
  ) 
  {
    
  }

  RegisterUser(
    username: string,
    email: string,
    password: string): Observable<string> {
    return new Observable<string>(observer => {
      let regInput = {
        username: username,
        password: password,
        userMail: email
      };
      let regInputJson = JSON.stringify(regInput);
      let regvm = new RegisterUserForm(regInputJson);

      this.accountRepository.RegisterUser(regvm).subscribe(
        (data: any) => {
          if (data != null) 
          {
            observer.next("ok");
          } 
          else 
          {
            observer.next("Email or username already taken.");
          }
        },
        (err: any) => {
          observer.next(err);
        }
      );
    });
  }

  Login(username: string, password: string): Observable<string> {
    return new Observable<string>((observer) => {
        let loginInput = {
            userLogin: username,
            password: password
        };
        this.accountRepository.Login(loginInput).subscribe(
            (jwt: JwtForm) => {
                if (jwt.id != undefined || jwt.id != null) 
                {
                    // this.sessionStorageService.Jwt = jwt;
                    localStorage.setItem("jwt", JSON.stringify(jwt))
                    this.userService.FetchUserData();
                    this._isLoggedUpdated.next(true);
                    observer.next('ok');
                } 
                else 
                {
                    observer.next('Wrong login or password.')
                }
            },
            (err) => {
                observer.next(err)
            }
        );
    })
}

  Logout() {
    localStorage.removeItem("jwt");

    let url = '/home';
    this.router.navigate([url]);

    this._isLoggedUpdated.next(false);

    this.userService._userDataUpdated.next(null);
  }
}
