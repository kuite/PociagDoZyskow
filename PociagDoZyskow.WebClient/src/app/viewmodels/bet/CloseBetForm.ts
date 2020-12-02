
export class CloseBetForm {
    betOpenId: string; 
    closingUserId: string; 
    wonBetOptionId: string; 
    comment: string; 
    createSameBet: boolean; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.betOpenId = parsed.betOpenId; 
        this.closingUserId = parsed.closingUserId; 
        this.wonBetOptionId = parsed.wonBetOptionId; 
        this.comment = parsed.comment; 
        this.createSameBet = parsed.createSameBet; 
        
    }
}

