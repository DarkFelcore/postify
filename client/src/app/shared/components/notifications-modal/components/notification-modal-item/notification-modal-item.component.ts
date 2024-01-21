import { Component, Input, inject } from '@angular/core';
import { INotification } from '../../../../types/notification';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';
import { NotificationTypeEnum } from '../../../../types/enums/enums';
import { UserService } from '../../../../services/user.service';
import { IAcceptFollowRequest, IRejectFollowRequest } from '../../../../types/requests/requests';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-notification-modal-item',
  standalone: true,
  imports: [StoryBubbleItemComponent],
  templateUrl: './notification-modal-item.component.html',
  styleUrl: './notification-modal-item.component.scss',
})
export class NotificationModalItemComponent {
  @Input() notification!: INotification;

  // Services
  userService: UserService = inject(UserService);

  NotificationTypeEnum = NotificationTypeEnum;

  onFollowRequestAcceptedClicked(): void {
    const request: IAcceptFollowRequest = {
      followerId: this.notification.senderId,
    };
    this.userService.acceptFollowRequest(request).subscribe({
      next: () => this.userService.followRequestDecision(),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onFollowRequestRejectedClicked(): void {
    const request: IRejectFollowRequest = {
      followerId: this.notification.senderId,
    };
    this.userService.rejectFollowRequest(request).subscribe({
      next: () => this.userService.followRequestDecision(),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }
}
