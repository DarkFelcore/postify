import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AuthService } from './auth/auth.service';
import { IUser } from './shared/types/user';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  authService: AuthService = inject(AuthService);

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser(): void {
    const token: string = localStorage.getItem('token') as string;
    this.authService.loadCurrentUser(token).subscribe({
      next: (user: IUser | null) => {},
      error: (err: HttpErrorResponse) => {},
    });
  }
}
