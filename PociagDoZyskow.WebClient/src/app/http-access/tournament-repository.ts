
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';


import { TournamentForm } from '../viewmodels/esports/TournamentForm';
import { JoinTournamentForm } from '../viewmodels/esports/JoinTournamentForm';
import { LeaveTournamentForm } from '../viewmodels/esports/LeaveTournamentForm';
import { UpdateTournamentResultsForm } from '../viewmodels/esports/UpdateTournamentResultsForm';
import { CreateTournamentForm } from '../viewmodels/esports/CreateTournamentForm';
import { UpdateMatchResultForm } from '../viewmodels/esports/UpdateMatchResultForm';
import { DeleteTournamentForm } from '../viewmodels/esports/DeleteTournamentForm';

@Injectable({
    providedIn: 'root',
})
export class TournamentRepository {
    constructor(private requests: RequestsHelper) { }

    
    GetOpenEvents(): Observable<any> {
        return new Observable<TournamentForm[]>((observer) => {
            let request = this.requests.get('/Esports/GetOpenTournaments', );

            request.subscribe(
                (data: TournamentForm[]) => {
                    data.forEach(element => {
                        // console.log("GetOpenEvents()");
                        // console.log(element);
                        let newdatestring = element.startTime.toString().replace("Z", "");
                        element.startTime = this.convertUTCDateToLocalDate(new Date(newdatestring));
                    });
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

    GetTournament(arg1: string): Observable<any> {
        return new Observable<TournamentForm>((observer) => {
            let request = this.requests.get('/Esports/GetTournament?id=' + arg1);

            request.subscribe(
                (data: TournamentForm) => {
                    // console.log("GetEvent()");
                    // console.log(data);
                    let newdatestring = data.startTime.toString().replace("Z", "");
                    // data.startTime = new Date(newdatestring);
                    data.startTime = this.convertUTCDateToLocalDate(new Date(newdatestring));
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

    JoinEvent(arg1: JoinTournamentForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/JoinTournament', arg1);

            request.subscribe(
                (data: string) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    console.log(err);
                    observer.next(err.error.message);
                }
            );
        });
    } 

    LeaveTournament(arg1: LeaveTournamentForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/LeaveTournament', arg1);

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

    UpdateTournamentResults(arg1: UpdateTournamentResultsForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/SubmitTournamentResults', arg1);

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

    UpdateMatchResult(arg1: UpdateMatchResultForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/UpdateMatchResult', arg1);

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

    CreateTournament(arg1: CreateTournamentForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/CreateTournament', arg1);

            request.subscribe(
                (data: string) => {
                    if (data == "Ok") 
                    {
                        observer.next("Ok");
                    } 
                    else 
                    {
                        observer.next(data);
                    }
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    console.log(err);
                    observer.next(err.error.message);
                }
            );
        });
    }

    DeleteTournament(arg1: DeleteTournamentForm): Observable<any> {
        return new Observable<string>((observer) => {
            let request = this.requests.post('/Esports/DeleteTournament', arg1);

            request.subscribe(
                (data: string) => {
                    if (data == "Ok") 
                    {
                        observer.next("Ok");
                    } 
                    else 
                    {
                        observer.next(data);
                    }
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    console.log(err);
                    observer.next(err.error.message);
                }
            );
        });
    }


    private convertUTCDateToLocalDate(date) {
        var newDate = new Date(date.getTime()+date.getTimezoneOffset()*60*1000);
    
        var offset = date.getTimezoneOffset() / 60;
        var hours = date.getHours();
    
        newDate.setHours(hours - offset);
    
        return newDate;   
    }


}