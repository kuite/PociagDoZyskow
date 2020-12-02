
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';


@Injectable({
    providedIn: 'root',
})
export class ExternalAuthRepository {
    constructor(private requests: RequestsHelper) { }

    
    FacebookLogin(arg1: string): Observable<any> {
        return new Observable<any>((observer) => {
            let request = this.requests.post('/ExternalAuth/FacebookLogin', arg1);

            request.subscribe(
                (data: any) => {
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