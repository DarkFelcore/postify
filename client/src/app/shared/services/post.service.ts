import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import {
  IComment,
  IModifyPostCommentsCountAction,
  IPostDetails,
  IPostOverview,
} from '../types/post';
import { environment } from '../../../environments/environment.development';
import {
  ICommentPostRequest,
  ILikePostCommentRequest,
} from '../types/requests/requests';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  http: HttpClient = inject(HttpClient);

  loadPostsEmitter: EventEmitter<void> = new EventEmitter<void>();
  reactPostCommentEmitter: EventEmitter<IComment> =
    new EventEmitter<IComment>();
  modifyPostCountEmitter: EventEmitter<IModifyPostCommentsCountAction> =
    new EventEmitter<IModifyPostCommentsCountAction>();

  getAllPostsAsync(): Observable<IPostOverview[]> {
    return this.http.get<IPostOverview[]>(environment.baseUrl + 'posts');
  }

  getPostByIdAsync(postId: string): Observable<IPostDetails> {
    return this.http.get<IPostDetails>(environment.baseUrl + 'posts/' + postId);
  }

  likePostAsync(postId: string): Observable<boolean> {
    return this.http.post<boolean>(
      environment.baseUrl + 'posts/like/' + postId,
      null
    );
  }

  commentPostAsync(
    postId: string,
    request: ICommentPostRequest
  ): Observable<IComment> {
    return this.http.post<IComment>(
      environment.baseUrl + 'posts/comment/' + postId,
      request
    );
  }

  likePostCommentAsync(request: ILikePostCommentRequest): Observable<boolean> {
    return this.http.post<boolean>(
      environment.baseUrl + 'posts/like/comment',
      request
    );
  }

  favoritePostAsync(postId: string): Observable<boolean> {
    return this.http.post<boolean>(
      environment.baseUrl + 'posts/favorite/' + postId,
      null
    );
  }

  deletePostCommentAsync(commentId: string): Observable<void> {
    return this.http.delete<void>(
      environment.baseUrl + 'posts/comment/' + commentId
    );
  }

  loadPosts(): void {
    this.loadPostsEmitter.emit();
  }

  reactPostComment(comment: IComment): void {
    this.reactPostCommentEmitter.emit(comment);
  }

  modifyPostCommentCount(action: IModifyPostCommentsCountAction): void {
    this.modifyPostCountEmitter.emit(action);
  }
}
