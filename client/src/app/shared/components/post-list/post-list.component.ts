import { Component, OnInit, inject, signal } from '@angular/core';
import { IPostOverview } from '../../types/post';
import { PostService } from '../../services/post.service';
import { map } from 'rxjs';
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
  currentUser!: IUser;

  // signals
  posts = signal<IPostOverview[]>([]);

  // services
  postService: PostService = inject(PostService);
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.loadPosts();
    this.loadCurrentUser();
  }

  loadCurrentUser(): void {
    const token: string = localStorage.getItem('token') as string;
    this.authService.loadCurrentUser(token).subscribe({
      next: (user: IUser | null) => {
        if (user) {
          this.currentUser = user;
        }
      },
      error: (err: HttpErrorResponse) => {},
    });
  }

  loadPosts(): void {
    this.postService.getAllPostsAsync().subscribe({
      next: (posts: IPostOverview[]) => this.posts.set(posts),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }
}
