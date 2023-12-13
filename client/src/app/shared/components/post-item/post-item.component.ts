import { Component, Input, OnInit } from '@angular/core';
import { IPost } from '../../types/post';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import moment from 'moment';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [StoryBubbleItemComponent],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.scss'
})
export class PostItemComponent implements OnInit {
  @Input() post!: IPost;

  timeStamp!: string;
  description!: string;
  hasOpenedDescription: boolean = false;
  totalComments!: number;

  ngOnInit(): void {
    this.timeStamp = moment(this.post.createdAt).fromNow();
    this.getFirstPostLikeUserName();
    this.getModifiedDescription();
  }

  getFirstPostLikeUserName(): string {
    return this.post.postLikes.filter(x => x.userName !== this.post.poster.userName)[0].userName;
  }

  getModifiedDescription(): void {
    if(this.isTruncatedDescription()) {
      this.description = this.truncateDescription();
    } else {
      this.description = this.resetDescription();
    }
  }

  showFullDescription(): void {
    if(this.isTruncatedDescription() && !this.hasOpenedDescription) {
      this.hasOpenedDescription = true;
      this.description = this.resetDescription();
    }
  }

  isTruncatedDescription(): boolean {
    return this.post.description.length > 32;
  }

  resetDescription(): string {
    return this.post.description;
  }

  truncateDescription(): string {
    return this.post.description.substring(0, 32);
  }

}
