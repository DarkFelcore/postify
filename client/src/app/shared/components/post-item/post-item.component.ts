import moment from 'moment';
import {
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import {
  IComment,
  IPostDetails,
  IPostOverview,
  IUserPoster,
} from '../../types/post';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import { RouterModule } from '@angular/router';
import { IUser } from '../../types/user';
import { PostService } from '../../services/post.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ICommentPostRequest } from '../../types/requests/requests';
import { finalize, take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { PostDetailsComponent } from '../post-details/post-details.component';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [
    CommonModule,
    StoryBubbleItemComponent,
    RouterModule,
    PostDetailsComponent,
  ],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.scss',
})
export class PostItemComponent implements OnInit {
  @ViewChild('commentInput') commentInput!: ElementRef;

  @Input() post!: IPostOverview;
  @Input() currentUser!: IUser | null;

  childCommentsMap: Map<string, IComment[]> = new Map<string, IComment[]>();

  firstPostLikeUser!: IUserPoster;
  postDetails!: IPostDetails;

  timeStamp!: string;
  description!: string;
  postModalId!: string;

  hasOpenedDescription: boolean = false;
  totalComments!: number;
  isPostLiked: boolean = false;
  hasActivePostComment: boolean = false;

  private readonly postService: PostService = inject(PostService);

  ngOnInit(): void {
    this.postModalId = `#post-details-modal-${this.post?.id}`;
    this.timeStamp = moment(this.post.createdAt).fromNow();
    this.firstPostLikeUser = this.post.postLikes.filter(
      (x) => x.userName !== this.post.poster.userName
    )[0];

    this.checkPostedLiked();
    this.getModifiedDescription();
    this.fetchPostDetails();
  }

  fetchPostDetails(): void {
    this.postService.getPostByIdAsync(this.post.id).subscribe({
      next: (postDetails: IPostDetails) => {
        this.postDetails = postDetails;
        this.initChildComments();
      },
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  initChildComments(): void {
    this.postDetails.comments.forEach((comment) => {
      const parentCommentId = comment.parentCommentId as string;
      if (parentCommentId !== null) {
        if (this.childCommentsMap.has(parentCommentId)) {
          const existingComments = this.childCommentsMap.get(parentCommentId)!;
          this.childCommentsMap.set(parentCommentId, [
            ...existingComments,
            comment,
          ]);
        } else {
          this.childCommentsMap.set(parentCommentId, [comment]);
        }
      }
    });
  }

  checkPostedLiked(): void {
    if (
      this.post.postLikes.findIndex((x) => x.id === this.currentUser?.id) !== -1
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
      next: (_: boolean) => {},
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  // Receives like emit from post details
  onReceiveLikePost(status: boolean): void {
    this.isPostLiked = status;
  }

  onPostCommentInputChange(event: Event): void {
    let value = (event.target as HTMLInputElement).value;
    // set to true if the value is not empty, otherwise false
    this.hasActivePostComment = !!value;
  }

  onPlaceComment(parentCommentId?: string): void {
    const value = this.commentInput?.nativeElement?.value as string;
    if (value && value.length > 0) {
      var request: ICommentPostRequest = {
        description: value,
        parentCommentId: parentCommentId,
      };

      this.postService
        .commentPostAsync(this.post.id, request)
        .pipe(
          take(1),
          finalize(() => (this.hasActivePostComment = false))
        )
        .subscribe({
          next: (_: IComment) => this.postService.loadPosts(),
          error: (err: HttpErrorResponse) => {
            console.log(err);
          },
        });

      this.commentInput.nativeElement.value = '';
    }
  }
}
