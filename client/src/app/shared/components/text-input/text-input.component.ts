import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
// using controlValueaccessor --wil act as a bridge between us and the DOM
export class TextInputComponent implements ControlValueAccessor{

  @Input() type = 'text';
  @Input() label = ''

  // self -> used for proving that any input that we give is unique
  // NgControl -> It binds a FormControl object to a DOM element
  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this; // takes care of our text input component
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }


  // Created this method to use in the template
  // we will use this method, rather than use the one in the ctor
  get control(): FormControl {
    return this.controlDir.control as FormControl
  }
  

}
