
export class BtcWithdrawForm {
    userId: string; 
    amountUsd: number; 
    btcAddress: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.amountUsd = parsed.amountUsd; 
        this.btcAddress = parsed.btcAddress; 
        
    }
}