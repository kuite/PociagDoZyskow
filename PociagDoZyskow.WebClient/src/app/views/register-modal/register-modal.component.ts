import { Component, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RegisterModalService } from './register-modal.service';
import { Subscription }   from 'rxjs';
import { NotificationsService } from '../notifications/notifications.service';
import { Router } from '@angular/router';

@Component({
  selector: 'register-modal',
  templateUrl: 'register-modal.component.html',
  styleUrls: ['./register-modal.component.scss']
})

export class RegisterModalComponent implements OnInit, AfterViewInit {
  public Username: string;
  public Password: string;
  public RepeatPassword: string;
  public Email: string;
  public registerUserModal;
  public IsCreatingAccount: boolean;
  public hideModalButton: any;
  public ErrorMessage: string;
  public isUsernameValid: boolean = true;
  public isPasswordValid: boolean = true;
  public isEmailValid: boolean = true;
  public isValidateSuccess: boolean = true;
  public AcceptedTerms: boolean = false;
  public ShowAcceptTermsMsg: boolean = false;
  public IsCaptchaResolved: boolean = true;
  public ShowCaptchaMsg: boolean = false;

  showModalSubscription: Subscription;

  private hideButton;
  private showButton;
  
  constructor(
    private router: Router,
    private _rootNode: ElementRef,
    private notificationsService: NotificationsService,
    private registerModalService: RegisterModalService,
    private authService: AuthService) 
    {

    }

  ngOnInit() {
  }

  ngAfterViewInit() {

    this.showButton = this._rootNode.nativeElement.children[0];
    this.hideButton = this._rootNode.nativeElement.children[1];
    this.registerUserModal = this._rootNode.nativeElement.children[2];

    this.showModalSubscription = this.registerModalService.showRegModalObservable$.subscribe(() => { this.showButton.click(); });
  }


  registerUser() {
    this.IsCreatingAccount = true;
    this.ValidateInputs();
    if(this.isValidateSuccess)
    {
      this.authService.RegisterUser(this.Username, this.Email, this.Password).subscribe((response) => { 
        if (response == 'ok'){
          //hide login modal
          this.hideButton.click();
          this.notificationsService.NotifySuccess("Account has been created. You can login now.", 3500);
        } else {
          //wrong login/password or sth
          this.ErrorMessage = response;
          this.IsCreatingAccount = false;
          this.notificationsService.NotifyError(response, 3500);
        }
     });
    }
  }

  public ValidateInputs() {
    this.isValidateSuccess = true;
    if (this.AcceptedTerms == false)
    {
      this.isValidateSuccess = false;
      this.ShowAcceptTermsMsg = true;
    }
    else if (this.AcceptedTerms == true)
    {
      this.ShowAcceptTermsMsg = false;
    }
    if (this.IsCaptchaResolved == true)
    {
      this.ShowCaptchaMsg = false;
    }
    else if(this.IsCaptchaResolved == false)
    {
      this.isValidateSuccess = false;
      this.ShowCaptchaMsg = true;
    }
    if(this.Username == undefined || this.Username.length < 4 || this.Username.length > 15)
    {
      this.isUsernameValid = false;
      this.isValidateSuccess = false;
    }
    else
    {
      this.isUsernameValid = true;
    }

    if(this.Password == undefined || this.RepeatPassword == undefined || this.Password != this.RepeatPassword || this.RepeatPassword == "")
    {
      this.isPasswordValid = false;
      this.isValidateSuccess = false;
    }
    else
    {
      this.isPasswordValid = true;
    }

    if(this.Email == undefined || !this.Email.includes('@') || this.Email == "")
    {
      this.isEmailValid = false;
      this.isValidateSuccess = false;
    }
    else
    {
      this.isEmailValid = true;
    }
  }

  onHideModal() {
    this.IsCreatingAccount = false;
    this.ErrorMessage = "";
    this.Username = "";
    this.Email = "";
    this.Password = "";
    this.RepeatPassword = "";
    this.isUsernameValid = true;
    this.isPasswordValid = true;
    this.isEmailValid = true;
    this.isValidateSuccess = true;
    this.ShowAcceptTermsMsg = false;
    this.AcceptedTerms = false;
    grecaptcha.reset();
  }

  public Resolved(captchaResponse: string) {
    this.IsCaptchaResolved = true;
  }

  public GotoTerms()
  {
    let url = '/terms';
    this.router.navigate([url]);
  }

  public ShowModal()
  {
    this.showButton.click();
  }

  public CloseModal()
  {
    this.hideButton.click();
  }

  public SelectTermsAgreement()
  {
    this.AcceptedTerms = !this.AcceptedTerms;
  }


  ngOnDestroy() {
    // prevent memory leak when component destroyed
    // console.log('RegisterModalComponent ngOnDestroy');
    this.showModalSubscription.unsubscribe();
  }
}
