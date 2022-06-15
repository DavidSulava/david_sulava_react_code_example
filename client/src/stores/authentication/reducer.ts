import {
  IAccountInfo,
  IPostPasswordRecovery,
  IPostPasswordRecoveryConfirm,
  IPutAccountInfo,
  ISignInData,
  ISignUpData,
  ITariff,
  IUser
} from "../../types/user";
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Nullable } from '../../types/common';

export interface IAuthState {
  user: Nullable<IUser>,
  accountInfo: Nullable<IAccountInfo>,
  tariff: Nullable<ITariff[]>,
  isUserLoading: boolean,
}

const initialState: IAuthState = {
  user: null,
  accountInfo: null,
  tariff: null,
  isUserLoading: false,
}

const authSlice = createSlice({
  name: "auth",
  initialState: initialState,
  reducers: {
    signIn: (state, action: PayloadAction<ISignInData>) => {
    },
    signUp: (state, action: PayloadAction<ISignUpData>) => {
    },
    authOrg: (state, action: PayloadAction<string>) => {
    },
    getAccount: (state, action: PayloadAction) => {
    },
    getTariff: (state, action: PayloadAction) => {
    },
    putAccountInfo: (state, action: PayloadAction<IPutAccountInfo>) => {
    },
    postSendEmailToRestorePassword: (state, action: PayloadAction<IPostPasswordRecovery>) => {
    },
    postPasswordRecoveryConfirm: (state, action: PayloadAction<IPostPasswordRecoveryConfirm>) => {
    },
    setUser: (state, action: PayloadAction<Nullable<IUser>>) => {
      state.user = action.payload
    },
    setAccount: (state, action: PayloadAction<IAccountInfo|null>) => {
      state.accountInfo = action.payload
    },
    setIsUserLoading: (state, action: PayloadAction<boolean>) => {
      state.isUserLoading = action.payload
    },
    setTariff: (state, action: PayloadAction<Nullable<ITariff[]>>) => {
      state.tariff = action.payload
    }
  }
});

export const {
  signIn,
  signUp,
  authOrg,
  putAccountInfo,
  postSendEmailToRestorePassword,
  postPasswordRecoveryConfirm,
  getTariff,
  getAccount,
  setUser,
  setIsUserLoading,
  setTariff,
  setAccount,
} = authSlice.actions

export default authSlice.reducer