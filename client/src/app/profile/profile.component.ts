import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { UserService } from '../shared/services/user.service';
import { IProfile } from '../shared/types/profile';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ProfileTabListComponent } from './components/profile-tab-list/profile-tab-list.component';
import { IFriendShip, IFriendShipStatus, IUser } from '../shared/types/user';
import { AuthService } from '../auth/auth.service';
import { IGetUserFriendShipRequest } from '../shared/types/requests/requests';
import { FriendshipStatusButtonComponent } from '../shared/components/friendship-status-button/friendship-status-button.component';
import { FriendshipModelComponent } from '../shared/components/friendship-model/friendship-model.component';
import { FriendShipStatusEnum } from '../shared/types/enums/enums';
import { ProfileSettingsModelComponent } from '../shared/components/profile-settings-model/profile-settings-model.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    NavbarComponent,
    ProfileTabListComponent,
    FriendshipStatusButtonComponent,
    FriendshipModelComponent,
    ProfileSettingsModelComponent
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  profile!: IProfile;

  currentUser!: IUser | null;
  friendship!: IFriendShipStatus;
  friendships!: IFriendShip[];

  friendShipKind!: 'Followers' | 'Followings';

  FriendShipStatusEnum = FriendShipStatusEnum;

  @ViewChild('tabListComponent') tabListComponent!: ProfileTabListComponent;

  userService: UserService = inject(UserService);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.loadLoggedInUser();
    this.loadUserProfile();
    this.listenFriendshipChanged();
  }

  listenFriendshipChanged(): void {
    this.userService.friendshipStatusChangedEmitter.subscribe({
      next: () => this.loadUserFriendshipStatus(),
    });
  }

  loadUserFriendshipStatus(): void {
    var request: IGetUserFriendShipRequest = {
      userId: this.currentUser?.id as string,
      profileId: String(this.activatedRoute.snapshot.paramMap.get('userId')),
    };
    this.userService.getFriendShipStatus(request).subscribe({
      next: (status: IFriendShipStatus) => (this.friendship = status),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  loadLoggedInUser(): void {
    this.authService.currentUser$.subscribe({
      next: (user: IUser | null) => (this.currentUser = user),
    });
  }

  loadUserProfile(): void {
    this.userService
      .getUserProfile(
        String(this.activatedRoute.snapshot.paramMap.get('userId'))
      )
      .subscribe({
        next: (profile: IProfile) => {
          this.profile = profile;
          if (profile) {
            !this.isOwnProfile() && this.loadUserFriendshipStatus();
          }
        },
        error: (err: HttpErrorResponse) => this.router.navigateByUrl('/'),
      });
  }

  onPostCountClicked(): void {
    this.tabListComponent.onTabClicked(1);
  }

  onFollowersCountClicked(): void {
    this.friendShipKind = 'Followers';
    this.userService.getUserFollowers(this.profile.id).subscribe({
      next: (friendships: IFriendShip[]) => {
        this.friendships = [];
        this.friendships = friendships;
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onFollowingsCountClicked(): void {
    this.friendShipKind = 'Followings';
    this.userService.getUserFollowings(this.profile.id).subscribe({
      next: (friendships: IFriendShip[]) => {
        this.friendships = [];
        this.friendships = friendships;
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  isOwnProfile(): boolean {
    return this.currentUser?.id === this.profile.id;
  }
}
