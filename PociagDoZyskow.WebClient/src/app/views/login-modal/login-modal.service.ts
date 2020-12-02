import { Injectable } from '@angular/core';
import { Subject }    from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class LoginModalService {
  private _showLoginModalSource = new Subject();

  public showLoginModalObservable$ = this._showLoginModalSource.asObservable();

  constructor() { }

  show(){
    this._showLoginModalSource.next();
  }
}