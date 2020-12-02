
import { Component, Input, AfterViewInit } from '@angular/core';
import { UserService } from '../../services/user-service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss']
})


export class DefaultLayoutComponent implements AfterViewInit {
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement = document.body;

  constructor(private userService: UserService) 
  {
    this.userService.FetchUserData();
  }

  ngAfterViewInit(){
    
  }
}
