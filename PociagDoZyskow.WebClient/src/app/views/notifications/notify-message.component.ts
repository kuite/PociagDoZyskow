import { Component, Input, OnInit } from "@angular/core";
import { delay } from "rxjs/operators";

@Component({
    selector: "notify-message",
    templateUrl: "notify-message.component.html",
    styleUrls: ["./notify-message.scss"]
})


export class NotifyMessageComponent {
    @Input() message: string;
    @Input() type: string;

    public Message: string;
    public NotificationLabel: string;
    public IsVisible: boolean = true;
    public IsMsgSuccess: boolean;
    public IsMsgInform: boolean;
    public IsMsgError: boolean;

    constructor() 
    {

    }


    ngOnInit() 
    {
        this.Message = this.message;
        this.NotificationLabel = this.type;
        this.GetType();
    }

    private GetType()
    {
        if (this.type === "Success")
        {
            this.IsMsgSuccess = true;
        }
        if (this.type === "Information")
        {
            this.IsMsgInform = true;
        }
        if (this.type === "Error")
        {
            this.IsMsgError = true;
        }
    }

    public CloseMessage() {
        this.IsVisible = false;
    }

}
