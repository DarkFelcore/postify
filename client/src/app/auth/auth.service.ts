import { Injectable, inject } from '@angular/core';
import { Observable, ReplaySubject, map, of } from 'rxjs';
import { IUser } from '../shared/types/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.development';
import { LoginRequest } from '../shared/requests/login-request';
import { RegisterRequest } from '../shared/requests/register-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource: ReplaySubject<IUser | null> = new ReplaySubject<IUser | null>(1);
  currentUser$: Observable<IUser | null> = this.currentUserSource.asObservable();

  // Services
  private readonly http: HttpClient = inject(HttpClient);
  private readonly router: Router = inject(Router);

  loadCurrentUser(token: string): Observable<IUser | null> {
    if(token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    return this.http.get<IUser | null>(environment.baseUrl + 'auth').pipe(
      map((user: IUser | null) => {
        user && this.notifyUserInformation(user);
        return user;
      })
    )
  }

  login(request: LoginRequest) : Observable<IUser | null> {
    return this.http.post<IUser | null>(environment.baseUrl + 'auth/login', request).pipe(
      map((user: IUser | null) => {
        user && this.notifyUserInformation(user);
        return user;
      })
    )
  }

  register(request: RegisterRequest) : Observable<IUser | null> {
    return this.http.post<IUser | null>(environment.baseUrl + 'auth/register', request).pipe(
      map((user: IUser | null) => {
        user && this.notifyUserInformation(user);
        return user;
      })
    )
  }
  
  logout(): void {
    this.clearUser();
    this.router.navigateByUrl('/auth');
  }

  clearUser(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }

  private notifyUserInformation(user: IUser): void {
    localStorage.setItem('token', user.token);
    this.currentUserSource.next(user);
  }

}
