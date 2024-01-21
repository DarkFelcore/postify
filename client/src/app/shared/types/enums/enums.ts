export enum ErrorCode {
  NoNotifications = 'Notifications.NoNotifications',
  InvalidCredentials = 'Auth.InvalidCredentials',
}

export enum NotificationTypeEnum {
  Like = 'Like',
  Comment = 'Comment',
  FollowRequest = 'FollowRequest',
  FollowAccepted = 'FollowAccepted',
  Story = 'Story',
}

export enum FriendShipStatusEnum {
  Following = 'Following',
  Pending = 'Pending',
  Follow = 'Follow',
  FollowBack = 'Follow Back',
}

export enum DateRangeEnum {
  Today = 'Today',
  Yesterday = 'Yesterday',
  ThisWeek = 'This Week',
  ThisMonth = 'This Month',
  Older = 'Older',
}
