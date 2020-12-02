
export class MoneyTransferForm {
    userId: string; 
    amountUsd: number; 
    type: string; 
    status: string;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.amountUsd = parsed.amountUsd; 
        this.type = parsed.type; 
        this.status = parsed.status; 
        
    }
}