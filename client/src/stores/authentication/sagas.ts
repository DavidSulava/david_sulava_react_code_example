import { GET_TARIFF, SIGN_IN } from './constants';
import { all, takeLatest, call, put, delay } from 'typed-redux-saga';
import { AuthenticationActions, ISignInAction } from './actions';
import Api from '../../services/api/api';
import { CommonActions } from '../common/actions';
import { setLocalStorage } from '../../helpers/localStorage';
import { ACCESS_TOKEN_KEY } from '../../types/user';

function* signIn({formData}: ISignInAction) {
  try {
    yield put(AuthenticationActions.setIsUserLoading(true))
    yield* call(delay, 300);
    const response = yield* call(Api.signIn, formData)
    yield put(AuthenticationActions.setUser(response))
    setLocalStorage(ACCESS_TOKEN_KEY, response.token)
    yield put(AuthenticationActions.getTariff())
  }
  catch(e) {
    yield put(CommonActions.setError(e as string))
  }
  finally {
    yield put(AuthenticationActions.setIsUserLoading(false))
  }
}
function* getTariff() {
  try {
    const response = yield* call(Api.getTariff)
    yield put(AuthenticationActions.setTariff(response))
  }
  catch(e) {
    yield put(CommonActions.setError(e as string))
  }
}

function* authWatcher() {
  yield all([
    takeLatest(SIGN_IN, signIn),
    takeLatest(GET_TARIFF, getTariff),
  ])
}

export default authWatcher