export class PrizeInfoForm {
    id: string;
    eventId: string; 
    tournamentStanding: number; 
    username: string; 
    poolPercentageReward: number; 
    isCustomPrize: boolean;
    isPoolPercentagePrize: boolean;
    customReward: string; 

    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.eventId = parsed.eventId; 
        this.tournamentStanding = parsed.tournamentStanding; 
        this.customReward = parsed.customReward; 
        this.poolPercentageReward = parsed.poolPercentageReward; 
        this.isCustomPrize = parsed.isCustomPrize; 
        this.isPoolPercentagePrize = parsed.isPoolPercentagePrize; 
        this.username = parsed.username; 
    }
}
