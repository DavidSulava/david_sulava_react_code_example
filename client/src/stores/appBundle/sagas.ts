import { all, call, delay, put, select, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import {
  deleteAppBundle,
  getAppBundleById,
  getAppBundleList, getAppBundleTableList,
  postAppBundle,
  putAppBundle, setAppBundle, setAppBundleIsLoading,
  setAppBundleList,
  setAppBundleListIsLoading, setAppBundleTableList
} from './reducer';
import { setError, setPostReqResp } from '../common/reducer';
import { PayloadAction } from '@reduxjs/toolkit';
import { IPostAppBundle, IPuttAppBundle } from '../../types/appBundle';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';

function* getAppBundleListSaga() {
  try {
    yield put(setAppBundleListIsLoading(true))
    const bundleArray = yield* call(Api.getAppBundle)
    yield put(setAppBundleList(bundleArray))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setAppBundleListIsLoading(false))
  }
}
function* getAppBundleTableListSaga(): any {
  try {
    const dataState = yield select((state: IState) => state.appBundle.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})

    yield put(setAppBundleListIsLoading(true))
    const bundleArray = yield* call(Api.getAppBundle, dataString)
    yield put(setAppBundleTableList(bundleArray))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setAppBundleListIsLoading(false))
  }
}

function* postAppBundleSaga({payload: formData}: PayloadAction<IPostAppBundle>) {
  try {
    yield put(setAppBundleIsLoading(true))
    const resp = yield* call(Api.postAppBundle, formData)
    yield* call(getAppBundleTableListSaga)
    yield put(setPostReqResp(resp))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setAppBundleIsLoading(false))
  }
}
function* putAppBundleSaga({payload: formData}: PayloadAction<IPuttAppBundle>) {
  try {
    yield put(setAppBundleIsLoading(true))
    yield* call(Api.putAppBundle, formData)
    yield* call(getAppBundleTableListSaga)
    yield put(setPostReqResp('200'))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setAppBundleIsLoading(false))
  }
}
function* deleteAppBundleSaga({payload: id}: PayloadAction<string>) {
  try {
    yield* call(Api.deleteAppBundle, id)
    yield* call(getAppBundleTableListSaga)
  }
  catch(e: any) {
    yield put(setError(e))
  }
}
function* getAppBundleByIdSaga({payload: id}: PayloadAction<string>) {
  try {
    yield put(setAppBundleIsLoading(true))
    const bundle = yield* call(Api.getAppBundleById, id)
    yield* call(delay, 500)
    yield put(setAppBundle(bundle))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setAppBundleIsLoading(false))
  }
}

function* appBundleWatcher() {
  yield all([
    takeLatest(getAppBundleById.type, getAppBundleByIdSaga),
    takeLatest(getAppBundleList.type, getAppBundleListSaga),
    takeLatest(getAppBundleTableList.type, getAppBundleTableListSaga),
    takeLatest(postAppBundle.type, postAppBundleSaga),
    takeLatest(putAppBundle.type, putAppBundleSaga),
    takeLatest(deleteAppBundle.type, deleteAppBundleSaga),
  ])
}

export default appBundleWatcher