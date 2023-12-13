import { Component } from '@angular/core';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import { StoryBubbleDefaultItemComponent } from '../story-bubble-default-item/story-bubble-default-item.component';

@Component({
  selector: 'app-story-bubble-list',
  standalone: true,
  imports: [StoryBubbleItemComponent, StoryBubbleDefaultItemComponent],
  templateUrl: './story-bubble-list.component.html',
  styleUrl: './story-bubble-list.component.scss'
})
export class StoryBubbleListComponent {

}
