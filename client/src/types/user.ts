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
  role: EUserRoles
}

export interface ITariff {
  id: string,
  name: string
}

export const ACCESS_TOKEN_KEY = 'accessToken'

export enum EUserRoles {
  User = 0,
  Admin = 1,
  SuperAdmin = 2,
}