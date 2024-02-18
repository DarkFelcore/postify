import { Component, OnInit, inject, signal } from '@angular/core';
import { IPostOverview } from '../../types/post';
import { PostService } from '../../services/post.service';
import { Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { PostItemComponent } from '../post-item/post-item.component';
import { AuthService } from '../../../auth/auth.service';
import { IUser } from '../../types/user';

@Component({
  selector: 'app-post-list',
  standalone: true,
  imports: [CommonModule, PostItemComponent],
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.scss',
})
export class PostListComponent implements OnInit {
  currentUser$!: Observable<IUser | null>;

  // signals
  posts = signal<IPostOverview[]>([]);

  // services
  postService: PostService = inject(PostService);
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.loadPosts();
    this.listenLoadPosts();
  }

  listenLoadPosts(): void {
    this.postService.loadPostsEmitter.subscribe({
      next: () => this.loadPosts(),
      error: (err: any) => console.log(err),
    });
  }

  loadPosts(): void {
    this.postService.getAllPostsAsync().subscribe({
      next: (posts: IPostOverview[]) => this.posts.set(posts),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }
}
