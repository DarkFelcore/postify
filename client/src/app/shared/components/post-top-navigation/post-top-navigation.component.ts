import { Component } from '@angular/core';

@Component({
  selector: 'app-post-top-navigation',
  standalone: true,
  imports: [],
  templateUrl: './post-top-navigation.component.html',
  styleUrl: './post-top-navigation.component.scss'
})
export class PostTopNavigationComponent {

  navItemClickedValue: string = "1";

  onNavItemClickedValue(event: MouseEvent): void {
    this.navItemClickedValue = (event.target as HTMLAnchorElement).getAttribute('value') as string;
  }
}
