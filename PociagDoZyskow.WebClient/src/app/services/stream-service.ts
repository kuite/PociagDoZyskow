import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Observable } from "rxjs"
import 'rxjs/add/operator/shareReplay';


import { StreamRepository } from '../http-access/stream-repository';
import { StreamForm } from '../viewmodels/stream/StreamForm';
import { BetRepository } from '../http-access/bet-repository';

@Injectable({
    providedIn: 'root',
})
export class StreamService {
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

    CreateStream(
        category: string, 
        description: string,
        url: string, 
        title: string, 
        authorId: string, 
        username: string,
        allowUserBets: boolean): Observable<StreamForm> {
        return new Observable<StreamForm>((observer) => {
            
            let requestData = {
                category: category,
                description: description,
                url: url,
                title: title,
                authorId: authorId,
                authorName: username,
                allowUserBets: allowUserBets,
                isFeaturedStream: false
            };
            this.streamRepository.CreateStream(requestData).subscribe(
                (stream: StreamForm) => {
                    if (stream != null) 
                    {
                        localStorage.setItem("stream_created", JSON.stringify(stream))
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

    EndStream(streamId: number, userId: string): Observable<string> 
    {
        return new Observable<string>((observer) => {
            
            let requestData = {
                streamId: streamId,
                userId: userId
            };
            this.streamRepository.EndStream(requestData).subscribe(
                (response: string) => {
                    if (response == "Ok") 
                    {
                        observer.next("Ok");
                    } 
                    else 
                    {
                        observer.next(null);
                    }

                },
                (err) => 
                {
                    observer.next(err)
                }
            );
        })
    }

    GetStreams(): Observable<StreamForm[]> {
        return new Observable<StreamForm[]>((observer) => {
            // console.log('service.GetStreams')
            this.streamRepository.GetStreams().subscribe(
                (streams: StreamForm[]) => {
                    if (streams != null) 
                    {
                        observer.next(streams);
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

    GetStream(streamId): Observable<StreamForm> {
        return new Observable<StreamForm>((observer) => {
            
            this.streamRepository.GetStream(streamId).subscribe(
                (stream: StreamForm) => {
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

}