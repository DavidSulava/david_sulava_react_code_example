export interface IUser {
  id: string,
  firstName: string,
  lastName: string,
  email: string,
  phone: string,
  token: string,
  role?: string
}

export interface ISignInData {
  email: string,
  password: string,
}

export interface ISignUpData {
  email: string,
  password: string,
  firstName: string,
  lastName: string,
  phone: string,
  role: eUserRoles
}

export interface ITariff {
  id: string,
  name: string
}

export const ACCESS_TOKEN_KEY = 'accessToken'

export enum eUserRoles {
  User = 0,
  Admin = 1,
  SuperAdmin = 2,
}