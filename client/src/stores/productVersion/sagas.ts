import { all, call, delay, put, select, takeLatest } from 'typed-redux-saga';
import { setError } from '../common/reducer';
import Api from '../../services/api/api';
import { PayloadAction } from '@reduxjs/toolkit';
import {
  delProdVer,
  getProdVersion,
  getProdVersionList,
  postProdVerByProdId,
  putProdVer,
  setProdVersion,
  setProdVersionList, setProdVersionListLoading,
  setProdVersionLoading
} from './reducer';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import { IPostProductVersion, IProductVersion } from '../../types/productVersion';

function* getProdVersionListSaga({payload: id}: PayloadAction<string>):any {
  try {
    const dataState = yield select((state: IState) => state.prodVersion.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})
    const idParam = `&productId=${id}`
    yield put(setProdVersionListLoading(true))
    const response = yield* call(Api.getProdVersionList,  dataString + idParam)
    yield put(setProdVersionList(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setProdVersionListLoading(false))
  }
}
function* getProdVersionSaga({payload: id}: PayloadAction<string>):any {
  try {
    yield put(setProdVersionLoading(true))
    const response = yield* call(Api.getProdVersion,  id)
    yield* call(delay, 500);
    yield put(setProdVersion(response))
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
    yield* put(setProdVersionLoading(true))
    yield* call(Api.postProdVersion,  formData)
    const productId = formData.get('ProductId')
    yield* put(getProdVersionList(productId as string))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield* put(setProdVersionLoading(false))
  }
}

function* putProdVersionSaga({payload: formData}: PayloadAction<IPostProductVersion>) {
  try {
    yield* call(Api.putProdVersion,  formData)
    const productId = formData.get('ProductId')
    yield* put(getProdVersionList(productId as string))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* delProdVersionSaga({payload: product}: PayloadAction<IProductVersion>) {
  try {
    const param = `id=${product.id}`
    yield* call(Api.delProductVersion,  param)

    yield* put(getProdVersionList(product.productId))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* prodVersionWatcher() {
  yield all([
    takeLatest(getProdVersionList.type, getProdVersionListSaga),
    takeLatest(postProdVerByProdId.type, postProdVersionSaga),
    takeLatest(putProdVer.type, putProdVersionSaga),
    takeLatest(delProdVer.type, delProdVersionSaga),
    takeLatest(getProdVersion.type, getProdVersionSaga),
  ])
}

export default prodVersionWatcher