import { Injectable, inject } from '@angular/core';
import { INotification } from '../types/notification';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  http: HttpClient = inject(HttpClient);

  getUserNotifications(): Observable<INotification[]> {
    return this.http.get<INotification[]>(environment.baseUrl + 'notifications');
  }

  markUserNotificationsAsRead(): Observable<void> {
    return this.http.post<void>(environment.baseUrl + 'notifications/markAsRead', null);
  }
}
