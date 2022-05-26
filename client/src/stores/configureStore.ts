import { all } from "redux-saga/effects";
import authSlice, { IAuthState } from "./authentication/reducer";
import authWatcher from './authentication/sagas';
import organisationWatcher from './organisation/sagas';
import createSagaMiddleware from "redux-saga";
import commonSlice, { ICommonState } from './common/reducer';
import organisationSlice, { IOrganisationState } from './organisation/reducer';
import productSlice, { IProductState } from './product/reducer';
import commonWatcher from './common/sagas';
import { combineReducers, configureStore } from '@reduxjs/toolkit';
import productWatcher from './product/sagas';
import prodVersionSlice, { IProdVersionState } from './productVersion/reducer';
import prodVersionWatcher from './productVersion/sagas';

export interface IState {
  auth: IAuthState,
  common: ICommonState,
  organisation: IOrganisationState,
  product: IProductState,
  prodVersion: IProdVersionState,
}

const reducers = combineReducers<IState>({
  auth: authSlice,
  common: commonSlice,
  organisation: organisationSlice,
  product: productSlice,
  prodVersion: prodVersionSlice,
})

function* sagas() {
  yield all([
    authWatcher(),
    commonWatcher(),
    organisationWatcher(),
    productWatcher(),
    prodVersionWatcher(),
  ])
}

const sagaMiddleware = createSagaMiddleware()

export const store = configureStore({
  reducer: reducers,
  middleware: (getDefaultMiddleware) => getDefaultMiddleware({thunk: false, serializableCheck: false}).concat(sagaMiddleware)
})

sagaMiddleware.run(sagas);