import { Component, Input } from "@angular/core";
import { AbstractValueAccessor, MakeProvider } from "../html-model-accessor";

@Component({
  selector: "input-numeric-yp",
  templateUrl: "input-numeric.component.html",
  styleUrls: ["./input-numeric.scss"],
  providers: [MakeProvider(InputNumericComponent)]
})


export class InputNumericComponent extends AbstractValueAccessor {
  @Input() numericType: string;
  @Input() label: string;
  
  public RangeBottom: Number = 0;
  public RangeTop: Number = 1000;
  public isInputValid = true;

  ngOnInit(){
  }

  public ValidateInput(){
    this.FormatNumber();
    let isNumber = !Number.isNaN(Number(this.value));
    let isPositive = Number(this.value) > 0;
    //let allowedAmount = Number(this.value) > this.RangeBottom && Number(this.value) < this.RangeTop;

    if(this.value == ""){
      this.isInputValid = true;
      return;
    }
    if(this.numericType=="natural" && Number.isInteger(Number(this.value)) && isPositive) {
      this.isInputValid = true;
    }
    else if(this.numericType=="positive" && isNumber && isPositive) {
      this.isInputValid = true;
    }
    else if(this.numericType=="negative" && isNumber && !isPositive) {
      this.isInputValid = true;
    }
    else if(this.numericType=="double" && isNumber) {
      this.isInputValid = true;
    }
    else {
      //red border on input
      this.isInputValid = false;
    }

  }

  public FormatNumber()
  {
    if(this.value.includes(','))
    {
      let frazeToReplace = /,/gi;
      this.value = this.value.replace(frazeToReplace, '.');
    }
  }
}
