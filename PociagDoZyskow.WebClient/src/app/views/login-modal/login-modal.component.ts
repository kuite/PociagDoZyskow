import { Component, OnInit, AfterViewInit, ElementRef, Input } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { LoginModalService } from './login-modal.service';
import { Subscription }   from 'rxjs';
import { NotificationsService } from '../notifications/notifications.service';

@Component({
  selector: 'login-modal',
  templateUrl: 'login-modal.component.html',
  styleUrls: ['./login-modal.component.scss']
})

export class LoginModalComponent implements OnInit, AfterViewInit {
  public Username: string;
  public Password: string;

  public IsLoging = false;
  public ErrorMessage = "";
  private showModalSub: Subscription;
  private hideModalButton: HTMLElement;
  
  constructor(
    private _rootNode: ElementRef, 
    private authService: AuthService, 
    private loginModalService: LoginModalService,
    private notificationsService: NotificationsService) 
  {
    this.IsLoging = true;
  }

  ngOnInit() {
    
  }

  ngAfterViewInit() {
    let showModalButton = this._rootNode.nativeElement.children[0] as HTMLElement;
    this.hideModalButton = this._rootNode.nativeElement.children[1] as HTMLElement;

    this.showModalSub = this.loginModalService.showLoginModalObservable$.subscribe(() => { showModalButton.click(); });
  }

  login(){
    this.IsLoging = true; //show loading 
    this.ErrorMessage = "";
    this.authService.Login(this.Username, this.Password).subscribe((response) => { 
      if (response == 'ok'){
        //hide login modal
        this.hideModalButton.click();
        this.notificationsService.NotifySuccess("You have been logged", 3500);
      } else {
        //wrong login/password or sth
        this.ErrorMessage = response;
        this.IsLoging = false;
      }
    });
  }

  public GoToNextInput(event)
  {
    let element = event.srcElement.parentElement.nextElementSibling.childNodes[1];

    if(element == null)
        return;
    else
        element.focus();
  }

  onHideModal() {
    this.IsLoging = false;
    this.ErrorMessage = "";
    this.Username = "";
    this.Password = "";
  }

  ngOnDestroy() {
    // prevent memory leak when component destroyed
    this.showModalSub.unsubscribe();
  }
}
