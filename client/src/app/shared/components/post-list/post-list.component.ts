import { Component, OnInit, inject, signal } from '@angular/core';
import { IPostOverview } from '../../types/post';
import { PostService } from '../../services/post.service';
import { map } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { PostItemComponent } from '../post-item/post-item.component';

@Component({
  selector: 'app-post-list',
  standalone: true,
  imports: [CommonModule, PostItemComponent],
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.scss'
})
export class PostListComponent implements OnInit {

  // signals
  posts = signal<IPostOverview[]>([]);

  // services
  postService: PostService = inject(PostService);

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.postService.getAllPostsAsync().subscribe({
      next: (posts: IPostOverview[]) => this.posts.set(posts),
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

}
