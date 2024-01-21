export interface IFollowUserRequest {
  userId: string;
}

export interface IGetUserFriendShipRequest {
  userId: string;
  profileId: string;
}

export interface IAcceptFollowRequest {
  followerId: string;
}

export interface IRejectFollowRequest {
  followerId: string;
}


export interface IRefreshTokenRequest {
  accessToken: string;
  refreshToken: string;
}