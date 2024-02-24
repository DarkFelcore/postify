import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostService } from '../../services/post.service';
import { IComment, IPostDetails, IUserPoster } from '../../types/post';
import { PostDetailsInfoHeaderComponent } from './components/post-details-info-header/post-details-info-header.component';
import { PostDetailsCommentItemComponent } from './components/post-details-comment-item/post-details-comment-item.component';
import { PostDetailsPosterDescriptionComponent } from './components/post-details-poster-description/post-details-poster-description.component';
import { IUser } from '../../types/user';
import { HttpErrorResponse } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import moment from 'moment';
import { ICommentPostRequest } from '../../types/requests/requests';

@Component({
  selector: 'app-post-details',
  standalone: true,
  imports: [
    CommonModule,
    PostDetailsInfoHeaderComponent,
    PostDetailsCommentItemComponent,
    PostDetailsPosterDescriptionComponent,
    RouterModule,
  ],
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.scss',
})
export class PostDetailsComponent implements OnInit, OnDestroy {
  @Input() postDetails!: IPostDetails;
  @Input() postModalId!: string;
  @Input() childCommentsMap!: Map<string, IComment[]>;
  @Input() isPostLiked!: boolean;
  @Input() postLikes!: IUserPoster[];
  @Input() firstPostLikeUser!: IUserPoster;
  @Input() currentUser!: IUser | null;

  @Output() postLikeEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();

  @ViewChild('addCommentInput') commentInput!: ElementRef;

  postService: PostService = inject(PostService);

  timestamp!: string;
  isCommentInputEmpty: boolean = true;

  ngOnInit(): void {
    this.handlePostModalId();
  }

  ngOnChanges(): void {
    this.timestamp = moment(this.postDetails?.createdAt).fromNow();
  }

  ngOnDestroy(): void {
    const modalBackdrop = document.querySelector('.modal-backdrop');
    if (modalBackdrop) modalBackdrop.remove();
  }

  onLikePost(isDoubleClicked: boolean = false): void {
    if (this.isPostLiked && isDoubleClicked) {
      return;
    }

    this.isPostLiked = !this.isPostLiked;
    this.postLikeEmitter.emit(this.isPostLiked);
    this.postService.likePostAsync(this.postDetails?.id).subscribe({
      next: (_: boolean) => {},
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onCommentInputChange(e: Event): void {
    let value = (e.target as HTMLInputElement).value;
    value === ''
      ? (this.isCommentInputEmpty = true)
      : (this.isCommentInputEmpty = false);
  }

  onCommentPostButtonClicked(): void {
    if (!this.isCommentInputEmpty) {
      const request: ICommentPostRequest = {
        description: this.commentInput?.nativeElement?.value,
        parentCommentId: undefined,
      };
      this.postService
        .commentPostAsync(this.postDetails?.id, request)
        .subscribe({
          next: (comment: IComment) => {
            if (comment) {
              this.postDetails.comments.push(comment);
              this.commentInput.nativeElement.value = '';
              this.isCommentInputEmpty = true;
            }
          },
        });
    }
  }

  onCommentIconClicked(): void {
    this.commentInput?.nativeElement.focus();
  }

  private handlePostModalId(): void {
    this.postModalId = this.postModalId.replace('#', '');
  }
}
