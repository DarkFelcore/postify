import { Component, inject } from '@angular/core';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-profile-settings-model',
  standalone: true,
  imports: [],
  templateUrl: './profile-settings-model.component.html',
  styleUrl: './profile-settings-model.component.scss'
})
export class ProfileSettingsModelComponent {
  authService: AuthService = inject(AuthService);
}
