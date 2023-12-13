import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-register-form-errors',
  standalone: true,
  imports: [],
  templateUrl: './register-form-errors.component.html',
  styleUrl: './register-form-errors.component.scss'
})
export class RegisterFormErrorsComponent {
  @Input() registerForm!: FormGroup;
}
