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

export interface ICommentPostRequest {
  description: string;
  parentCommentId?: string;
}

export interface ILikePostCommentRequest {
  commentId: string;
  userId: string;
}