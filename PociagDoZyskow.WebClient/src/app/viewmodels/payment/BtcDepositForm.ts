
export class BtcDepositForm {
    amount: string; 
    address: string; 
    timeout: string; 
    status_url: string; 
    qrcode_url: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.amount = parsed.amount; 
        this.address = parsed.address; 
        this.timeout = parsed.timeout; 
        this.status_url = parsed.status_url; 
        this.qrcode_url = parsed.qrcode_url; 
        
    }
}