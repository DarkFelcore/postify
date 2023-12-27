import { EventEmitter, Injectable, inject } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { INotification } from '../types/notification';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private hubConnection!: signalR.HubConnection;

  public notificationEmitter: EventEmitter<INotification> = new EventEmitter<INotification>();
  public toastrService: ToastrService = inject(ToastrService);

  public startConnection = () => {
    var token: string = localStorage.getItem('token') as string;
    if(token) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7127/Notify", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
          accessTokenFactory: () => token
        })
        .build();
      
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
  }

  public addNotificationListener = () => {
    this.hubConnection.on("SendNotification", (notification: INotification) => {
      this.notificationEmitter.emit(notification);
      this.toastrService.info("You have new notifications");
    })
  }
  
}
