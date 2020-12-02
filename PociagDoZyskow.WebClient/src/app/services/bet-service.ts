import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Observable } from "rxjs"
import 'rxjs/add/operator/shareReplay';


import { StreamRepository } from '../http-access/stream-repository';
import { BetOpenForm } from '../viewmodels/bet/BetOpenForm';
import { BetPlayedForm } from '../viewmodels/bet/BetPlayedForm';
import { PlayBetForm } from '../viewmodels/bet/PlayBetForm';
import { StreamForm } from '../viewmodels/stream/StreamForm';
import { CreateBetForm } from '../viewmodels/bet/CreateBetForm';
import { BetRepository } from '../http-access/bet-repository';
import { CloseBetForm } from '../viewmodels/bet/CloseBetForm';

@Injectable({
    providedIn: 'root',
})
export class BetService {
    public _streamsUpdated = new Subject();
    public Streams = this._streamsUpdated.asObservable();
    private _Streams: StreamForm;

    public _activeStreamUpdated = new Subject();
    public ActiveStreamDetails = this._streamsUpdated.asObservable();
    private _ActiveStreamDetails: StreamForm;


    constructor(
        private streamRepository: StreamRepository,
        private betRepository: BetRepository) 
    {
        
    }

    GetBetsOpen(streamId): Observable<BetOpenForm[]> {
        return new Observable<BetOpenForm[]>((observer) => {
            
            let requestData = {
                streamId: streamId
            };
            this.betRepository.GetBetsOpen(requestData).subscribe(
                (stream: BetOpenForm[]) => {
                    if (stream != null) 
                    {
                        observer.next(stream);
                    } 
                    else 
                    {
                        observer.next(null); //sprawdz jaka zwrotka gdy nie ma streamow online
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }

    GetBetsPlayed(streamId: number, userId: string): Observable<BetPlayedForm[]> {
        return new Observable<BetPlayedForm[]>((observer) => {
            
            let requestData = {
                streamId: streamId,
                userId: userId
            };
            this.betRepository.GetBetsPlayed(requestData).subscribe(
                (bets: BetPlayedForm[]) => {
                    if (bets != null) 
                    {
                        observer.next(bets);
                    } 
                    else 
                    {
                        observer.next(null); //sprawdz jaka zwrotka gdy glebiej
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }

    GetBetsHistory(userId: string): Observable<BetPlayedForm[]> {
        return new Observable<BetPlayedForm[]>((observer) => {
            
            this.betRepository.GetBetsHistory(userId).subscribe(
                (bets: BetPlayedForm[]) => {
                    if (bets != null) 
                    {
                        observer.next(bets);
                    } 
                    else 
                    {
                        observer.next(null); //sprawdz jaka zwrotka gdy glebiej
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }
    
    CloseBet(form: CloseBetForm): Observable<string> {
        return new Observable<string>((observer) => {
            

            this.betRepository.CloseBet(form).subscribe(
                (response: BetPlayedForm) => {
                    if (response != null) 
                    {
                        observer.next("success");
                    } 
                    else 
                    {
                        observer.next("error playing bet"); //sprawdz jaka zwrotka gdy nie ma streamow online
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }

    CreateBetOpen(form: CreateBetForm): Observable<string> {
        return new Observable<string>((observer) => {
            

            this.betRepository.CreateBet(form).subscribe(
                (response: BetOpenForm) => {
                    if (response != null) 
                    {
                        observer.next("success");
                    } 
                    else 
                    {
                        observer.next("error creating bet"); //sprawdz jaka zwrotka
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }
    
    PlayBet(form: PlayBetForm): Observable<string> {
        return new Observable<string>((observer) => {
            

            this.betRepository.PlayBet(form).subscribe(
                (response: BetPlayedForm) => {
                    if (response != null) 
                    {
                        observer.next("success");
                    } 
                    else 
                    {
                        observer.next("error playing bet"); //sprawdz jaka zwrotka gdy nie ma streamow online
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }

}