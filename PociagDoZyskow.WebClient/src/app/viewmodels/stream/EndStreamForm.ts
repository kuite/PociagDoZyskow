
export class EndStreamForm {
    streamId: number; 
    userId: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.streamId = parsed.streamId; 
        this.userId = parsed.userId; 
        
    }
}