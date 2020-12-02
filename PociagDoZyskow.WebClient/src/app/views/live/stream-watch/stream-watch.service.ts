import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class StreamWatchService {
  private _showNewBetOpenForm = new Subject();
  public DisplayNewBetOpenForm$ = this._showNewBetOpenForm.asObservable();


  ShowNewBetOpenForm() {
    this._showNewBetOpenForm.next(true);
  }

  CancellNewBetOpenForm() {
    this._showNewBetOpenForm.next(false);
  }
}
