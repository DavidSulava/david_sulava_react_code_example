import { GET_TARIFF, SET_IS_USER_LOADING, SET_TARIFF, SET_USER, SIGN_IN } from './constants';
import { ISignInData } from '../../types/LoginPage';
import { ITariff, IUser, Nullable } from '../../types/user';

export interface ISignInAction {
  type: typeof SIGN_IN,
  formData: ISignInData
}

export interface ISetUserAction {
  type: typeof SET_USER,
  payload: Nullable<IUser>
}

export interface IGetTariffAction {
  type: typeof GET_TARIFF,
}

export interface ISetTariffAction {
  type: typeof SET_TARIFF,
  payload: ITariff[]
}

export interface IIsUserLoadingAction {
  type: typeof SET_IS_USER_LOADING,
  payload: boolean
}

export type IAuthActions = ISetUserAction
|IIsUserLoadingAction
|ISetTariffAction

export class AuthenticationActions {
  public static signIn(formData: ISignInData): ISignInAction {
    return {
      type: SIGN_IN,
      formData: formData,
    }
  }

  public static setUser(userData: Nullable<IUser>): ISetUserAction {
    return {
      type: SET_USER,
      payload: userData,
    }
  }

  public static setIsUserLoading(isLoading: boolean): IIsUserLoadingAction {
    return {
      type: SET_IS_USER_LOADING,
      payload: isLoading,
    }
  }

  public static getTariff(): IGetTariffAction {
    return {
      type: GET_TARIFF,
    }
  }

  public static setTariff(tariffData: ITariff[]): ISetTariffAction {
    return {
      type: SET_TARIFF,
      payload: tariffData
    }
  }
}