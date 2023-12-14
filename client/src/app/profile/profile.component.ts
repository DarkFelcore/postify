import { Component, OnInit, inject, signal } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { UserService } from '../shared/services/user.service';
import { IProfile } from '../shared/types/profile';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ProfileTabListComponent } from './components/profile-tab-list/profile-tab-list.component';
import { Observable } from 'rxjs';
import { IUser } from '../shared/types/user';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent, ProfileTabListComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {

  profile = signal<IProfile | null>(null);

  currentUser!: IUser | null;

  userService: UserService = inject(UserService);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  authService: AuthService = inject(AuthService);


  ngOnInit(): void {
    this.loadLoggedInUser();
    this.loadUserProfile();
  }

  loadLoggedInUser(): void {
    this.authService.currentUser$.subscribe({
      next: (user: IUser | null) => this.currentUser = user
    })
  }

  loadUserProfile(): void {
    this.userService.getUserProfile(String(this.activatedRoute.snapshot.paramMap.get('userId'))).subscribe({
      next: (profile: IProfile) => this.profile.set(profile),
      error: (err: HttpErrorResponse) => this.router.navigateByUrl('/')
    })
  }

  isOwnProfile(): boolean {
    return this.currentUser?.id === this.profile()?.id;
  }

  
}
