import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from '../shared/types/user';
import { CommonModule } from '@angular/common';
import { SignalrService } from '../shared/services/signalr.service';
import { INotification } from '../shared/types/notification';
import { NotificationsModalComponent } from '../shared/components/notifications-modal/notifications-modal.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, NotificationsModalComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  authService: AuthService = inject(AuthService);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  cd: ChangeDetectorRef = inject(ChangeDetectorRef);
  signalRService: SignalrService = inject(SignalrService);

  userId!: string;
  notificationReceived: boolean = false;
  currentUser$!: Observable<IUser | null>;

  constructor() {
    this.signalRService.startConnection();
    this.signalRService.addNotificationListener();
  }
  
  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.listenNotifications();
  }

  listenNotifications(): void {
    this.signalRService.notificationEmitter.subscribe({
      next: (notification: INotification) => {
        console.log(notification)
        if(notification) this.notificationReceived = true;
      }
    })
  }

  onViewOwnProfileClicked(userId: string): void {
    this.router.navigateByUrl('/profile/' + userId)
  }

}
