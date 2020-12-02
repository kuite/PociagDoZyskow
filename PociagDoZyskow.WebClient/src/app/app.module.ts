import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, PathLocationStrategy } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

// Modal Component
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';
// Import containers
import { DefaultLayoutComponent } from './containers';

import { AuthService } from './services/auth-service';
import { UserService } from './services/user-service';

const APP_CONTAINERS = [
  DefaultLayoutComponent
];

import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular'

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'; //for dropdowns 
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RecaptchaModule } from 'ng-recaptcha';


import { RegisterModalComponent } from './views/register-modal/register-modal.component';
import { LoginModalComponent } from './views/login-modal/login-modal.component';
import { HeaderComponent } from './views/header/header.component';
import { RequestsHelper } from './utils/requests-helper';
import { StreamService } from './services/stream-service';
import { PaymentsService } from './services/payments-service';
import { FooterComponent } from './views/footer/footer.component';
import { NotificationsComponent } from './views/notifications/notifications.component';
import { NotifyMessageComponent } from './views/notifications/notify-message.component';
import { CommonComponentsModule } from './views/common/common-components.module';
import { RegisterConfirmComponent } from './views/register-confirm/register-confirm.component';
import { TournamentOverviewComponent } from './views/tournament-overview/tournament-overview.component';


@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule,
    FormsModule,
    BrowserAnimationsModule,
    CommonComponentsModule,
    RecaptchaModule
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    RegisterModalComponent,
    LoginModalComponent,
    HeaderComponent,
    FooterComponent,
    NotificationsComponent,
    NotifyMessageComponent,
    RegisterConfirmComponent
  ],
  providers: [
    RequestsHelper,
    AuthService,
    UserService,
    StreamService,
    PaymentsService,
    {
      provide: LocationStrategy,
      useClass: PathLocationStrategy
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }