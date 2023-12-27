export interface IUser {
    id: string;
    pictureUrl: string;
    token: string;
}

export interface IFollowUserRequest {
    userId: string;
}

export interface IUserToUnfollow {
    id: string;
    pictureUrl?: string;
    userName: string;
}

export interface IFriendShipStatus {
    status: string;
}

export interface IFriendShip {
    id: string,
    pictureUrl?: string;
    userName: string;
    firstName: string;
    lastName: string;
    friendShipStatus: FriendShipStatusEnum;
}

export enum FriendShipStatusEnum {
    Following = "Following",
    Pending = "Pending",
    Follow = "Follow",
    FollowBack = "Follow Back",
}