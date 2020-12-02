
export class BetPlayedForm {
    id: string;
    streamId: number; 
    userId: string;
    status: string; 
    betId: string; 
    option: number; 
    title: string; 
    optionSelectedText: string; 
    optionACourse: number; 
    optionBCourse: number; 
    amount: number; 
    date: Date;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id;
        this.streamId = parsed.streamId; 
        this.userId = parsed.userId; 
        this.status = parsed.status; 
        this.betId = parsed.betId; 
        this.option = parsed.option; 
        this.title = parsed.title; 
        this.optionSelectedText = parsed.optionSelectedText; 
        this.optionACourse = parsed.optionACourse; 
        this.optionBCourse = parsed.optionBCourse; 
        this.amount = parsed.amount; 
        // this.date = new Date(parseInt(parsed.createdDate.substr(6)));
        this.date = new Date(parseInt(parsed.createdDate));
        
    }
}