import { Component, Input, OnChanges, OnInit, inject, signal } from '@angular/core';
import { IComment, IPostDetails } from '../../../../types/post';
import { CommonModule } from '@angular/common';
import { StoryBubbleItemComponent } from '../../../story-bubble-item/story-bubble-item.component';
import moment from 'moment';
import { RouterModule } from '@angular/router';
import { PostService } from '../../../../services/post.service';

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

  timeStamp!: string;
  likeCount!: number;

  isChildSectionOpened: boolean = false;
  collapsedChildCommentId!: string;

  postService: PostService = inject(PostService);

  ngOnInit(): void {
    this.timeStamp = moment(this.comment.createdAt).fromNow();
    this.likeCount = this.comment.commentLikes?.length;
    this.collapsedChildCommentId = `show-comments-${this.comment.id}`
  }

  toggleChildComments(): void {
    this.isChildSectionOpened = !this.isChildSectionOpened;
  }

  onLikePostCommentClicked(): void {
    
  }
}
