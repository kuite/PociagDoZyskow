import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import 'rxjs/add/operator/shareReplay';

import { JwtForm } from '../viewmodels/account/JwtForm';
import { UserRepository } from '../http-access/user-repository';
import { UserDataForm } from '../viewmodels/user/UserDataForm';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    public Jwt: JwtForm;
    public UserId: string;
    // public IsLogged = false;

    public _userDataUpdated = new ReplaySubject<UserDataForm>(1);
    public UserData = this._userDataUpdated.asObservable();

    constructor(private userRepository: UserRepository) 
    {
        this.FetchUserData(); 
    }

    FetchUserData() {
        let token = localStorage.getItem('jwt');
        if (token != null) {
            let jwt = new JwtForm(token);
            this.userRepository.GetUserData(jwt.id).subscribe(
                (data: UserDataForm) => {
                    if (data != null) {
                        this.UserId = data.id;
                        this._userDataUpdated.next(data);
                    } else {

                    }
                },
                (err) => {
                    // console.log(err);
                }
            );
        }
    }

}