import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'yp-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  public SslCertUrl: string;

  constructor(private router: Router) 
  { 
    this.SslCertUrl = environment.apiUrl + "/assets/ssl.png";
  }


  public GoToAbout()
  {
    let url = '/about';
    this.router.navigate([url]);
  }

  public GoToTerms()
  {
    let url = '/terms';
    this.router.navigate([url]);
  }

}
