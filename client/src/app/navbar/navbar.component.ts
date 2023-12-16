import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ActivatedRoute, ParamMap, Router, RouterModule } from '@angular/router';
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
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  userId!: string;
  currentUser$!: Observable<IUser | null>;
  
  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
  }

  onViewOwnProfileClicked(userId: string): void {
    this.router.navigateByUrl('/profile/' + userId)
  }
  
  logout(): void {
    this.authService.logout();
  }

}
