import { Injectable } from '@angular/core';
import { Subject }    from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class RegisterModalService {
  private _showRegModalSource = new Subject();

  public showRegModalObservable$ = this._showRegModalSource.asObservable();

  constructor() { }

  show(){
    this._showRegModalSource.next();
  }
}