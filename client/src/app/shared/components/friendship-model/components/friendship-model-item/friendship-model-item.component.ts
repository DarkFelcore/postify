import { Component, Input } from '@angular/core';
import { IFriendShip } from '../../../../types/user';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';
import { FriendshipStatusButtonComponent } from '../../../friendship-status-button/friendship-status-button.component';

@Component({
  selector: 'app-friendship-model-item',
  standalone: true,
  imports: [StoryBubbleItemComponent, FriendshipStatusButtonComponent],
  templateUrl: './friendship-model-item.component.html',
  styleUrl: './friendship-model-item.component.scss'
})
export class FriendshipModelItemComponent {
  @Input() friendShip!: IFriendShip;
  @Input() userId!: string | undefined;
}
