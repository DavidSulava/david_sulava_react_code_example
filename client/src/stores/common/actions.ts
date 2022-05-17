import { SET_ERROR } from './constants';

export interface ISetError {
  type: typeof SET_ERROR,
  payload: string
}

export type ICommonActions = ISetError

export class CommonActions {
  public static setError(error: string): ISetError {
    return {
      type: SET_ERROR,
      payload: error,
    }

  }
}