import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IProfile } from '../types/profile';
import { environment } from '../../../environments/environment.development';
import { IGetUserFriendShipRequest } from '../types/requests/get-user-friendship-request';
import { IFriendShip, IFriendShipStatus } from '../types/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly http : HttpClient = inject(HttpClient);

  getUserProfile(userId: string): Observable<IProfile> {
    return this.http.get<IProfile>(environment.baseUrl + `users/${userId}`);
  }

  getFriendShipStatus(request: IGetUserFriendShipRequest): Observable<IFriendShipStatus> {
    return this.http.post<IFriendShipStatus>(environment.baseUrl + "users/friendship", request);
  }

  getUserFollowers(userId: string): Observable<IFriendShip[]> {
    return this.http.get<IFriendShip[]>(environment.baseUrl + 'users/' + userId + '/followers');
  }
}
