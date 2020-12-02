
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';

import { GetBetsOpenForm } from '../viewmodels/bet/GetBetsOpenForm'; 
import { BetOpenForm } from '../viewmodels/bet/BetOpenForm'; 
import { CreateBetForm } from '../viewmodels/bet/CreateBetForm'; 
import { PlayBetForm } from '../viewmodels/bet/PlayBetForm'; 
import { CancellBetForm } from '../viewmodels/bet/CancellBetForm'; 
import { CloseBetForm } from '../viewmodels/bet/CloseBetForm'; 
import { GetUserBetsForm } from '../viewmodels/bet/GetUserBetsForm'; 
import { BetPlayedForm } from '../viewmodels/bet/BetPlayedForm'; 

@Injectable({
    providedIn: 'root',
})
export class BetRepository {
    constructor(private requests: RequestsHelper) { }

    
    GetBetsOpen(arg1: GetBetsOpenForm): Observable<any> {
        return new Observable<BetOpenForm[]>((observer) => {
            let request = this.requests.post('/Bet/GetBetsOpen', arg1);

            request.subscribe(
                (data: BetOpenForm[]) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    CreateBet(arg1: CreateBetForm): Observable<any> {
        return new Observable<BetOpenForm>((observer) => {
            let request = this.requests.post('/Bet/CreateBet', arg1);
            
            request.subscribe(
                (data: BetOpenForm) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    PlayBet(arg1: PlayBetForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Bet/PlayBet', arg1);

            request.subscribe(
                (data: string) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    CancellBet(arg1: CancellBetForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Bet/CancellBet', arg1);

            request.subscribe(
                (data: string) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    CloseBet(arg1: CloseBetForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Bet/CloseBet', arg1);

            request.subscribe(
                (data: string) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    GetBetsPlayed(arg1: GetUserBetsForm): Observable<any> {
        return new Observable<BetPlayedForm[]>((observer) => {
            let request = this.requests.post('/Bet/GetBetsPlayed', arg1);

            request.subscribe(
                (data: BetPlayedForm[]) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    GetBetsHistory(arg1: string): Observable<any> {
        return new Observable<BetPlayedForm[]>((observer) => {
            let request = this.requests.get('/Bet/GetBetsHistory?userId=' + arg1);

            request.subscribe(
                (data: BetPlayedForm[]) => {
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