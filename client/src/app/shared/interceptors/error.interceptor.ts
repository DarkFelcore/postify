import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  BehaviorSubject,
  Observable,
  catchError,
  filter,
  switchMap,
  switchScan,
  take,
  throwError,
} from 'rxjs';
import { ErrorCode } from '../types/enums/enums';
import { IRefreshTokenRequest } from '../types/requests/requests';
import { AuthService } from '../../auth/auth.service';
import { IUser } from '../types/user';
import { Router } from '@angular/router';

/* export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  // SERVICES
  const toastr: ToastrService = inject(ToastrService);
  const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err) {
        // Refresh token error
        if (err.status === 401 && !req.url.includes('auth/login')) {
          var currentAccessToken: string = localStorage.getItem(
            'token'
          ) as string;
          var currentRefreshToken: string = localStorage.getItem(
            'refreshToken'
          ) as string;

          var request: IRefreshTokenRequest = {
            accessToken: currentAccessToken,
            refreshToken: currentRefreshToken,
          };

          // Call POST refresh-token endpoint, create this in the auth service, resend the request with the new token obtained + set in local storage
          authService
            .refreshToken(request)
            .pipe(
              switchMap((user: IUser | null) => {
                localStorage.setItem('token', user!.token);
                localStorage.setItem('refreshToken', user!.refreshToken);

                req = req.clone({
                  setHeaders: {
                    Authorization: `Bearer ${user?.token}`,
                  },
                });
                return next(req);
              }),
              catchError((err: HttpErrorResponse) => {
                authService.logout();
                return throwError(() => err);
              })
            )
            .subscribe();
        } else if (err?.error?.status === 401) {
          var errorCode: string = err.error.errorCodes[0];
          toastr.error(err.error.title);
        }
        if (err?.error?.status === 404 || err?.status === 404) {
          var errorCode: string = err.error.errorCodes[0];
          if (errorCode === ErrorCode.NoNotifications) {
            toastr.error(err.error.title);
          }
        }
      }
      return throwError(() => err);
    })
  );
}; */

@Injectable({ providedIn: 'root' })
export class ErrorInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private accessTokenSubject: BehaviorSubject<string | null> =
    new BehaviorSubject<string | null>(null);

  constructor(
    private readonly authService: AuthService,
    private readonly toastr: ToastrService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        var errorCode: string = error?.error?.errorCodes[0];

        if (errorCode === ErrorCode.InvalidCredentials) {
          this.toastr.error(error?.error?.title);
          return throwError(() => error);
        }

        if (error?.status === 401) {
          return this.handle401Error(req, next);
        }

        if (error?.error?.status === 404 || error?.status === 404) {
          if (errorCode === ErrorCode.NoNotifications) {
            this.toastr.error(error?.error?.title);
          }
          return throwError(() => error);
        }

        return throwError(() => error);
      })
    );
  }

  private handle401Error(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<any> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.accessTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((user: IUser | null) => {
          this.isRefreshing = false;
          this.accessTokenSubject.next(user!.token);
          return next.handle(this.addToken(req, user!.token));
        })
      );
    } else {
      return this.accessTokenSubject.pipe(
        filter((token: string | null) => token != null),
        take(1),
        switchMap((token: string | null) => {
          return next.handle(this.addToken(req, token as string));
        })
      );
    }
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
}
