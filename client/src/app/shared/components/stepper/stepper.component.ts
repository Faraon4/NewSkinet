import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{provide: CdkStepper, useExisting: StepperComponent}] //  with this and line "extends" --> we get full functionality of the CdkStepper
})
export class StepperComponent extends CdkStepper implements OnInit{
  // linearModeSelected --> used for purpose that next step cannot be achieved until the prev step is not complited
  @Input() linearModeSelected = true; 
  

  ngOnInit(): void {
    this.linear = this.linearModeSelected;
  }
  
  onClick(index: number) {
    this.selectedIndex = index; // We did not create a selectedIndex  method --> this is because it comes inside the CdkStepper

  }

}
