import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import { AccountRepository } from "../../http-access/account-repository";

@Component({
  templateUrl: "register-confirm.component.html",
  styleUrls: ["./register-confirm.scss"]
})
export class RegisterConfirmComponent implements OnInit {

  public ConfirmationId: string;
  public IsLogged = false;
  public IsConfirmed = false;

  constructor(
    private route: ActivatedRoute,
    private accountRepository: AccountRepository,
  ) 
  {
    let id: Observable<string> = route.params.map(p => p.id);
    id.subscribe(value => { 
      this.ConfirmationId = value; 
      this.accountRepository.ConfirmUserMail(this.ConfirmationId).subscribe(
        (data: boolean) => {
          if (data == true)
          {
            this.IsConfirmed = true;
          }
        },
        err => {
          this.IsConfirmed = false;
          // console.log(err);
        }
      );
    });

  }

  ngOnInit() {

  }

  ngOnDestroy() {

  }
}
