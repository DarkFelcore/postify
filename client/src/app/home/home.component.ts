import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { PostListComponent } from '../shared/components/post-list/post-list.component';
import { PostTopNavigationComponent } from '../shared/components/post-top-navigation/post-top-navigation.component';
import { StoryBubbleListComponent } from '../shared/components/story-bubble-list/story-bubble-list.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, PostListComponent, PostTopNavigationComponent, StoryBubbleListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
