import { IncrementDecrementEnum } from './enums/enums';

export interface IPostOverview {
  id: string;
  description: string;
  image: string;
  createdAt: string;
  commentsCount: number;
  postLikes: IUserPoster[];
  poster: IUserPoster;
}

export interface IPostDetails {
  id: string;
  description: string;
  image: string;
  createdAt: string;
  comments: IComment[];
  poster: IUserPoster;
  postLiked: boolean;
}

export interface IComment {
  id: string;
  parentCommentId?: string;
  description: string;
  createdAt: string;
  commentLikes: ICommentLike[];
  user: IUserPoster;
}

export interface ICommentLike {
  userId: string;
  commentId: string;
}

export interface IUserPoster {
  id: string;
  pictureUrl: string;
  userName: string;
}

export interface IModifyPostCommentsCountAction {
  amount: number;
  action: IncrementDecrementEnum;
}
