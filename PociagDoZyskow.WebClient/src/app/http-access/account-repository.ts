
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';

import { RegisterUserForm } from '../viewmodels/account/RegisterUserForm'; 
import { LoginUserForm } from '../viewmodels/account/LoginUserForm'; 
import { JwtForm } from '../viewmodels/account/JwtForm'; 

@Injectable({
    providedIn: 'root',
})
export class AccountRepository {
    constructor(private requests: RequestsHelper) { }

    
    RegisterUser(arg1: RegisterUserForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Account/RegisterUser', arg1);

            request.subscribe(
                (data: string) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    // // console.log(err);
                    observer.next(null);
                }
            );
        });
    } 

    Login(arg1: LoginUserForm): Observable<any> {
        return new Observable<JwtForm>((observer) => {
            let request = this.requests.post('/Account/Login', arg1);

            request.subscribe(
                (data: JwtForm) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    ConfirmUserMail(arg1: string): Observable<any> {
        return new Observable<boolean>((observer) => {
            let request = this.requests.get('/Account/ConfirmMail?userId=' + arg1);

            request.subscribe(
                (data: boolean) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    }

}