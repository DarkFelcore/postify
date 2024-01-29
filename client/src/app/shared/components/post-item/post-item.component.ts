import moment from 'moment';
import { Component, Input, OnInit, inject } from '@angular/core';
import { IPostOverview, IUserPoster } from '../../types/post';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import { RouterModule } from '@angular/router';
import { IUser } from '../../types/user';
import { PostService } from '../../services/post.service';
import { HttpErrorResponse } from '@angular/common/http';

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
  hasActivePostComment: boolean = false;

  private readonly postService: PostService = inject(PostService);

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

  onLikePost(isDoubleClicked: boolean = false): void {
    if (this.isPostLiked && isDoubleClicked) {
      return;
    }
    this.isPostLiked = !this.isPostLiked;
    this.postService.likePostAsync(this.post?.id).subscribe({
      next: (status: boolean) => {},
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onPostCommentInputChange(event: Event): void {
    let value = (event.target as HTMLInputElement).value;
    // set to true if the value is not empty, otherwise false
    this.hasActivePostComment = !!value;
  }
}
