import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegisterModalComponent } from './register-modal.component';

const routes: Routes = [
  {
    path: '',
    component: RegisterModalComponent,
    outlet: 'modal',
    data: {
      title: 'Join Now'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegisterModalRoutingModule {}
