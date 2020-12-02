import { BetOptionForm } from "./BetOptionForm";

export class CreateBetForm {
    streamId: number; 
    title: string; 
    authorId: string; 
    betOptions: BetOptionForm[];

    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.streamId = parsed.streamId; 

        this.title = parsed.title; 
        this.authorId = parsed.authorId; 
        parsed.betOptions.forEach(bet => {
            this.betOptions.push(new BetOptionForm(bet))
        });
    }
}