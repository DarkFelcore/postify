import { NotificationTypeEnum } from './enums/enums';

export interface INotification {
  message: string;
  isRead: boolean;
  type: NotificationTypeEnum;
  createdAt: string;
  senderId: string;
  followRequestUsername?: string;
  followRequestPictureUrl?: string;
  followRequestCurrentFriendShipStatus?: string;
  followRequestAcceptedUsername?: string;
  followRequestAcceptedPictureUrl?: string;
}

export interface GroupedNotifications {
  [dateGroup: string]: INotification[];
}
