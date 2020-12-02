import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StreamsComponent } from './streams.component';
import { StreamCreateComponent } from './stream-create/stream-create.component';
import { StreamWatchComponent } from './stream-watch/stream-watch.component';


const routes: Routes = [
  {
    path: '',
    component: StreamsComponent,
    data: {
      title: 'betting streams'
    }
  },
  {
    path: 'create',
    component: StreamCreateComponent,
    data: {
      title: 'create stream'
    }
  },
  {
    path: 'live/:id',
    component: StreamWatchComponent,
    data: {
      title: 'Live bet-stream'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StreamsRoutingModule {}
