export interface IProfile {
  id: string;
  firstName: string;
  lastName: string;
  userName: string;
  pictureUrl: string;
  followersCount: number;
  followingCount: number;
  posts: IProfilePost[];
}

export interface IProfilePost {
  id: string;
  image: string;
}
