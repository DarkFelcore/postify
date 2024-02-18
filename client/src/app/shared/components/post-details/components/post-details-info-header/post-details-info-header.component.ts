import { Component, Input } from '@angular/core';
import { IUserPoster } from '../../../../types/post';
import { CommonModule } from '@angular/common';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';

@Component({
  selector: 'app-post-details-info-header',
  standalone: true,
  imports: [CommonModule, StoryBubbleItemComponent],
  templateUrl: './post-details-info-header.component.html',
  styleUrl: './post-details-info-header.component.scss'
})
export class PostDetailsInfoHeaderComponent {
  @Input() poster!: IUserPoster;
}
