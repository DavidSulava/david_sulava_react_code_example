import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';
import { IGetProductVersion, IPostProductVersion, IProductVersion } from '../../types/productVersion';

export interface IProdVersionState {
  isProdVersionLoading: boolean,
  prodVersionList: Nullable<IGetProductVersion>,
  prodVersion: Nullable<IProductVersion>,
  dataState: IGridDataStateFilter,
}

export const initialProdVerState: IProdVersionState = {
  isProdVersionLoading: false,
  prodVersionList: null,
  prodVersion: null,
  dataState: {
    filter: {
      filters: [] as IGridFilterSetting[],
      logic: ''
    } as IGridDataFilter,
    group: '',
    take: 8,
    skip: 0,
    sort: [{field: 'created', dir: 'desc'}]
  },
}

const prodVersionSlice = createSlice({
  name: "prodVersion",
  initialState: initialProdVerState,
  reducers: {
    getProdVersionList: (state, action: PayloadAction<string>) => {
    },
    getProdVersion: (state, action: PayloadAction<string>) => {
    },
    getConfigurations: (state, action: PayloadAction<string>) => {
    },
    postProdVerByProdId: (state, action: PayloadAction<IPostProductVersion>) => {
    },
    putProdVer: (state, action: PayloadAction<IPostProductVersion>) => {
    },
    delProdVer: (state, action: PayloadAction<IProductVersion>) => {
    },
    setProdVersionList: (state, action: PayloadAction<IGetProductVersion>) => {
      state.prodVersionList = action.payload
    },
    setProdVersion: (state, action: PayloadAction<IProductVersion>) => {
      state.prodVersion = action.payload
    },
    setProdVersionLoading: (state, action: PayloadAction<boolean>) => {
      state.isProdVersionLoading = action.payload
    },
    setProdVersionDataState: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
  }
});

export const {
  getProdVersionList,
  getProdVersion,
  postProdVerByProdId,
  delProdVer,
  putProdVer,
  setProdVersionList,
  setProdVersion,
  setProdVersionLoading,
  setProdVersionDataState
} = prodVersionSlice.actions

export default prodVersionSlice.reducer