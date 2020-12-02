export class JoinTournamentForm {
    userId: string; 
    tournamentId: string; 
    filledRequiredInfo: string; 
    inGameUsername: string;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.tournamentId = parsed.tournamentId; 
        this.filledRequiredInfo = parsed.filledRequiredInfo; 
        this.inGameUsername = parsed.inGameUsername; 
    }
}