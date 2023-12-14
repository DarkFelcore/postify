import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IProfile } from '../types/profile';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly http : HttpClient = inject(HttpClient);

  getUserProfile(userId: string): Observable<IProfile> {
    return this.http.get<IProfile>(environment.baseUrl + `users/${userId}`);
  }
}
