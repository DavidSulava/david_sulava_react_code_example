import { all, call, delay, put, select, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { setError } from '../common/reducer';
import { delProduct, getProduct, postProduct, putProduct, setIsProductLoading, setProduct } from './reducer';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import { PayloadAction } from '@reduxjs/toolkit';
import { IDelProduct, IPostProduct, IPutProduct } from '../../types/product';

function* getProductSaga({payload: organizationId}: PayloadAction<string>): any {
  try {
    const dataState = yield select((state: IState) => state.product.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})
    const orgId = `&organizationId=${organizationId}`

    yield put(setIsProductLoading(true))
    yield* call(delay, 300);
    const productArray = yield* call(Api.getProduct, dataString + orgId)
    yield put(setProduct(productArray))
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
    yield put(getProduct(formData.organizationId))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* putProductSaga({payload: formData}: PayloadAction<IPutProduct>) {
  try {
    yield* call(Api.putProduct, formData)
    yield put(getProduct(formData.organizationId))
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
      yield put(getProduct(organisationId))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* productWatcher() {
  yield all([
    takeLatest(getProduct.type, getProductSaga),
    takeLatest(postProduct.type, postProductSaga),
    takeLatest(putProduct.type, putProductSaga),
    takeLatest(delProduct.type, delProductSaga),
  ])
}

export default productWatcher