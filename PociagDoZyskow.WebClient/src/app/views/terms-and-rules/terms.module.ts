import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { TermsComponent } from './terms.component';
import { TermsRoutingModule } from './terms-routing.module';


@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    ModalModule,
    TermsRoutingModule
  ],
  declarations: [ 
    TermsComponent
 ]
})
export class TermsModule { }
