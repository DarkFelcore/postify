export interface IPostOverview {
  id: string;
  description: string;
  image: string;
  createdAt: string;
  commentsCount: number;
  postLikes: IUserPoster[];
  poster: IUserPoster;
}

export interface IUserPoster {
  id: string;
  pictureUrl: string;
  userName: string;
}
