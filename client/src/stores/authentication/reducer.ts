import { ITariff, IUser, Nullable } from "../../types/user";
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ISignInData } from '../../types/LoginPage';

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

const authSlice = createSlice({
  name: "auth",
  initialState: initialState,
  reducers: {
    signIn: (state, action: PayloadAction<ISignInData>) => {},
    getTariff: (state, action: PayloadAction) => {},
    setUser: (state, action: PayloadAction<Nullable<IUser>>) => {
      state.user = action.payload
    },
    setIsUserLoading: (state, action: PayloadAction<boolean>) => {
      state.isUserLoading = action.payload
    },
    setTariff: (state, action: PayloadAction<Nullable<ITariff[]>>) => {
      state.tariff = action.payload
    }
  }
});

export const {setUser, setIsUserLoading, setTariff, signIn, getTariff} = authSlice.actions

export default authSlice.reducer