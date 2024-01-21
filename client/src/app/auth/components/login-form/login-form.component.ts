import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { HorisontalDividerTextCenteredComponent } from '../../../shared/components/horisontal-divider-text-centered/horisontal-divider-text-centered.component';
import { LoginFacebookComponent } from '../../../shared/components/login-facebook/login-facebook.component';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest } from '../../../shared/requests/login-request';
import { AuthService } from '../../auth.service';
import { IUser } from '../../../shared/types/user';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    HorisontalDividerTextCenteredComponent, 
    LoginFacebookComponent,
    RouterModule,
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss'
})
export class LoginFormComponent implements OnInit {

  loginForm!: FormGroup;

  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);
  fb: FormBuilder = inject(FormBuilder);

  @Output() toggleFormEmitter: EventEmitter<void> = new EventEmitter<void>();

  ngOnInit(): void {
    this.buildLoginForm();
  }

  buildLoginForm(): void {
    this.loginForm = this.fb.group({
      emailOrUsername: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if(this.loginForm.valid) {
      var request: LoginRequest = {
        emailOrUsername: this.loginForm.get('emailOrUsername')?.value,
        password: this.loginForm.get('password')?.value
      }
      this.authService.login(request).subscribe({
        next: (user: IUser | null) => this.router.navigateByUrl('/')
      });
    }
  }

  toggleForm(): void {
    this.loginForm.reset();
    this.toggleFormEmitter.emit();
  }
}
