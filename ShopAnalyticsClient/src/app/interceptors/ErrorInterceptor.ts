import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    debugger;
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        debugger;
        let errorMessage = 'An unexpected error occurred.';
        debugger;
        if (error.error) {
          if (error.error.errors && Array.isArray(error.error.errors)) {
            // Map errors from the `errors` array
            errorMessage = error.error.errors.map((err: any) => err.message).join(', ');
          } else if (error.error.detail) {
            // Use the `detail` property if `errors` is not present
            errorMessage = error.error.detail;
          }
        }

        // Show the error message in a snackbar
        this.snackBar.open(errorMessage, 'Close', {
          duration: 5000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          panelClass: ['app-notification-error'],
        });

        // Optionally, log the error for debugging
        console.error('ErrorInterceptor:', error);

        // Rethrow the error so the application can handle it further if needed
        return throwError(() => error);
      })
    );
  }
}
