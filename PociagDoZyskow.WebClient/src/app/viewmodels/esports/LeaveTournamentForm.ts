export class LeaveTournamentForm {
    userId: string; 
    tournamentId: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.tournamentId = parsed.tournamentId; 
    }
}