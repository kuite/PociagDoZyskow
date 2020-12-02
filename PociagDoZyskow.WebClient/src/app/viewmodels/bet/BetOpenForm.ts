import { BetOptionForm } from "./BetOptionForm";

export class BetOpenForm {
    id: string; 
    streamId: number; 
    betOptions: BetOptionForm[];
    title: string; 
    author: string; 
    authorId: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.streamId = parsed.streamId; 
        parsed.betOptions.forEach(betOption => {
            this.betOptions.push(new BetOptionForm(betOption));          
        });
        this.title = parsed.title; 
        this.author = parsed.author; 
        this.authorId = parsed.authorId; 
        
    }
}