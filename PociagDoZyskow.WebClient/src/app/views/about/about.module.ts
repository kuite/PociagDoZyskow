import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { AboutComponent } from './about.component';
import { AboutRoutingModule } from './about-routing.module';


@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    ModalModule,
    AboutRoutingModule
  ],
  declarations: [ 
    AboutComponent
 ]
})
export class AboutModule { }
