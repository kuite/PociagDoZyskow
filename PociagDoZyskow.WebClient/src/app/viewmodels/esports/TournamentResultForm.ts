
export class TournamentResultForm {
    id: string;
    tournamentId : string; 
    tournamentStanding : number; 
    inGameUsername : string; 
    username : string; 
    wonAmount: number;

    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.Id; 
        this.tournamentId = parsed.tournamentId; 
        this.tournamentStanding = parsed.tournamentStanding; 
        this.inGameUsername = parsed.inGameUsername; 
        this.username = parsed.username; 
        this.wonAmount = parsed.wonAmount; 
    }
}
