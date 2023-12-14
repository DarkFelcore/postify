import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-story-bubble-default-item',
  standalone: true,
  imports: [],
  templateUrl: './story-bubble-default-item.component.html',
  styleUrl: './story-bubble-default-item.component.scss'
})
export class StoryBubbleDefaultItemComponent {
  
  @Input() loggedInUserPictureUrl!: string;

}
