import { NgModule } from '@angular/core';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { CommonComponentsModule } from '../common/common-components.module';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    TabsModule,
    HomeRoutingModule,
    CommonModule,
    CommonComponentsModule,
  ],
  declarations: 
  [ 
    HomeComponent,
  ]
})
export class HomeModule { }
