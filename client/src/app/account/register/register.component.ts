import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { debounce, debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  errors: string[] | null = null;

    constructor(private fb: FormBuilder, private accountService:AccountService, private router: Router) {}

    complexPassword = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"

    registerForm = this.fb.group({
      displayName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
      password: ['', [Validators.required, Validators.pattern(this.complexPassword)]]
    })

    onSubmit() {
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => this.router.navigateByUrl('/shop'),
        error: error => this.errors = error.errors
      })
    }

    validateEmailNotTaken(): AsyncValidatorFn {
      return (control: AbstractControl) => {
        // we are using it for not calling all the time the API when we introduce new char to the email field
        //take(1) --> takes the last example that we typed take will send it to the URL api
        // switchMap --> send the last example to the URL api backend to perform the necesary check
        return control.valueChanges.pipe(
          debounceTime(1000),
          take(1),
          switchMap(() => {
            return this.accountService.checkEmailExists(control.value).pipe(
              map( result => result ? {emailExists: true} : null),
              finalize(() => control.markAsTouched())
            )
          })
        )
      }
    }

}
