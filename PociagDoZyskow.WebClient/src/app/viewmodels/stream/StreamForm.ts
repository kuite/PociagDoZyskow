
export class StreamForm {
    id: number; 
    category: string; 
    hostSite: string; 
    url: string; 
    title: string; 
    description: string; 
    authorUsername: string; 
    authorId: string; 
    allowUserBets: boolean; 
    betOpenCount: number; 
    viewers: number; 
    isFeaturedStream: boolean; 
    authorCommission: number;
    createdDate: Date;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.category = parsed.category; 
        this.hostSite = parsed.hostSite; 
        this.url = parsed.url; 
        this.title = parsed.title; 
        this.description = parsed.description; 
        this.authorUsername = parsed.authorUsername; 
        this.authorId = parsed.authorId; 
        this.allowUserBets = parsed.allowUserBets; 
        this.betOpenCount = parsed.betOpenCount; 
        this.viewers = parsed.viewers; 
        this.isFeaturedStream = parsed.isFeaturedStream; 
        this.authorCommission = parsed.authorCommission; 
        this.createdDate = new Date(parseInt(parsed.createdDate.substr(6)));

    }
}