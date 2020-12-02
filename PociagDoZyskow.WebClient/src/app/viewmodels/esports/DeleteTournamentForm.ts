export class DeleteTournamentForm {
    tournamentId: string; 
    userId: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.tournamentId = parsed.touramentId; 
        this.userId = parsed.userId; 
    }
}