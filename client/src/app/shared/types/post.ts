export interface IPostOverview {
    description: string;
    image: string;
    createdAt: string;
    commentsCount: number;
    postLikes: IUserPoster[];
    poster: IUserPoster
}

export interface IUserPoster {
    pictureUrl: string;
    userName: string;
}