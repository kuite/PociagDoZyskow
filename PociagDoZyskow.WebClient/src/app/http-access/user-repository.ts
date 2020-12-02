
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';

import { UserDataForm } from '../viewmodels/user/UserDataForm'; 

@Injectable({
    providedIn: 'root',
})
export class UserRepository {
    constructor(private requests: RequestsHelper) { }

    
    GetUserData(arg1: string): Observable<any> {
        return new Observable<UserDataForm>((observer) => {
            let request = this.requests.get('/User/GetUserData?userEntityId=' + arg1);

            request.subscribe(
                (data: UserDataForm) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    // console.log(err);
                    observer.next(null);
                }
            );
        });
    }

}