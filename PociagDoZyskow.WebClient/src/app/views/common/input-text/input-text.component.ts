import { Component, Input } from "@angular/core";
import { AbstractValueAccessor, MakeProvider } from "../html-model-accessor";

@Component({
  selector: "input-text-yp",
  templateUrl: "input-text.component.html",
  styleUrls: ["./input-text.scss"],
  providers: [MakeProvider(InputTextComponent)]
})


export class InputTextComponent extends AbstractValueAccessor {
  @Input() label: string;
  @Input() ispassword: boolean;
  
  public IsInputValid = true;
  public Label: string;
  public IsPassword = false;


  ngOnInit(){
    this.Label = this.label;
    this.IsPassword = this.ispassword;
  }

  public ValidateInput(){
    // let isNumber = !Number.isNaN(Number(this.value));
    // let isPositive = Number(this.value) > 0;
    // //let allowedAmount = Number(this.value) > this.RangeBottom && Number(this.value) < this.RangeTop;

    // if(this.value == ""){
    //   this.isInputValid = true;
    //   return;
    // }
    // if(this.numericType=="natural" && Number.isInteger(Number(this.value)) && isPositive) {
    //   this.isInputValid = true;
    // }
    // else if(this.numericType=="positive" && isNumber && isPositive) {
    //   this.isInputValid = true;
    // }
    // else if(this.numericType=="negative" && isNumber && !isPositive) {
    //   this.isInputValid = true;
    // }
    // else if(this.numericType=="double" && isNumber) {
    //   this.isInputValid = true;
    // }
    // else {
    //   //red border on input
    //   this.isInputValid = false;
    // }

  }

}
