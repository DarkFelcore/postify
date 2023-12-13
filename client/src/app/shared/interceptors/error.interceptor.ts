import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const toastr: ToastrService = inject(ToastrService);
  //const router: Router = inject(Router);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if(err) {
        if(err?.error?.status === 401 || err?.status === 401) {
          toastr.error(err.error.title);
        }
      }
      return throwError(() => err);
    })
  );
};
