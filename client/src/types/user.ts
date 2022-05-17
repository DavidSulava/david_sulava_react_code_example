export interface IUser {
  id: string,
  firstName: string,
  lastName: string,
  email: string,
  token: string,
}

export interface ITariff {
  id: string,
  name: string
}

export type Nullable<T> = T|undefined|null

export const ACCESS_TOKEN_KEY = 'accessToken'

