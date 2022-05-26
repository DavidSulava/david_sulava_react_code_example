import { all, call, put, select, takeLatest } from 'typed-redux-saga';
import { setError } from '../common/reducer';
import Api from '../../services/api/api';
import { PayloadAction } from '@reduxjs/toolkit';
import { getProdVerByProdId, setProdVersions, setProdVersionLoading, postProdVerByProdId, delProdVer, putProdVer } from './reducer';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import { IPostProductVersion, IProductVersion } from '../../types/productVersion';

function* getProdVersionSaga({payload: id}: PayloadAction<string>):any {
  try {
    const dataState = yield select((state: IState) => state.prodVersion.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})
    const idParam = `&productId=${id}`
    yield put(setProdVersionLoading(true))
    const response = yield* call(Api.getProdVersion,  dataString + idParam)
    yield put(setProdVersions(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setProdVersionLoading(false))
  }
}

function* postProdVersionSaga({payload: formData}: PayloadAction<IPostProductVersion>) {
  try {
    yield* call(Api.postProdVersion,  formData)
    const productId = formData.get('ProductId')
    yield* put(getProdVerByProdId(productId as string))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* puttProdVersionSaga({payload: formData}: PayloadAction<IPostProductVersion>) {
  try {
    yield* call(Api.putProdVersion,  formData)
    const productId = formData.get('ProductId')
    yield* put(getProdVerByProdId(productId as string))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* delProdVersionSaga({payload: product}: PayloadAction<IProductVersion>) {
  try {
    const param = `id=${product.id}`
    yield* call(Api.delProductVersion,  param)

    yield* put(getProdVerByProdId(product.productId))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* prodVersionWatcher() {
  yield all([
    takeLatest(getProdVerByProdId.type, getProdVersionSaga),
    takeLatest(postProdVerByProdId.type, postProdVersionSaga),
    takeLatest(putProdVer.type, puttProdVersionSaga),
    takeLatest(delProdVer.type, delProdVersionSaga),
  ])
}

export default prodVersionWatcher