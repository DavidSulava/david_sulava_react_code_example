import { all, takeLatest, call, put, delay } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { setLocalStorage } from '../../helpers/localStorage';
import { ACCESS_TOKEN_KEY, ISignInData, ISignUpData } from '../../types/user';
import { setError, setPostReqResp } from '../common/reducer';
import { authOrg, getTariff, setIsUserLoading, setTariff, setUser, signIn, signUp } from './reducer';
import { PayloadAction } from '@reduxjs/toolkit';
import axios, { AxiosError } from 'axios';

function* signInSaga({payload:formData}: PayloadAction<ISignInData>) {
  try {
    yield put(setIsUserLoading(true))
    yield* call(delay, 300);
    const response = yield* call(Api.signIn, formData)
    yield put(setUser(response))
    setLocalStorage(ACCESS_TOKEN_KEY, response.token)
    yield put(getTariff())
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsUserLoading(false))
  }
}

function* signUpSaga({payload:formData}: PayloadAction<ISignUpData>) {
  try {
    yield put(setIsUserLoading(true))
    yield* call(delay, 300);
    const response = yield* call(Api.signUp, formData)
    yield put(setPostReqResp(response))
  }
  catch(e: any) {
    if (axios.isAxiosError(e)){
      const error = e as AxiosError
      let msg =  error.response?.data as any
      yield put(setError(msg?.message || error.message))
      return
    }

    yield put(setError(e?.message))
  }
  finally {
    yield put(setIsUserLoading(false))
  }
}

function* authOrgSaga({payload:orgId}: PayloadAction<string>) {
  try {
    const response = yield* call(Api.authOrg, orgId)
    setLocalStorage(ACCESS_TOKEN_KEY, response.token)
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* getTariffSaga() {
  try {
    const response = yield* call(Api.getTariff)
    yield put(setTariff(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* authWatcher() {
  yield all([
    takeLatest(signIn.type, signInSaga),
    takeLatest(signUp.type, signUpSaga),
    takeLatest(authOrg.type, authOrgSaga),
    takeLatest(getTariff.type, getTariffSaga),
  ])
}

export default authWatcher