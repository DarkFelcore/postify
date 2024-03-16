import { Component, Input, OnInit, inject } from '@angular/core';
import {
  IComment,
  IModifyPostCommentsCountAction,
  IPostDetails,
} from '../../../../types/post';
import { CommonModule } from '@angular/common';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';
import moment from 'moment';
import { RouterModule } from '@angular/router';
import { PostService } from '../../../../services/post.service';
import { IUser } from '../../../../types/user';
import { ILikePostCommentRequest } from '../../../../types/requests/requests';
import { HttpErrorResponse } from '@angular/common/http';
import { IncrementDecrementEnum } from '../../../../types/enums/enums';

@Component({
  selector: 'app-post-details-comment-item',
  standalone: true,
  imports: [CommonModule, RouterModule, StoryBubbleItemComponent],
  templateUrl: './post-details-comment-item.component.html',
  styleUrl: './post-details-comment-item.component.scss',
})
export class PostDetailsCommentItemComponent implements OnInit {
  @Input() comment!: IComment;
  @Input() postDetails!: IPostDetails;
  @Input() childCommentsMap!: Map<string, IComment[]>;
  @Input() currentUser!: IUser | null;
  @Input() isChildComment: boolean = false;

  timeStamp!: string;
  likeCount: number = 0;
  commentOptionsModalId!: string;

  isChildSectionOpened: boolean = false;
  isCommentLikedByCurrentUser: boolean = false;
  collapsedChildCommentId!: string;

  postService: PostService = inject(PostService);

  ngOnInit(): void {
    this.timeStamp = moment(this.comment.createdAt).fromNow();
    this.commentOptionsModalId = `#comment-item-options-modal-${this.comment.id}`;
    this.collapsedChildCommentId = `show-comments-${this.comment.id}`;
    this.likeCount = this.comment.commentLikes?.length;
    this.isCommentLikedByCurrentUser = this.checkPostCommentLikeByCurrentUser();
  }

  toggleChildComments(): void {
    this.isChildSectionOpened = !this.isChildSectionOpened;
  }

  onLikePostCommentClicked(): void {
    const request: ILikePostCommentRequest = {
      userId: this.currentUser?.id as string,
      commentId: this.comment?.id,
    };
    this.isCommentLikedByCurrentUser = !this.isCommentLikedByCurrentUser;
    this.postService.likePostCommentAsync(request).subscribe({
      next: (_: boolean) => this.modifyPostLikeCount(),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onDeleteCommentClicked(id: string): void {
    this.postService.deletePostCommentAsync(id).subscribe({
      next: (_: void) => {
        const index = this.postDetails?.comments?.findIndex(
          (x: IComment) => x.id === this.comment?.id
        );

        // Delete root comment
        if (index !== -1 && typeof index === 'number') {
          const action: IModifyPostCommentsCountAction = {
            action: IncrementDecrementEnum.Decrement,
            amount: this.countChildCommentsAmountByParentCommentId(id) + 1,
          };
          this.postService.modifyPostCommentCount(action);
          this.postDetails.comments.splice(index, 1);
        }
        // Delete child comment
        else {
          const parentCommentId = this.comment?.parentCommentId as string;
          if (this.childCommentsMap.has(parentCommentId)) {
            const existingChildComments =
              this.childCommentsMap.get(parentCommentId)!;
            var newChildComments = existingChildComments.filter(
              (x) => x.id !== this?.comment?.id
            );
            this.childCommentsMap.set(parentCommentId, newChildComments);
            const action: IModifyPostCommentsCountAction = {
              action: IncrementDecrementEnum.Decrement,
              amount: 1,
            };
            this.postService.modifyPostCommentCount(action);
          }
        }
      },
    });
  }

  private checkPostCommentLikeByCurrentUser(): boolean {
    var commentLikes = this.comment?.commentLikes;
    if (commentLikes?.some((x) => x.userId === this.currentUser?.id))
      return true;
    return false;
  }

  private modifyPostLikeCount(): void {
    // Like
    if (this.isCommentLikedByCurrentUser) {
      if (this.likeCount === undefined) this.likeCount = 1;
      else this.likeCount++;
    }
    // Dislike
    else {
      if (this.likeCount > 0) this.likeCount--;
      else this.likeCount = 0;
    }
  }

  private countChildCommentsAmountByParentCommentId(commentId: string): number {
    if (this.childCommentsMap?.has(commentId)) {
      return this.childCommentsMap?.get(commentId)?.length!;
    }
    return 0;
  }
}
