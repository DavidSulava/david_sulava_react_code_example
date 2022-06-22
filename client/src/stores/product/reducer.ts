import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import {
  IDelProduct, IGetProdListResp,
  IPostProduct, IProduct,
  IPutProduct
} from '../../types/product';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';

export interface IProductState {
  dataState: IGridDataStateFilter,
  productList: Nullable<IGetProdListResp>,
  product: Nullable<IProduct>,
  isProductListLoading: boolean,
  isProductLoading: boolean,
}

export const initialProductState: IProductState = {
  productList: null,
  product: null,
  isProductListLoading: false,
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
  initialState: initialProductState,
  reducers: {
    getProductList: (state, action: PayloadAction) => {
    },
    getProductById: (state, action: PayloadAction<string>) => {
    },
    postProduct: (state, action: PayloadAction<IPostProduct>) => {
    },
    putProduct: (state, action: PayloadAction<IPutProduct>) => {
    },
    delProduct: (state, action: PayloadAction<IDelProduct>) => {
    },
    setIsProductListLoading: (state, action: PayloadAction<boolean>) => {
      state.isProductListLoading = action.payload
    },
    setIsProductLoading: (state, action: PayloadAction<boolean>) => {
      state.isProductLoading = action.payload
    },
    setProductFilter: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
    setProductList: (state, action: PayloadAction<Nullable<IGetProdListResp>>) => {
      state.productList = action.payload
    },
    setProduct: (state, action: PayloadAction<IProduct>) => {
      state.product = action.payload
    }
  }
});

export const {
  setProductFilter,
  setProductList,
  setProduct,
  getProductList,
  getProductById,
  postProduct,
  putProduct,
  delProduct,
  setIsProductListLoading,
  setIsProductLoading,
} = productSlice.actions

export default productSlice.reducer