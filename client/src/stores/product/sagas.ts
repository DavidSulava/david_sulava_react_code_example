import { all, call, delay, put, select, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { setError } from '../common/reducer';
import {
  delProduct,
  getProductById,
  getProductList,
  postProduct,
  putProduct,
  setIsProductLoading,
  setProduct,
  setProductList
} from './reducer';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import { PayloadAction } from '@reduxjs/toolkit';
import { IDelProduct, IPostProduct, IPutProduct } from '../../types/product';

function* getProductListSaga(): any {
  try {
    const dataState = yield select((state: IState) => state.product.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})

    yield put(setIsProductLoading(true))
    yield* call(delay, 300);
    const productArray = yield* call(Api.getProductList, dataString)
    yield put(setProductList(productArray))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsProductLoading(false))
  }
}

function* getProductSaga({payload: id}: PayloadAction<string>) {
  try {

    yield put(setIsProductLoading(true))
    yield* call(delay, 500);
    const product= yield* call(Api.getProduct, id)
    yield put(setProduct(product))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsProductLoading(false))
  }
}

function* postProductSaga({payload: formData}: PayloadAction<IPostProduct>) {
  try {
    yield* call(Api.postProduct, formData)
    yield put(getProductList())
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* putProductSaga({payload: formData}: PayloadAction<IPutProduct>) {
  try {
    yield* call(Api.putProduct, formData)
    yield put(getProductList())
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* delProductSaga({payload: {prodId, organisationId}}: PayloadAction<IDelProduct>) {
  try {
    const param = `productId=${prodId}`
    yield* call(Api.delProduct, param)
    if(organisationId)
      yield put(getProductList())
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* productWatcher() {
  yield all([
    takeLatest(getProductList.type, getProductListSaga),
    takeLatest(getProductById.type, getProductSaga),
    takeLatest(postProduct.type, postProductSaga),
    takeLatest(putProduct.type, putProductSaga),
    takeLatest(delProduct.type, delProductSaga),
  ])
}

export default productWatcher