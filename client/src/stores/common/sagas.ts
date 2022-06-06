import { all, call, put, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { getAppBundle, setAppBundle, setError } from './reducer';

function* getAppBundleSaga() {
  try {
    const bundleArray = yield* call(Api.getAppBundle)
    yield put(setAppBundle(bundleArray))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* commonWatcher() {
  yield all([
    takeLatest(getAppBundle.type, getAppBundleSaga)
  ])
}

export default commonWatcher