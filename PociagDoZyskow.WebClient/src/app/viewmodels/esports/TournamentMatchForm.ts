import { ParticipantInfoForm } from "./ParticipantInfoForm";

export class TournamentMatchForm {
    id: string;
    tournamentId: string; 
    startTime: Date;
    winnerUsername: string; 
    stage: number; 
    winnerId: string; 
    resultScreenPath: string; 
    players: ParticipantInfoForm[]; 

    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.id = parsed.id; 
        this.tournamentId = parsed.tournamentId; 
        this.startTime = parsed.startTime; 
        this.winnerUsername = parsed.winnerUsername; 
        this.stage = parsed.stage; 
        this.winnerId = parsed.winnerId; 
        this.resultScreenPath = parsed.resultScreenPath; 
        parsed.players.forEach(obj => {
            this.players.push(new ParticipantInfoForm(obj));          
        });
    }
}
