import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { BetOpenForm } from "../../../viewmodels/bet/BetOpenForm";
import { BetPlayedForm } from "../../../viewmodels/bet/BetPlayedForm";

@Injectable({
  providedIn: "root"
})
export class BetViewsService {
  public ActiveBetId: string;
  public ActiveSelection: any;
  public ActiveAmount: number;

  private _hideBetDetailsForm = new Subject();
  public HideBetDetailsForm$ = this._hideBetDetailsForm.asObservable();

  private _removeBetSource = new Subject();
  public RemoveBet$ = this._removeBetSource.asObservable();

  private _showPlayBetSource = new Subject();
  public ShowPlayBetForm$ = this._showPlayBetSource.asObservable();

  private _showAdminBetSource = new Subject();
  public ShowAdminBetForm$ = this._showAdminBetSource.asObservable();

  private _updateBetSource = new Subject();
  public UpdateBet$ = this._updateBetSource.asObservable();

  private _updateMyBetSource = new Subject();
  public UpdateMyBet$ = this._updateMyBetSource.asObservable();

  constructor() {}

  //for closing bet details in other component
  public HideAllBetDetailForms() {
    this._hideBetDetailsForm.next(true);
    this.ActiveBetId = null;
    this.ActiveSelection =  null;
  }

  public ToggleBetDetailsForm()
  {

  }

  public RemoveBetOpen(id: string)
  {
    this._removeBetSource.next(id);
  }

  public UpdateBet(bet: BetOpenForm)
  {
    this._updateBetSource.next(bet);
  }

  public UpdateMyBet(bet: BetPlayedForm)
  {
    this._updateMyBetSource.next(bet);
  }

}
