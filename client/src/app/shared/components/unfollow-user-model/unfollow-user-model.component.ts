import { Component, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unfollow-user-model',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './unfollow-user-model.component.html',
  styleUrl: './unfollow-user-model.component.scss'
})
export class UnfollowUserModelComponent {
  
  userService: UserService = inject(UserService);
  router: Router = inject(Router);

  public onUnfollowButtonClicked(): void {
    const followedId = this.userService.getUserToUnfollow().id;
    this.userService.unfollowUser(followedId).subscribe({
      next: () => this.userService.friendshipChanged(),
      error: (err: HttpErrorResponse) => console.log("Unable to unfollow user: " + err)
    });
  }

}
