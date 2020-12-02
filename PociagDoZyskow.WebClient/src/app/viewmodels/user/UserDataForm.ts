
export class UserDataForm {
    id: string; 
    usermail: string; 
    username: string; 
    karma: number; 
    avatarUrl: string; 
    balance: number; 
    inPlay: number; 
    total: number; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.usermail = parsed.usermail; 
        this.username = parsed.username; 
        this.karma = parsed.karma; 
        this.avatarUrl = parsed.avatarUrl; 
        this.balance = parsed.balance; 
        this.inPlay = parsed.inPlay; 
        this.total = parsed.total;
    }
}