import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { CommonComponentsModule } from '../common/common-components.module';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    ModalModule,
    ProfileRoutingModule,
    CommonComponentsModule
  ],
  declarations: [ 
    ProfileComponent
 ]
})
export class ProfileModule { }
