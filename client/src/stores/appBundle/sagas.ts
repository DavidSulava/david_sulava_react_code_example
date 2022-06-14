import { all, call, put, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { getAppBundleList, setAppBundleList, setAppBundleListIsLoading } from './reducer';
import { setError } from '../common/reducer';

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

function* appBundleWatcher() {
  yield all([
    takeLatest(getAppBundleList.type, getAppBundleListSaga)
  ])
}

export default appBundleWatcher