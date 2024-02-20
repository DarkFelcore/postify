import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IComment, IPostDetails, IPostOverview } from '../types/post';
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

  loadPosts(): void {
    this.loadPostsEmitter.emit();
  }
}
