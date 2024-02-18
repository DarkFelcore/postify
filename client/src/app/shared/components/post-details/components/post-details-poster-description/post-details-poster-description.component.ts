import { Component, Input, OnInit } from '@angular/core';
import { IPostDetails } from '../../../../types/post';
import { CommonModule } from '@angular/common';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';
import { RouterModule } from '@angular/router';
import moment from 'moment';

@Component({
  selector: 'app-post-details-poster-description',
  standalone: true,
  imports: [CommonModule, StoryBubbleItemComponent, RouterModule],
  templateUrl: './post-details-poster-description.component.html',
  styleUrl: './post-details-poster-description.component.scss'
})
export class PostDetailsPosterDescriptionComponent implements OnInit {
  @Input() postDetails!: IPostDetails;

  timeStamp!: string;

  ngOnInit(): void {
    this.timeStamp = moment(this.postDetails.createdAt).fromNow();
  }
}
