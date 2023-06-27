import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          if (error.status === 400){ // This is for validation error
            if(error.error.errors) {
              throw error.error;
            } else {
              this.toastr.error(error.error.message, error.status.toString())
            }
            this.toastr.error(error.error.message, error.status.toString())
          }
          if (error.status === 401){ //THis is for authoethication
            this.toastr.error(error.error.message, error.status.toString())
          }
          if(error.status === 404) {
            this.router.navigateByUrl('/not-found');
          };
          if(error.status === 500) {
            const navigationExtras: NavigationExtras = {state: {error: error.error}}; // we are taking the message of the server error
            this.router.navigateByUrl('/server-error', navigationExtras); // adding the variable as a second parameter, we have access to it in the componenet, this aram can be read only from constructor
          }
        }
        return throwError(() => new Error(error.message))
      })
    )
  }
}
