import { Component, OnInit, inject } from '@angular/core';
import { StoryBubbleItemComponent } from '../story-bubble-item/story-bubble-item.component';
import { StoryBubbleDefaultItemComponent } from '../story-bubble-default-item/story-bubble-default-item.component';
import { Observable } from 'rxjs';
import { IUser } from '../../types/user';
import { AuthService } from '../../../auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-story-bubble-list',
  standalone: true,
  imports: [CommonModule, StoryBubbleItemComponent, StoryBubbleDefaultItemComponent],
  templateUrl: './story-bubble-list.component.html',
  styleUrl: './story-bubble-list.component.scss'
})
export class StoryBubbleListComponent implements OnInit  {
  currentUser$!: Observable<IUser | null>;
  
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
  }
}
