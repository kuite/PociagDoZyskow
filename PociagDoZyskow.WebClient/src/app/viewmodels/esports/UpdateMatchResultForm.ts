import { TournamentResultForm } from "./TournamentResultForm";

export class UpdateMatchResultForm {
    adminId: string; 
    tournamentId: string; 
    matchId: string; 
    winnerName: string; 
    resultUrl: string; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.adminId = parsed.adminId; 
        this.tournamentId = parsed.tournamentId; 
        this.matchId = parsed.matchId; 
        this.winnerName = parsed.winnerName; 
        this.resultUrl = parsed.resultUrl; 
    }
}
