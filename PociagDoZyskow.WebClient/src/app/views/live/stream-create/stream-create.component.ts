import { Component, Input, OnInit } from "@angular/core";
import { StreamService } from "../../../services/stream-service";
import { UserDataForm } from "../../../viewmodels/user/UserDataForm";
import { NotificationsService } from "../../notifications/notifications.service";
import { Router } from "@angular/router";
import { UserService } from "../../../services/user-service";

@Component({
  templateUrl: "stream-create.component.html",
  styleUrls: ["./stream-create.scss"]
})


export class StreamCreateComponent {
  public Title: string;
  public Description: string;
  public DescriptionCount: number = 0;
  public Category: string = "lol"; // remove it after test
  public Url: string;
  public User: UserDataForm;

  public isUrlValid: boolean;
  public isTitleValid: boolean;
  public isDescriptionValid: boolean;
  public isInputFormValid: boolean;

  constructor(
    private streamService: StreamService,
    private userService: UserService,
    private router: Router,
    private notificationService: NotificationsService) {
      this.userService.UserData.subscribe(
        (data: UserDataForm) => {
          if (data != null)
          {
            this.User = data;
          }
        },
        (err) => {
          // console.log(err);
        });

        this.setValidDefault();
    }

  public CreateStream() {
    this.setValidDefault();
    this.validateInputs();
    this.CorrectYoutubeUrl();
    if(this.isInputFormValid && this.User != null)
    {
      this.streamService.CreateStream(
        this.Category, 
        this.Description,
        this.Url, 
        this.Title, 
        this.User.id,
        this.User.username, true)
        .subscribe((response) => { 
          if (response != null)
          {
            let streamId = response.id;
            this.notificationService.NotifySuccess("stream created successfully.", 3500);
            this.viewStream(streamId);
          } else {
            this.notificationService.NotifyError("stream creation failed.", 3500);
          }
      });
    }
    else if(this.User == null)
    {
      this.notificationService.NotifyInfo("you are not logged in.", 3500);
    }
  }

  viewStream(streamId) {
    let url = '/live/live/' + streamId;
    this.router.navigate([url]);
  }

  public GoToLive() {
    let url = '/live/';
    this.router.navigate([url]);
  }

  public CorrectYoutubeUrl()
  {
    if(this.Url.includes('watch?v='))
    {
      let frazeToReplace = /watch\?v=/gi;
      this.Url = this.Url.replace(frazeToReplace, 'embed/');
    }
  }

  public ValidateDescription(){
    this.DescriptionCount = this.Description.length;
  }

  validateInputs(){
    if (this.Url == undefined || !this.Url.includes('http'))
    {
      this.isUrlValid = false;
      this.isInputFormValid = false;
    }
    if (this.Title == null || this.Title == "" || this.Title == undefined || this.Title.length == 0)
    {
      this.isTitleValid = false;
      this.isInputFormValid = false;
    }
    if (this.Description == null || this.Description == "" || this.Description == undefined || this.Description.length == 0)
    {
      this.isDescriptionValid = false; 
      this.isInputFormValid = false;
    }
    if(this.DescriptionCount > 255)
    {
      this.isDescriptionValid = false; 
      this.isInputFormValid = false;
    }
  }

  setValidDefault(){
    this.isUrlValid = true;
    this.isTitleValid = true;
    this.isDescriptionValid = true;
    this.isInputFormValid = true;
  }
}
