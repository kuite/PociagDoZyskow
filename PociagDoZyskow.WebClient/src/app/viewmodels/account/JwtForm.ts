
export class JwtForm {
    id: string; 
    auth_token: string; 
    expires_in: number; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.auth_token = parsed.auth_token; 
        this.expires_in = parsed.expires_in; 
        
    }
}