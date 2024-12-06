export interface IUsers {
  userOne: string;
}
export interface IAuth {
  signup: string;
}
export class Images {
  public static readonly mainLogo: string = 'images/logo/my-logo.png';
  public static readonly bannerLogo: string = 'images/logo/login.png';

  public static readonly auth: IAuth = {
    signup: 'images/authpage/signup.jpg',
  };

  public static readonly users: IUsers = {
    userOne: 'images/authpage/profile-image.jpg',
  };
}
