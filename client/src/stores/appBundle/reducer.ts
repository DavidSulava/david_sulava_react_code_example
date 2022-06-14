import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAppBundle } from '../../types/appBundle';

export interface IAppBundleState {
  isBundleListLoading: boolean,
  isBundleLoading: boolean,
  appBundleList: IAppBundle[],
}

export const initialAppBundleState: IAppBundleState = {
  isBundleListLoading: false,
  isBundleLoading: false,
  appBundleList: [],
}

const appBundleSlice = createSlice({
  name: "appBundle",
  initialState: initialAppBundleState,
  reducers: {
    getAppBundleList: (state, action: PayloadAction) => {
    },
    setAppBundleList: (state, action: PayloadAction<IAppBundle[]>) => {
      state.appBundleList = action.payload
    },
    setAppBundleListIsLoading: (state, action: PayloadAction<boolean>) => {
      state.isBundleListLoading = action.payload
    },
  }
});

export const {
  getAppBundleList,
  setAppBundleList,
  setAppBundleListIsLoading,
} = appBundleSlice.actions

export default appBundleSlice.reducer