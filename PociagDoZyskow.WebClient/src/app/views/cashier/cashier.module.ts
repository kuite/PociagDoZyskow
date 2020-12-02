import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';

import { CashierComponent } from './cashier.component';
import { CashierRoutingModule } from './cashier-routing.module';
import { CommonComponentsModule } from '../common/common-components.module';


@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    CommonComponentsModule,
    ModalModule,
    CashierRoutingModule
  ],
  declarations: [CashierComponent]
})
export class CashierModule { }
