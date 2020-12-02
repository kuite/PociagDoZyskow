import { InputTextComponent } from "./input-text/input-text.component";
import { InputNumericComponent } from "./input-numeric/input-numeric.component";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { TournamentOverviewComponent } from "../tournament-overview/tournament-overview.component";

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        InputTextComponent,
        InputNumericComponent,
        TournamentOverviewComponent
    ],
    exports: [
        InputTextComponent,
        InputNumericComponent,
        TournamentOverviewComponent
    ],
  })
  
  export class CommonComponentsModule {}