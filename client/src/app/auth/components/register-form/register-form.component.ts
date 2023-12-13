import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginFacebookComponent } from '../../../shared/components/login-facebook/login-facebook.component';
import { HorisontalDividerTextCenteredComponent } from '../../../shared/components/horisontal-divider-text-centered/horisontal-divider-text-centered.component';
import { Router, RouterModule } from '@angular/router';
import { RegisterRequest } from '../../../shared/requests/register-request';
import { AuthService } from '../../auth.service';
import { IUser } from '../../../shared/types/user';
import { HttpErrorResponse } from '@angular/common/http';
import { EMAIL_REGEX, PASSWORD_REGEX } from '../../../shared/constants/constants';
import { RegisterFormErrorsComponent } from '../register-form-errors/register-form-errors.component';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    HorisontalDividerTextCenteredComponent,
    LoginFacebookComponent,
    RouterModule,
    RegisterFormErrorsComponent
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss'
})
export class RegisterFormComponent implements OnInit {
  registerForm!: FormGroup;

  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);
  fb: FormBuilder = inject(FormBuilder);

  @Output() toggleFormEmitter: EventEmitter<void> = new EventEmitter<void>();

  ngOnInit(): void {
    this.buildRegisterForm();
  }

  buildRegisterForm(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(EMAIL_REGEX)]],
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.pattern(PASSWORD_REGEX)]],
    });
  }

  onSubmit(): void {
    if(this.registerForm.valid) {
      const request: RegisterRequest = {
        email: this.registerForm.get('email')?.value,
        firstName: this.registerForm.get('firstName')?.value,
        lastName: this.registerForm.get('lastName')?.value,
        userName: this.registerForm.get('userName')?.value,
        password: this.registerForm.get('password')?.value
      }
      this.authService.register(request).subscribe({
        next: (user: IUser | null) => this.router.navigateByUrl('/')
      });
    }
  }

  toggleForm(): void {
    this.registerForm.reset();
    this.toggleFormEmitter.emit();
  }
}
