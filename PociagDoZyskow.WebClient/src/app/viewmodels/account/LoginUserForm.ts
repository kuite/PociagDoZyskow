
export class LoginUserForm {
    userLogin: string; 
    password: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userLogin = parsed.userLogin; 
        this.password = parsed.password; 
        
    }
}