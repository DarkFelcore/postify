export interface INotification {
    message: string;
    isRead: boolean;
    type: INotificationType;
    reveiverId: string;
    senderId: string;
    createdAt: string;
}

export enum INotificationType {
    Like,
    Comment,
    FolloweRequest,
    FollowAccepted,
    Story
}