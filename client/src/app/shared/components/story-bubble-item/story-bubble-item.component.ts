import { Component, Input, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-story-bubble-item',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './story-bubble-item.component.html',
  styleUrl: './story-bubble-item.component.scss'
})
export class StoryBubbleItemComponent implements OnInit {
  height!: string;
  @Input() width!: string;
  @Input() pictureUrl!: string | undefined;
  @Input() userId!: string;

  ngOnInit(): void {
    this.height = this.width;
  }
}
