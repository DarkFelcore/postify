import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Signal,
  computed,
  inject,
  signal,
} from '@angular/core';
import { GroupedNotifications, INotification } from '../../types/notification';
import { NotificationModalItemComponent } from './components/notification-modal-item/notification-modal-item.component';
import { NotificationService } from '../../services/notification.service';
import { HttpErrorResponse } from '@angular/common/http';
import moment, { Moment } from 'moment';
import { DateRangeEnum } from '../../types/enums/enums';

@Component({
  selector: 'app-notifications-modal',
  standalone: true,
  imports: [CommonModule, NotificationModalItemComponent],
  templateUrl: './notifications-modal.component.html',
  styleUrl: './notifications-modal.component.scss',
})
export class NotificationsModalComponent implements OnInit {
  @Input() userNotifications!: Signal<INotification[]>;
  @Output() onMarkUserNotificationsAsReadEmitter: EventEmitter<void> =
    new EventEmitter<void>();

  dateGroupedNotifications = computed(() => {
    const groupedNotifications: GroupedNotifications = {};

    this.userNotifications().forEach((notification: INotification) => {
      const createdAtMoment: Moment = moment.utc(notification.createdAt);
      let dateGroup: DateRangeEnum;

      if (createdAtMoment.isSame(moment(), 'day'))
        dateGroup = DateRangeEnum.Today;
      else if (createdAtMoment.isSame(moment().subtract(1, 'day'), 'day'))
        dateGroup = DateRangeEnum.Yesterday;
      else if (createdAtMoment.isSame(moment(), 'week'))
        dateGroup = DateRangeEnum.ThisWeek;
      else if (createdAtMoment.isSame(moment(), 'month'))
        dateGroup = DateRangeEnum.ThisMonth;
      else dateGroup = DateRangeEnum.Older;

      groupedNotifications[dateGroup] = groupedNotifications[dateGroup] || [];
      groupedNotifications[dateGroup].push(notification);
    });

    return groupedNotifications;
  });

  hasUnreadNotifications = computed<boolean>(() => {
    return (
      this.userNotifications().filter(
        (notification: INotification) => !!notification.isRead
      ).length === 0
    );
  });

  // Services
  private readonly notificationService: NotificationService =
    inject(NotificationService);

  ngOnInit(): void {}

  public markAllNotificationsAsRead(): void {
    this.notificationService.markUserNotificationsAsRead().subscribe({
      next: () => this.onMarkUserNotificationsAsReadEmitter.emit(),
      error: (err: HttpErrorResponse) => console.error(err),
    });
  }
}
