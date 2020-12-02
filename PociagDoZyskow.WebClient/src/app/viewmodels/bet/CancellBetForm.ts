
export class CancellBetForm {
    betOpenId: string; 
    userId: string; 
    comment: string;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.betOpenId = parsed.betOpenId; 
        this.userId = parsed.userId; 
        this.comment = parsed.comment; 
        
    }
}