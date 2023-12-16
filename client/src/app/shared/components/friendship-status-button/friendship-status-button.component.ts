import { Component, Input } from '@angular/core';
import { FriendShipStatusEnum, IFriendShipStatus } from '../../types/user';

@Component({
  selector: 'app-friendship-status-button',
  standalone: true,
  imports: [],
  templateUrl: './friendship-status-button.component.html',
  styleUrl: './friendship-status-button.component.scss'
})
export class FriendshipStatusButtonComponent {
  @Input() status!: string;
  @Input() isProfilePage: boolean = true;

  get FriendShipStatusEnum() {
    return FriendShipStatusEnum;
  }
}
