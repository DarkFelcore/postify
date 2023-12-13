import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IPostOverview } from '../types/post';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  http: HttpClient = inject(HttpClient);

  getAllPostsAsync(): Observable<IPostOverview[]> {
    return this.http.get<IPostOverview[]>(environment.baseUrl + 'posts');
  }
  
}
