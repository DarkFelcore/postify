export interface IUser {
    id: string;
    pictureUrl: string;
    token: string;
}

export interface IFriendShipStatus {
    status: string;
}

export enum FriendShipStatusEnum {
    Following = "Following",
    Pending = "Pending",
    Follow = "Follow",
    FollowBack = "Follow Back",
}

export interface IFriendShip {
    id: string,
    pictureUrl?: string;
    userName: string;
    firstName: string;
    lastName: string;
    friendShipStatus: FriendShipStatusEnum;
}