import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from '../types/post';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  http: HttpClient = inject(HttpClient);

  getAllPostsAsync(): Observable<IPost[]> {
    return this.http.get<IPost[]>(environment.baseUrl + 'posts');
  }
  
}
