import { Component, Input, inject, signal } from '@angular/core';
import { IFriendShip, IUserToUnfollow } from '../../types/user';
import { UnfollowUserModelComponent } from '../unfollow-user-model/unfollow-user-model.component';
import { UserService } from '../../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IProfile } from '../../types/profile';
import { Router } from '@angular/router';
import { FriendShipStatusEnum } from '../../types/enums/enums';
import { IFollowUserRequest } from '../../types/requests/requests';

@Component({
  selector: 'app-friendship-status-button',
  standalone: true,
  imports: [UnfollowUserModelComponent],
  templateUrl: './friendship-status-button.component.html',
  styleUrl: './friendship-status-button.component.scss'
})
export class FriendshipStatusButtonComponent {
  @Input() status!: string;
  @Input() isProfilePage: boolean = true;
  @Input() user!: IProfile | IFriendShip;

  FriendShipStatusEnum = FriendShipStatusEnum;

  userService: UserService = inject(UserService);
  router: Router = inject(Router);

  followUser(): void {
    var request : IFollowUserRequest = { userId: this.user?.id }
    this.userService.followUser(request).subscribe({
      next: () => this.userService.friendshipChanged(),
      error: (err: HttpErrorResponse) => console.log("Unable to follow user: " + err)
    })
  }

  setUserToUnfollow(): void {
    const user: IUserToUnfollow = {
      id: this.user.id,
      userName: this.user.userName,
      pictureUrl: this.user.pictureUrl
    }
    this.userService.setUserToUnfollow(user)
  }

}
