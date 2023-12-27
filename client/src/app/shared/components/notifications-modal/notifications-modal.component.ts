import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-notifications-modal',
  standalone: true,
  imports: [CommonModule, NotificationsModalComponent],
  templateUrl: './notifications-modal.component.html',
  styleUrl: './notifications-modal.component.scss'
})
export class NotificationsModalComponent implements OnInit {

  ngOnInit(): void {
    
  }

  public markAllNotificationsAsRead(): void {

  }
}
