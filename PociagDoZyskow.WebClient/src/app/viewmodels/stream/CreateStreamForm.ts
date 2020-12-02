
export class CreateStreamForm {
    category: string; 
    url: string; 
    title: string; 
    authorId: string; 
    authorName: string; 
    allowUserBets: boolean; 
    isFeaturedStream: boolean; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.category = parsed.category; 
        this.url = parsed.url; 
        this.title = parsed.title; 
        this.authorId = parsed.authorId;
        this.authorName = parsed.authorName;
        this.allowUserBets = parsed.allowUserBets; 
        this.isFeaturedStream = parsed.isFeaturedStream; 
        
    }
}