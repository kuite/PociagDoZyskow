
export class PlayBetForm {
    streamId: number; 
    betOpenId: string; 
    userId: string; 
    betOptionId: string; 
    amount: number; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.streamId = parsed.streamId; 
        this.betOpenId = parsed.betOpenId; 
        this.userId = parsed.userId; 
        this.betOptionId = parsed.betOptionId; 
        this.amount = parsed.amount; 
        
    }
}