import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { IProfile } from '../types/profile';
import { environment } from '../../../environments/environment.development';
import { IAcceptFollowRequest, IFollowUserRequest, IGetUserFriendShipRequest, IRejectFollowRequest } from '../types/requests/requests';
import { IFriendShip, IFriendShipStatus, IUserToUnfollow } from '../types/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly http : HttpClient = inject(HttpClient);

  friendshipStatusChangedEmitter: EventEmitter<void> = new EventEmitter<void>;
  followRequestDesicionEmitter: EventEmitter<void> = new EventEmitter<void>;

  userToUnfollow = signal<IUserToUnfollow>({
    id: '',
    userName: '',
    pictureUrl: ''
  })

  getUserProfile(userId: string): Observable<IProfile> {
    return this.http.get<IProfile>(environment.baseUrl + `users/${userId}`);
  }

  getFriendShipStatus(request: IGetUserFriendShipRequest): Observable<IFriendShipStatus> {
    return this.http.post<IFriendShipStatus>(environment.baseUrl + "users/friendship", request);
  }

  getUserFollowers(userId: string): Observable<IFriendShip[]> {
    return this.http.get<IFriendShip[]>(environment.baseUrl + 'users/' + userId + '/followers');
  }

  getUserFollowings(userId: string): Observable<IFriendShip[]> {
    return this.http.get<IFriendShip[]>(environment.baseUrl + 'users/' + userId + '/followings');
  }

  unfollowUser(userId: string): Observable<boolean> {
    return this.http.delete<boolean>(environment.baseUrl + 'users/unfollow/' + userId);
  }

  followUser(request: IFollowUserRequest): Observable<boolean> {
    return this.http.post<boolean>(environment.baseUrl + 'users/follow', request);
  }

  acceptFollowRequest(request: IAcceptFollowRequest): Observable<boolean> {
    return this.http.post<boolean>(environment.baseUrl + 'users/follow/accepted', request);
  }

  rejectFollowRequest(request: IRejectFollowRequest): Observable<boolean> {
    return this.http.post<boolean>(environment.baseUrl + 'users/follow/rejected', request);
  }

  followRequestDecision(): void {
    this.followRequestDesicionEmitter.emit();
  }

  friendshipChanged(): void {
    this.friendshipStatusChangedEmitter.emit();
  }

  setUserToUnfollow(user: IUserToUnfollow) : void {
    this.userToUnfollow.set(user); 
  }

  getUserToUnfollow(): IUserToUnfollow {
    return this.userToUnfollow();
  }
}
