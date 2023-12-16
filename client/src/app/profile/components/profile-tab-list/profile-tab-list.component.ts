import { Component, Input, OnInit, inject } from '@angular/core';
import { IProfile } from '../../../shared/types/profile';
import { IUser } from '../../../shared/types/user';
import { AuthService } from '../../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { ProfilePostItemComponent } from '../profile-post-item/profile-post-item.component';

@Component({
  selector: 'app-profile-tab-list',
  standalone: true,
  imports: [CommonModule, ProfilePostItemComponent],
  templateUrl: './profile-tab-list.component.html',
  styleUrl: './profile-tab-list.component.scss'
})
export class ProfileTabListComponent implements OnInit {
  
  @Input() profile!: IProfile;
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
