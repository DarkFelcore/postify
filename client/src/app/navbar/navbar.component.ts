import { Component, OnInit, inject } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from '../shared/types/user';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  
  authService: AuthService = inject(AuthService);
  
  currentUser$!: Observable<IUser | null>;

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
  }
  
  logout(): void {
    this.authService.logout();
  }
}
