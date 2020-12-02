import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Observable } from "rxjs";

import { UserService } from "./user-service";
import { PaymentRepository } from "../http-access/payment-repository";
import { BtcDepositForm } from "../viewmodels/payment/BtcDepositForm";
import { UserDataForm } from "../viewmodels/user/UserDataForm";
import { MoneyTransferForm } from "../viewmodels/payment/MoneyTransferForm";

@Injectable({
  providedIn: "root"
})
export class PaymentsService {
  public _currentTransactionUpdated = new Subject();
  public CurrentTransaction = this._currentTransactionUpdated.asObservable();

  private userData: UserDataForm;

  constructor(
    private paymentRepository: PaymentRepository) 
  {

  }

  CreateBtcDeposit(USD: number, bonus: string, email: string, uid: string): Observable<BtcDepositForm> {
    return new Observable<BtcDepositForm>((observer) => {
        
        let requestData = {
          userId: uid,
          email:  email,
          amountUsd: USD,
          bonuscode: bonus
        }
        this.paymentRepository.CreateBtcPayment(requestData).subscribe(
        (payment: BtcDepositForm) => {
          if (payment != null) 
          {
            observer.next(payment);
          } 
          else 
          {
            observer.next(null); //sprawdz jaka zwrotka gdy nie ma streamow online
          }
        },
        (err) => {
            observer.next(err)
        });
    });
  }

  CreateBtcWithdraw(USD: number, btcAddress: string, email: string, uid: string): Observable<string> {
    return new Observable<string>((observer) => {
        
        let requestData = {
          userId: uid,
          amountUsd: USD,
          btcAddress: btcAddress
        }
        this.paymentRepository.CreateBtcWithDraw(requestData).subscribe(
        (resp: string) => {
            if (resp != null) 
            {
              observer.next(resp);
            } 
            else 
            {
              // console.log("oops - please contact admin youplay@mail.com");
              observer.next("oops - please contact admin youplay@mail.com"); //sprawdz jaka zwrotka gdy nie ma streamow online
            }
        },
        (err) => {
            observer.next(err)
        }
        );
    });
  }

  GetUsersTransfers(userId: string): Observable<MoneyTransferForm[]> {
    return new Observable<MoneyTransferForm[]>((observer) => {
      // console.log('GetUsersTransfers ' + userId);
      this.paymentRepository.GetUsersTransfers(userId).subscribe(
        (resp: MoneyTransferForm[]) => {
          if (resp != null) 
          {
            observer.next(resp);
          }
        },
        (err) => {
            observer.next(err)
        }
      );
    });
  }



}


