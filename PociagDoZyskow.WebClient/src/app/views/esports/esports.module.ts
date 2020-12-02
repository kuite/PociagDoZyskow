import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { EsportsComponent } from './esports.component';
import { EsportsRoutingModule } from './esports-routing.module';
import { TournamentDetailsComponent } from './tournament-details/tournament-details.component';
import { CommonComponentsModule } from '../common/common-components.module';
import { TournamentCreateComponent } from './tournament-create/tournament-create.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatchOverviewComponent } from './tournament-details/tournament-match-overview/tournament-match-overview';
import { TournamentBracketsViewComponent } from './tournament-details/tournament-brackets-view/tournament-brackets-view';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    CommonComponentsModule,
    ModalModule,
    EsportsRoutingModule,
    NgbModule
  ],
  declarations: [ 
    EsportsComponent,
    TournamentDetailsComponent,
    TournamentBracketsViewComponent,
    MatchOverviewComponent,
    TournamentCreateComponent
 ]
})
export class EsportsModule { }
