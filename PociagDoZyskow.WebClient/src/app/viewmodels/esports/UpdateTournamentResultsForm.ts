import { TournamentResultForm } from "./TournamentResultForm";

export class UpdateTournamentResultsForm {
    userId: string; 
    tournamentId: string; 
    results: TournamentResultForm[]; 
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.tournamentId = parsed.tournamentId; 
        this.results = parsed.results; 
    }
}