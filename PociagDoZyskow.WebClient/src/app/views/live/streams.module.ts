import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { StreamsComponent } from './streams.component';
import { StreamOverviewComponent } from './stream-overview/stream-overview.component';
import { StreamCreateComponent } from './stream-create/stream-create.component';
import { StreamsRoutingModule } from './streams-routing.module';
import { StreamWatchComponent } from './stream-watch/stream-watch.component';
import { BetOpenOverviewComponent } from './bet/betopen-overview/betopen-overview.component';
import { BetPlayedOverviewComponent } from './bet/betplayed-overview/betplayed-overview.component';
import { ChatComponent } from '../chat/chat.component';
import { BetOpenNewFormComponent } from './bet/betopen-new/betopen-new.component';
import { CommonComponentsModule } from '../common/common-components.module';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    BsDropdownModule,
    CommonComponentsModule,
    StreamsRoutingModule
  ],
  declarations: [
    StreamsComponent,
    StreamOverviewComponent,
    StreamCreateComponent,
    StreamWatchComponent,
    BetOpenOverviewComponent,
    BetPlayedOverviewComponent,
    BetOpenNewFormComponent,
    ChatComponent,
  ]
})

export class StreamsModule {}
