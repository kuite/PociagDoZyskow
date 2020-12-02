
export class PaymentForm {
    userId: string; 
    email: string; 
    amountUsd: number; 
    bonuscode: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.email = parsed.email; 
        this.amountUsd = parsed.amountUsd; 
        this.bonuscode = parsed.bonuscode; 
        
    }
}