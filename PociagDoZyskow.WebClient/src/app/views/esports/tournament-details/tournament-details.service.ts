import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class TournamentDetailsService {
  private _refreshMatchesView = new Subject();
  public RefreshMatchesView$ = this._refreshMatchesView.asObservable();


  RefreshMatchesView() {
    this._refreshMatchesView.next(true);
  }

  CancellNewBetOpenForm() {
    this._refreshMatchesView.next(false);
  }
}
