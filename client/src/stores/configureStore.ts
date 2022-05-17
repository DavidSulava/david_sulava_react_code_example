import { all } from "redux-saga/effects";
import { authReducer, IAuthState } from "./authentication/reducer";
import authWatcher from './authentication/sagas';
import organisationWatcher from './organisation/sagas';
import createSagaMiddleware from "redux-saga";
import { createStore, combineReducers, applyMiddleware, Store } from 'redux'
import { composeWithDevTools } from 'redux-devtools-extension'
import { commonReducer, ICommonState } from './common/reducer';
import { IOrganisationState, organisationReducer } from './organisation/reducer';
import commonWatcher from './common/sagas';

export interface IState {
  auth: IAuthState,
  common: ICommonState,
  organisation: IOrganisationState,
}

const reducers = combineReducers<IState>({
  auth: authReducer,
  common: commonReducer,
  organisation: organisationReducer,
})

function* sagas() {
  yield all([
    authWatcher(),
    commonWatcher(),
    organisationWatcher(),
  ])
}

export function configureStore(): Store<IState> {
  const sagaMiddleware = createSagaMiddleware()
  const store = createStore(reducers, composeWithDevTools(applyMiddleware(sagaMiddleware)))
  sagaMiddleware.run(sagas)
  return store
}