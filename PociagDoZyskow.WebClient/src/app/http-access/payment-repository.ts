
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Observable } from "rxjs"
import { RequestsHelper } from '../utils/requests-helper';

import { PaymentForm } from '../viewmodels/payment/PaymentForm'; 
import { BtcDepositForm } from '../viewmodels/payment/BtcDepositForm'; 
import { BtcWithdrawForm } from '../viewmodels/payment/BtcWithdrawForm'; 
import { MoneyTransferForm } from '../viewmodels/payment/MoneyTransferForm';

@Injectable({
    providedIn: 'root',
})
export class PaymentRepository {
    constructor(private requests: RequestsHelper) { }

    
    CreateBtcPayment(arg1: PaymentForm): Observable<any> {
        return new Observable<BtcDepositForm>((observer) => {
            let request = this.requests.post('/Payment/CreateBtcDeposit', arg1);

            request.subscribe(
                (data: BtcDepositForm) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    CreateBtcWithDraw(arg1: BtcWithdrawForm): Observable<any> {
        return new Observable<any>((observer) => {
            let request = this.requests.post('/Payment/CreateBtcWithDraw', arg1);

            request.subscribe(
                (data: any) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 
    UseDepoCode(arg1: string, arg2: string): Observable<any> {
        return new Observable<any>((observer) => {
            let request = this.requests.get('/Payment/UseDepoCode?userid=' + arg1 + '&code=' + arg2);

            request.subscribe(
                (data: any) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    } 

    GetUsersTransfers(arg1: string): Observable<any> {
        return new Observable<MoneyTransferForm[]>((observer) => {
            let request = this.requests.get('/Payment/GetUsersTransfers?userId=' + arg1);

            request.subscribe(
                (data: MoneyTransferForm[]) => {
                    let return_data = data;
                    observer.next(return_data);
                },
                (err) => {
                    observer.next(err)
                }
            );
        });
    }

}