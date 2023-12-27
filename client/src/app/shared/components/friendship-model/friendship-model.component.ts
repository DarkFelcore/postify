import { Component, Input } from '@angular/core';
import { IFriendShip } from '../../types/user';
import { FriendshipModelItemComponent } from './components/friendship-model-item/friendship-model-item.component';

@Component({
  selector: 'app-friendship-model',
  standalone: true,
  imports: [FriendshipModelItemComponent],
  templateUrl: './friendship-model.component.html',
  styleUrl: './friendship-model.component.scss'
})
export class FriendshipModelComponent {
  @Input() friendships!: IFriendShip[];
  @Input() userId!: string | undefined;
  @Input() friendShipKind!: 'Followers' | 'Followings';
}
