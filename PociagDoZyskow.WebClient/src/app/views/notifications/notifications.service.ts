import { Injectable } from '@angular/core';
import { Subject }    from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private _notifySuccessSource = new Subject();
  private _notifyErrorSource = new Subject();
  private _notifyInformSource = new Subject();

  public notifySuccessObservable$ = this._notifySuccessSource.asObservable();
  public notifyErrorObservable$ = this._notifyErrorSource.asObservable();
  public notifyInformObservable$ = this._notifyInformSource.asObservable();
  
  constructor() 
  { 

  }

  public NotifySuccess(msg: string, duration: number){
    let input = {
      msg: msg,
      duration: duration
    };
    this._notifySuccessSource.next(input);
  }

  public NotifyInfo(msg: string, duration: number){
    let input = {
      msg: msg,
      duration: duration
    };
    this._notifyInformSource.next(input);
  }

  public NotifyError(msg: string, duration: number){
    let input = {
      msg: msg,
      duration: duration
    };
    this._notifyErrorSource.next(input);
  }
}