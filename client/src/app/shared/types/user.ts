import { FriendShipStatusEnum } from "./enums/enums";

export interface IUser {
  id: string;
  pictureUrl: string;
  token: string;
  refreshToken: string;
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
  id: string;
  pictureUrl?: string;
  userName: string;
  firstName: string;
  lastName: string;
  friendShipStatus: FriendShipStatusEnum;
}
