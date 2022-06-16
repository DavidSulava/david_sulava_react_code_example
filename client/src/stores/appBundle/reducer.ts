import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAppBundle, IGetAppBundleResp, IPostAppBundle, IPuttAppBundle } from '../../types/appBundle';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';

export interface IAppBundleState {
  isBundleListLoading: boolean,
  isBundleLoading: boolean,
  appBundle: Nullable<IAppBundle>,
  appBundleList: Nullable<IGetAppBundleResp>,
  appBundleTableList:  Nullable<IGetAppBundleResp>,
  dataState: IGridDataStateFilter,
}

export const initialAppBundleState: IAppBundleState = {
  isBundleListLoading: false,
  isBundleLoading: false,
  appBundle: null,
  appBundleList: null,
  appBundleTableList: null,
  dataState:{
    filter: {
      filters: [] as IGridFilterSetting[],
      logic: ''
    } as IGridDataFilter,
    group: '',
    take: 5,
    skip: 0,
    sort: [{field: 'name', dir: 'desc'}]
  }
}

const appBundleSlice = createSlice({
  name: "appBundle",
  initialState: initialAppBundleState,
  reducers: {
    getAppBundleById: (state, action: PayloadAction<string>) => {
    },
    getAppBundleList: (state, action: PayloadAction) => {
    },
    getAppBundleTableList: (state, action: PayloadAction) => {
    },
    postAppBundle: (state, action: PayloadAction<IPostAppBundle>) => {
    },
    putAppBundle: (state, action: PayloadAction<IPuttAppBundle>) => {
    },
    deleteAppBundle: (state, action: PayloadAction<string>) => {
    },
    setAppBundleList: (state, action: PayloadAction<Nullable<IGetAppBundleResp>>) => {
      state.appBundleList = action.payload
    },
    setAppBundle: (state, action: PayloadAction<Nullable<Nullable<IAppBundle>>>) => {
      state.appBundle = action.payload
    },
    setAppBundleTableList: (state, action: PayloadAction<Nullable<IGetAppBundleResp>>) => {
      state.appBundleTableList = action.payload
    },
    setAppBundleListIsLoading: (state, action: PayloadAction<boolean>) => {
      state.isBundleListLoading = action.payload
    },
    setAppBundleIsLoading: (state, action: PayloadAction<boolean>) => {
      state.isBundleLoading = action.payload
    },
    setAppBundleDataState: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
  }
});

export const {
  getAppBundleById,
  postAppBundle,
  putAppBundle,
  getAppBundleList,
  getAppBundleTableList,
  deleteAppBundle,
  setAppBundle,
  setAppBundleList,
  setAppBundleTableList,
  setAppBundleListIsLoading,
  setAppBundleIsLoading,
  setAppBundleDataState,
} = appBundleSlice.actions

export default appBundleSlice.reducer