
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';

import { StreamForm } from '../viewmodels/stream/StreamForm'; 
import { CreateStreamForm } from '../viewmodels/stream/CreateStreamForm'; 
import { EndStreamForm } from '../viewmodels/stream/EndStreamForm'; 

@Injectable({
    providedIn: 'root',
})
export class StreamRepository {
    constructor(private requests: RequestsHelper) { }

    
    GetStreams(): Observable<any> {
        return new Observable<StreamForm[]>((observer) => {
            let request = this.requests.get('/Stream/GetStreams', );

            request.subscribe(
                (data: StreamForm[]) => {
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

    GetStream(arg1: number): Observable<any> {
        return new Observable<StreamForm>((observer) => {
            let request = this.requests.get('/Stream/GetStream?streamId=' + arg1);

            request.subscribe(
                (data: StreamForm) => {
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

    CreateStream(arg1: CreateStreamForm): Observable<any> {
        return new Observable<StreamForm>((observer) => {
            let request = this.requests.post('/Stream/CreateStream', arg1);

            request.subscribe(
                (data: StreamForm) => {
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

    EndStream(arg1: EndStreamForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Stream/EndStream', arg1);

            request.subscribe(
                (data: any) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    if (err.status == 200)
                    {
                        observer.next("Ok");
                    }
                    //observer.next(null);
                }
            );
        });
    }

}