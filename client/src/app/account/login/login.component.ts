import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  // loginForm will be used in the template to the the form to which element it is connected to
loginForm = new FormGroup( {
  email: new FormControl('', Validators.required),
  password: new FormControl('', Validators.required),
})

// this will be used in the template to say when press the submition button
onSubmit() {
  console.log(this.loginForm.value);
}


}
