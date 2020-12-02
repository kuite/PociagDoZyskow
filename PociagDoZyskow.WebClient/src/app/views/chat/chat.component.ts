import { Component, Input, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { UserDataForm } from '../../viewmodels/user/UserDataForm';
import { environment } from '../../../environments/environment';
import { Message } from './message';
import { NotificationsService } from '../notifications/notifications.service';
import { UserService } from '../../services/user-service';


@Component({
  selector: 'chat',
  templateUrl: 'chat.component.html',
  styleUrls: ['./chat.scss']
})



export class ChatComponent implements OnInit {
  @Input() chatId: string;

  private hubConnection: HubConnection;
  private _ChatStreamId: string = "";
  public UserData: UserDataForm;
  public Username = '';
  public IsLogged = false;
  public message: Message;
  public messages: Message[] = [];

  constructor(
    private notificationService: NotificationsService,
    private userService: UserService)
  {
    this.userService.UserData.subscribe((data: UserDataForm) => {
      if (data != null) {
        this.Username = data.username;
        this.UserData = data;
        this.IsLogged = true;
      }
      else {
        this.IsLogged = false;
      }
    });

  }

  ngOnInit() {
    this.messages = [];
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.hubUrl + "/chatHub")
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection to chat :('));

      this.hubConnection.on('sendToAll', (chatId: string, nick: string, receivedMessage: string) => {
        if (chatId == this.chatId)
        {
          let msg = 
          {
            author: nick,
            text: receivedMessage
          };
          this.messages.push(msg);
        }
      });
  }
  
  public sendMessage() {
    if (!this.IsLogged)
    {
      this.notificationService.NotifyInfo("Please login to chat.", 3500);
    }
    else if( this.message != null || this.message != undefined)
    {
      this.hubConnection
      .invoke('sendToAll', this.chatId, this.Username, this.message)
      .then(() => this.message = null)
      .catch(err => console.error(err));
    }
  }


  ngOnDestroy() {
    this.hubConnection.stop();
  }
}

