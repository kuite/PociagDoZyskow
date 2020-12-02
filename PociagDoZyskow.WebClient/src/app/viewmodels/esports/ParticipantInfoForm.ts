export class ParticipantInfoForm {
    userId: string; 
    username: string; 
    filledRequiredInfo: string; 
    inGameUsername: string; 
    points: number;
    tournamentStanding: number;
    
    constructor(dataSource: string) {
        let parsed = JSON.parse(dataSource);

        this.userId = parsed.userId; 
        this.username = parsed.username; 
        this.filledRequiredInfo = parsed.filledRequiredInfo; 
        this.points = parsed.points;
        this.inGameUsername = parsed.inGameUsername; 
        this.tournamentStanding = parsed.tournamentStanding; 
    }
}