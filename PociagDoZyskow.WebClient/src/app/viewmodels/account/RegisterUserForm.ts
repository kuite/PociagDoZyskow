
export class RegisterUserForm {
    username: string; 
    userMail: string; 
    password: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.username = parsed.username; 
        this.userMail = parsed.userMail; 
        this.password = parsed.password; 
        
    }
}