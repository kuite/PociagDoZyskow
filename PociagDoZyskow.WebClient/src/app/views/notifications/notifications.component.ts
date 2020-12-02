import { Component, Input, OnInit } from "@angular/core";
import { AbstractValueAccessor, MakeProvider } from "../common/html-model-accessor";
import { NotificationsService } from "./notifications.service";

@Component({
    selector: "notifications",
    templateUrl: "notifications.component.html",
    styleUrls: ["./notifications.scss"]
})


export class NotificationsComponent {

    public Notification: string = "";
    public NotificationsLog: string[] = [];
    public NotificationType: string = "";

    constructor(private notificationService: NotificationsService) 
    {
        this.notificationService.notifySuccessObservable$.subscribe(
            (data: any) => 
            { 
                this.Notification = data.msg; 
                this.NotificationType = "Success";
                this.NotificationsLog.push(this.Notification);
                this.removeAfterTime(data.duration);
            }
        );

        this.notificationService.notifyErrorObservable$.subscribe(
            (data: any) => 
            { 
                this.Notification = data.msg; 
                this.NotificationType = "Error";
                this.NotificationsLog.push(this.Notification);
                this.removeAfterTime(data.duration);
            }
        );

        this.notificationService.notifyInformObservable$.subscribe(
            (data: any) => 
            { 
                this.Notification = data.msg; 
                this.NotificationType = "Information";
                this.NotificationsLog.push(this.Notification);
                this.removeAfterTime(data.duration);
            }
        );

    }


    ngOnInit() 
    {

    }

    private removeAfterTime(num: number)
    {
        setTimeout(() => 
        {
            if (this.NotificationsLog !== undefined 
                && this.NotificationsLog != null
                && this.NotificationsLog.length > 0)
            {
                this.NotificationsLog.shift();
            }
        }, num);
    }

}
