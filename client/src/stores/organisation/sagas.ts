import { all, call, put, takeLatest } from 'typed-redux-saga';
import Api from '../../services/api/api';
import { setError } from '../common/reducer';
import { getOrganisations, isLoadingOrganisation, postOrganisation, saveOrganisations } from './reducer';
import { PayloadAction } from '@reduxjs/toolkit';
import { IPostOrganisation } from '../../types/OrganisationPage';

function* getOrganisationsSaga({payload: userId}: PayloadAction<string>) {
  try {
    yield put(isLoadingOrganisation(true))
    const response = yield* call(Api.getOrganisations, userId)
    yield put(saveOrganisations(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(isLoadingOrganisation(false))
  }
}

function* postOrganisationSaga({payload: organisation}: PayloadAction<IPostOrganisation>) {
  try {
    yield put(isLoadingOrganisation(true))
    yield* call(Api.postOrganisation, organisation)
    yield put(getOrganisations(organisation.userId))
    put(getOrganisations(organisation.userId))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(isLoadingOrganisation(false))
  }
}

function* organisationWatcher() {
  yield all([
    takeLatest(getOrganisations.type, getOrganisationsSaga),
    takeLatest(postOrganisation.type, postOrganisationSaga)
  ])
}

export default organisationWatcher