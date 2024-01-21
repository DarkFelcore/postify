import {
  ChangeDetectorRef,
  Component,
  OnInit,
  computed,
  inject,
  signal,
} from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from '../shared/types/user';
import { CommonModule } from '@angular/common';
import { SignalrService } from '../shared/services/signalr.service';
import {
  GroupedNotifications,
  INotification,
} from '../shared/types/notification';
import { NotificationsModalComponent } from '../shared/components/notifications-modal/notifications-modal.component';
import { NotificationService } from '../shared/services/notification.service';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../shared/services/user.service';
import { DateRangeEnum } from '../shared/types/enums/enums';
import moment, { Moment } from 'moment';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule, NotificationsModalComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit {
  authService: AuthService = inject(AuthService);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  cd: ChangeDetectorRef = inject(ChangeDetectorRef);
  signalRService: SignalrService = inject(SignalrService);
  notificationService: NotificationService = inject(NotificationService);
  userService: UserService = inject(UserService);

  userId!: string;
  currentUser$!: Observable<IUser | null>;
  userNotifications = signal<INotification[]>([]);

  // Computed values
  notificationReceived = computed(
    () =>
      this.userNotifications().filter((x) => !!x.isRead === false).length > 0
  );

  constructor() {
    this.signalRService.startConnection();
    this.signalRService.addNotificationListener();
  }

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;
    this.listenNotifications();
    this.listenFollowRequestDecision();
    this.getUserNotifications();
  }

  getUserNotifications(): void {
    this.notificationService.getUserNotifications().subscribe({
      next: (notifications: INotification[]) =>
        this.userNotifications.set(notifications),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  listenNotifications(): void {
    this.signalRService.notificationEmitter.subscribe({
      next: (_: INotification) => this.getUserNotifications(),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  listenFollowRequestDecision(): void {
    this.userService.followRequestDesicionEmitter.subscribe({
      next: () => this.getUserNotifications(),
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onViewOwnProfileClicked(userId: string): void {
    this.router.navigateByUrl('/profile/' + userId);
  }
}
