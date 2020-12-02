import { ParticipantInfoForm } from "./ParticipantInfoForm";
import { PrizeInfoForm } from "./PrizeInfoForm";
import { TournamentResultForm } from "./TournamentResultForm";
import { TournamentMatchForm } from "./TournamentMatchForm";

export class TournamentForm {
    id: string;
    title : string; 
    bannerPath: string;
    iconPath: string;
    platform: string;
    format : string; 
    category : string; 
    contact : string;
    rules : string; 
    requiredInfo : string; 
    description : string; 
    adminId : string; 
    adminUsername : string; 
    type : string; 
    buyIn : number; 
    participantsMaxCount : number; 
    participantsInfo: ParticipantInfoForm[];
    prizesInfo: PrizeInfoForm[];
    results: TournamentResultForm[];
    matches: TournamentMatchForm[];
    startTime : Date;
    timezone: string;
    status: string;
    isMoneyTournament: boolean;
    addedMoneyPrize: number;
    isBuyinAddedToPool: boolean;
    maxRoundsLimit: number;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.Id; 
        this.title = parsed.Title; 
        this.bannerPath = parsed.BannerPath; 
        this.iconPath = parsed.IconPath; 
        this.platform = parsed.Platform;
        this.format = parsed.Format; 
        this.category = parsed.Category; 
        this.contact = parsed.Contact; 
        this.rules = parsed.Rules; 
        this.requiredInfo = parsed.RequiredInfo; 
        this.description = parsed.Description; 
        this.adminId = parsed.AdminId; 
        this.adminUsername = parsed.AdminUsername; 
        this.type = parsed.Type; 
        this.buyIn = parsed.BuyIn; 
        this.participantsMaxCount = parsed.ParticipantsMaxCount; 
        this.startTime = new Date(parseInt(parsed.StartTime.substr(6)));
        parsed.participantsInfo.forEach(participantInf => {
            this.participantsInfo.push(new ParticipantInfoForm(participantInf));          
        });
        parsed.prizesInfo.forEach(obj => {
            this.prizesInfo.push(new PrizeInfoForm(obj));          
        });
        parsed.results.forEach(obj => {
            this.results.push(new TournamentResultForm(obj));          
        });
        parsed.matches.forEach(obj => {
            this.matches.push(new TournamentMatchForm(obj));          
        });
        this.timezone = parsed.timezone; 
        this.status = parsed.Status; 
        this.isMoneyTournament = parsed.IsMoneyTournament; 
        this.addedMoneyPrize = parsed.AddedMoneyPrize; 
        this.isMoneyTournament = parsed.isMoneyTournament; 
        this.maxRoundsLimit = parsed.maxRoundsLimit; 
    }
}