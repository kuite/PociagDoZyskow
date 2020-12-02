import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EsportsComponent } from './esports.component';
import { TournamentDetailsComponent } from './tournament-details/tournament-details.component';
import { TournamentCreateComponent } from './tournament-create/tournament-create.component';


const routes: Routes = [
  {
    path: '',
    component: EsportsComponent,
    data: {
      title: 'Esports'
    }
  },
  {
    path: 'tournament/:id',
    component: TournamentDetailsComponent,
    data: {
      title: 'Tournament'
    }
  },
  {
    path: 'create',
    component: TournamentCreateComponent,
    data: {
      title: 'Create'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EsportsRoutingModule {}
