import { Component, Input, OnDestroy } from '@angular/core';
import { IFriendShip } from '../../types/user';
import { FriendshipModelItemComponent } from './components/friendship-model-item/friendship-model-item.component';

@Component({
  selector: 'app-friendship-model',
  standalone: true,
  imports: [FriendshipModelItemComponent],
  templateUrl: './friendship-model.component.html',
  styleUrl: './friendship-model.component.scss',
})
export class FriendshipModelComponent implements OnDestroy {
  @Input() friendships!: IFriendShip[];
  @Input() userId!: string | undefined;
  @Input() friendShipKind!: 'Followers' | 'Followings';

  ngOnDestroy(): void {
    const modalBackdrop = document.querySelector('.modal-backdrop');
    if (modalBackdrop) modalBackdrop.remove();
  }
}
