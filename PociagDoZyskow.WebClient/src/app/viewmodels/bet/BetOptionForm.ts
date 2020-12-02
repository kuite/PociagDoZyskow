
export class BetOptionForm {
    id: string; 
    betOpenId: string; 
    optionText: string; 
    odd: number; 
    amount: number; 
    betsCount: number; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.betOpenId = parsed.betOpenId; 
        this.id = parsed.id; 
        this.optionText = parsed.optionText; 
        this.amount = parsed.amount; 
        this.odd = parsed.odd; 
        this.betsCount = parsed.betsCount; 
    }
}