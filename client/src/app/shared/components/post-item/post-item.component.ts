import { Component, Input, OnInit } from '@angular/core';
import { IPostOverview, IUserPoster } from '../../types/post';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import moment from 'moment';
import { RouterModule } from '@angular/router';
import { IUser } from '../../types/user';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [StoryBubbleItemComponent, RouterModule],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.scss',
})
export class PostItemComponent implements OnInit {
  @Input() post!: IPostOverview;
  @Input() currentUser!: IUser;

  firstPostLikeUser!: IUserPoster;

  timeStamp!: string;
  description!: string;
  hasOpenedDescription: boolean = false;
  totalComments!: number;
  isPostLiked: boolean = false;

  ngOnInit(): void {
    this.timeStamp = moment(this.post.createdAt).fromNow();
    this.firstPostLikeUser = this.post.postLikes.filter(
      (x) => x.userName !== this.post.poster.userName
    )[0];

    this.checkPostedLiked();
    this.getModifiedDescription();
  }

  checkPostedLiked(): void {
    if (
      this.post.postLikes.findIndex((x) => x.id === this.currentUser.id) !== -1
    ) {
      this.isPostLiked = true;
    }
  }

  getModifiedDescription(): void {
    if (this.isTruncatedDescription()) {
      this.description = this.truncateDescription();
    } else {
      this.description = this.resetDescription();
    }
  }

  showFullDescription(): void {
    if (this.isTruncatedDescription() && !this.hasOpenedDescription) {
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
