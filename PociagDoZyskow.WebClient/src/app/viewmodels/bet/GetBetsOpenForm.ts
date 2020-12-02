
export class GetBetsOpenForm {
    streamId: number; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.streamId = parsed.streamId; 
        
    }
}