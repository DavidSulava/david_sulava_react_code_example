import { ITariff, IUser, Nullable } from "../../types/user";
import { IAuthActions } from "./actions";
import { SET_IS_USER_LOADING, SET_TARIFF, SET_USER } from './constants';

export interface IAuthState {
  user: Nullable<IUser>,
  tariff: Nullable<ITariff[]>,
  isUserLoading: boolean,
}

const initialState: IAuthState = {
  user: null,
  tariff: null,
  isUserLoading: false
}

export const authReducer = (
  state: IAuthState = initialState,
  action: IAuthActions
) => {
  switch(action.type) {
    case SET_USER:
      return {
        ...state,
        user: action.payload
      }
    case SET_TARIFF:
      return {
        ...state,
        tariff: action.payload
      }
    case SET_IS_USER_LOADING:
      return {
        ...state,
        isUserLoading: action.payload
      }
    default:
      return state
  }
}