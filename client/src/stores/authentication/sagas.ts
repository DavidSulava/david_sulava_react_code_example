import { all, takeLatest, call, put, delay } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { setLocalStorage } from '../../helpers/localStorage';
import { ACCESS_TOKEN_KEY } from '../../types/user';
import { setError } from '../common/reducer';
import { getTariff, setIsUserLoading, setTariff, setUser, signIn } from './reducer';
import { PayloadAction } from '@reduxjs/toolkit';
import { ISignInData } from '../../types/LoginPage';

function* signInSaga({payload:formData}: PayloadAction<ISignInData>) {
  try {
    yield put(setIsUserLoading(true))
    yield* call(delay, 300);
    const response = yield* call(Api.signIn, formData)
    yield put(setUser(response))
    setLocalStorage(ACCESS_TOKEN_KEY, response.token)
    yield put(getTariff())
  }
  catch(e) {
    yield put(setError(e as string))
  }
  finally {
    yield put(setIsUserLoading(false))
  }
}

function* getTariffSaga() {
  try {
    const response = yield* call(Api.getTariff)
    yield put(setTariff(response))
  }
  catch(e) {
    yield put(setError(e as string))
  }
}

function* authWatcher() {
  yield all([
    takeLatest(signIn.type, signInSaga),
    takeLatest(getTariff.type, getTariffSaga),
  ])
}

export default authWatcher