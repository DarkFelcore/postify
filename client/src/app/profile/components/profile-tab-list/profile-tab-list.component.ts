import { Component, Input, OnInit, inject, signal } from '@angular/core';
import { IProfile, IProfilePost } from '../../../shared/types/profile';
import { IProfileTab } from '../../../shared/types/profile-tab';
import { Observable } from 'rxjs';
import { IUser } from '../../../shared/types/user';
import { AuthService } from '../../../auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-tab-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-tab-list.component.html',
  styleUrl: './profile-tab-list.component.scss'
})
export class ProfileTabListComponent implements OnInit {
  
  @Input() profile!: IProfile | null;
  @Input() currentUser!: IUser | null;

  selectedTabValue: number = 1;

  authService: AuthService = inject(AuthService);

  ngOnInit(): void {}

  onTabClicked(value: number): void {
    this.selectedTabValue = value;
  }

  isOwnProfile(): boolean {
    return this.currentUser?.id === this.profile?.id;
  }
}
