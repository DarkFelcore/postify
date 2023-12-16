import { Component, Input } from '@angular/core';
import { IProfilePost } from '../../../shared/types/profile';

@Component({
  selector: 'app-profile-post-item',
  standalone: true,
  imports: [],
  templateUrl: './profile-post-item.component.html',
  styleUrl: './profile-post-item.component.scss'
})
export class ProfilePostItemComponent {
  @Input() post!: IProfilePost;
}
