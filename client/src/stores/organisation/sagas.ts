import { all, call, put, takeLatest } from 'typed-redux-saga';
import { IGetOrganisation, IPostOrganisationAction, OrganisationActions } from './actions';
import { CommonActions } from '../common/actions';
import { GET_ORGANISATIONS, POST_ORGANISATION } from './constants';
import Api from '../../services/api/api';

function* getOrganisations({userId}: IGetOrganisation) {
  try {
    yield put(OrganisationActions.isLoadingOrganisation(true))
    const response = yield* call(Api.getOrganisations, userId)
    yield put(OrganisationActions.saveOrganisations(response))
  }
  catch(e) {
    yield put(CommonActions.setError(e as string))
  }
  finally {
    yield put(OrganisationActions.isLoadingOrganisation(false))
  }
}
function* postOrganisation({organisation}: IPostOrganisationAction) {
  try {
    yield put(OrganisationActions.isLoadingOrganisation(true))
    yield* call(Api.postOrganisation, organisation)
    yield put(OrganisationActions.getOrganisations(organisation.userId))
  }
  catch(e) {
    yield put(CommonActions.setError(e as string))
  }
  finally {
    yield put(OrganisationActions.isLoadingOrganisation(false))
  }
}

function* organisationWatcher(){
  yield all([
    takeLatest(GET_ORGANISATIONS, getOrganisations),
    takeLatest(POST_ORGANISATION, postOrganisation)
  ])
}

export default organisationWatcher