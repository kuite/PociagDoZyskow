import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { RegisterConfirmComponent } from './views/register-confirm/register-confirm.component';



export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'cashier',
        loadChildren: './views/cashier/cashier.module#CashierModule'
      },
      {
        path: 'home',
        loadChildren: './views/home/home.module#HomeModule'
      },
      // {
      //   path: 'live',
      //   loadChildren: './views/live/streams.module#StreamsModule'
      // },
      {
        path: 'confirm/:id',
        component: RegisterConfirmComponent,
        data: {
          title: 'Address confirm'
        }
      },
      {
        path: 'esports',
        loadChildren: './views/esports/esports.module#EsportsModule'
      },
      {
        path: 'profile',
        loadChildren: './views/profile/profile.module#ProfileModule'
      },
      {
        path: 'about',
        loadChildren: './views/about/about.module#AboutModule'
      },
      {
        path: 'terms',
        loadChildren: './views/terms-and-rules/terms.module#TermsModule'
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
