import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  // loginForm will be used in the template to the the form to which element it is connected to
loginForm = new FormGroup( {
  email: new FormControl('', [Validators.required,Validators.email]),
  password: new FormControl('', Validators.required),
})
returnUrl: string;

constructor(private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) {
this.returnUrl =  this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop' // add the check because , if we try to access the basket without being login , we have a new url generated, which we do not need
}

// this will be used in the template to say when press the submition button and will get login
// in this case , after succeful login , it will redirect to the shop page
onSubmit() {
  this.accountService.login(this.loginForm.value).subscribe({
    next: user=> this.router.navigateByUrl(this.returnUrl)
  });
}


}
