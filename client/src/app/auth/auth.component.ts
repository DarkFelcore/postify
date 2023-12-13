import { Component, OnInit } from '@angular/core';
import { PhonePreviewSlideshowComponent } from '../shared/components/phone-preview-slideshow/phone-preview-slideshow.component';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { RegisterFormComponent } from './components/register-form/register-form.component';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    PhonePreviewSlideshowComponent,
    LoginFormComponent,
    RegisterFormComponent
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent implements OnInit {

  isSignInForm: boolean = true;

  ngOnInit(): void {

  }

  toggleForm(): void {
    this.isSignInForm = !this.isSignInForm;
  }
}
