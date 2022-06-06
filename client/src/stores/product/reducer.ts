import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
  IDelProduct, IGetProdResp,
  IPostProduct,
  IPutProduct
} from '../../types/product';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';

export interface IProductState {
  dataState: IGridDataStateFilter,
  product: Nullable<IGetProdResp>,
  isProductLoading: boolean,
}

const initialState: IProductState = {
  product: null,
  isProductLoading: false,
  dataState: {
    filter: {
      filters: [] as IGridFilterSetting[],
      logic: ''
    } as IGridDataFilter,
    group: '',
    take: 10,
    skip: 0,
    sort: [{field: 'name', dir: 'asc'}]
  },
}

const productSlice = createSlice({
  name: "product",
  initialState: initialState,
  reducers: {
    getProduct: (state, action: PayloadAction) => {
    },
    postProduct: (state, action: PayloadAction<IPostProduct>) => {
    },
    putProduct: (state, action: PayloadAction<IPutProduct>) => {
    },
    delProduct: (state, action: PayloadAction<IDelProduct>) => {
    },
    setIsProductLoading: (state, action: PayloadAction<boolean>) => {
      state.isProductLoading = action.payload
    },
    setFilter: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
    setProduct: (state, action: PayloadAction<Nullable<IGetProdResp>>) => {
      state.product = action.payload
    }
  }
});

export const {setFilter, setProduct, getProduct, postProduct, putProduct, delProduct, setIsProductLoading} = productSlice.actions

export default productSlice.reducer