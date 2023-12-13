import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-story-bubble-item',
  standalone: true,
  imports: [],
  templateUrl: './story-bubble-item.component.html',
  styleUrl: './story-bubble-item.component.scss'
})
export class StoryBubbleItemComponent implements OnInit {
  height!: string;
  @Input() width!: string;
  @Input() pictureUrl!: string;

  ngOnInit(): void {
    this.height = this.width;
  }
}
