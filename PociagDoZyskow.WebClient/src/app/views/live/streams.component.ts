import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import 'rxjs/add/operator/map'
import { Router } from '@angular/router';

import { StreamService } from '../../services/stream-service';
import { StreamForm } from '../../viewmodels/stream/StreamForm';
import { UserService } from '../../services/user-service';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { RegisterModalService } from '../register-modal/register-modal.service';


@Component({
  selector: 'streams',
  templateUrl: 'streams.component.html',
  styleUrls: ['./streams.scss']
})
export class StreamsComponent implements OnInit {

  public Streams = [];
  public StreamsOnPage = [];
  public PageIndex: number;
  public PageCount: number;
  public StreamsPerPage: number = 7;
  public DisplayCreateForm = false;
  public Categories = ["other", "csgo", "lol"];
  public CurrentCategory =  "";
  public IsLogged: boolean = false;

  public VidUrl: SafeResourceUrl;

  constructor(
    private router: Router,
    private registerModalService: RegisterModalService,
    private streamService: StreamService,
    private userService: UserService) 
  {
    this.streamService.GetStreams().subscribe(
      (data: StreamForm[]) => {
        // this.Streams = data;
        this.Streams = data.sort((a, b) => 
          +new Date(b.createdDate) - +new Date(a.createdDate));
        
        this.PageIndex = 0;
        this.PageCount = Math.ceil(this.Streams.length/this.StreamsPerPage);
        this.StreamsOnPage = this.Streams.filter((i, index) => (index < this.StreamsPerPage));
      }
    );
    
    this.userService.UserData.subscribe(
      (data: UserDataForm) => {
        if (data != null)
        {
          this.IsLogged = true;
        }
        else
        {
          this.IsLogged = false;
        }
        
      },
      err => {
        // console.log(err);
      }
    );

    this.IsLogged = this.userService.UserId != null && this.userService.UserId != undefined;
  }

  ngOnInit() {
    this.userService.FetchUserData(); 
  }

  showCreateForm() {
    this.DisplayCreateForm = !this.DisplayCreateForm;
  }

  public FetchStreams() {
    this.streamService.GetStreams().subscribe(
      (data: StreamForm[]) => {
        this.Streams = data;
      });
  }

  ShowRegisterModal() {
    this.registerModalService.show();
  } 

  public ChangePage(step: number)
  {
    this.PageCount = Math.ceil(this.Streams.length/this.StreamsPerPage);
    
    let tempInd = this.PageIndex + step;
    if (tempInd < 0) { return; }
    if (tempInd + 1 > this.PageCount){ return; }
    this.PageIndex = tempInd;
    
    let bottomIndex = this.PageIndex * this.StreamsPerPage;
    let topIndex = bottomIndex + this.StreamsPerPage;
    
    this.StreamsOnPage = this.Streams.filter((i, index) => index >= bottomIndex && index < topIndex);
  }

  ngAfterViewInit() {

  }

  GoToBetStreams()
  {
    let url = '/live/create';
    this.router.navigate([url]);
  }


}
