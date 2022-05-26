import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';
import { IGetProductVersion, IPostProductVersion, IProductVersion } from '../../types/productVersion';

export interface IProdVersionState {
  isProdVersionLoading: boolean,
  prodVersions: Nullable<IGetProductVersion>,
  dataState: IGridDataStateFilter,
}

export const initialProdVerState: IProdVersionState = {
  isProdVersionLoading: false,
  prodVersions: null,
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
    getProdVerByProdId: (state, action: PayloadAction<string>) => {
    },
    postProdVerByProdId: (state, action: PayloadAction<IPostProductVersion>) => {
    },
    putProdVer: (state, action: PayloadAction<IPostProductVersion>) => {
    },
    delProdVer: (state, action: PayloadAction<IProductVersion>) => {
    },
    setProdVersions: (state, action: PayloadAction<IGetProductVersion>) => {
      state.prodVersions = action.payload
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
  getProdVerByProdId,
  postProdVerByProdId,
  delProdVer,
  putProdVer,
  setProdVersions,
  setProdVersionLoading,
  setProdVersionDataState
} = prodVersionSlice.actions

export default prodVersionSlice.reducer