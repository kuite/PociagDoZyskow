import { ParticipantInfoForm } from "./ParticipantInfoForm";
import { PrizeInfoForm } from "./PrizeInfoForm";

export class CreateTournamentForm {
    title : string;
    format : string; 
    platform: string;
    game : string;
    contact : string;
    adminUsername : string;
    rules : string;
    description : string;
    authorId : string;
    buyIn : number; 
    addedPoolFromAdmin : number;
    isBuyinAddedToPool : boolean;
    participantsMaxCount : number; 
    maxRoundsLimit : number; 
    startTime : Date;
    timeZone: string;

    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.title = parsed.Title; 
        this.format = parsed.Format; 
        this.platform = parsed.Platform;
        this.game = parsed.Category; 
        this.contact = parsed.Contact; 
        this.adminUsername = parsed.AdminUsername; 
        this.rules = parsed.Rules;  
        this.description = parsed.Description; 
        this.authorId = parsed.AuthorId; 
        this.buyIn = parsed.BuyIn; 
        this.addedPoolFromAdmin = parsed.AddedPoolFromAdmin;
        this.isBuyinAddedToPool = parsed.IsBuyinAddedToPool;
        this.participantsMaxCount = parsed.ParticipantsMaxCount; 
        this.maxRoundsLimit = parsed.MaxRoundsLimit; 
        this.startTime = new Date(parseInt(parsed.StartTime.substr(6))); 
        this.timeZone = parsed.timeZone; 
    }
}