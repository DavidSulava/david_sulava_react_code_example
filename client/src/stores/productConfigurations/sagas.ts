import { all, call, put, select, takeLatest, throttle } from 'typed-redux-saga';
import { PayloadAction } from '@reduxjs/toolkit';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import Api from '../../services/api/api';
import { setError } from '../common/reducer';
import {
  getConfigParams,
  getConfigurations,
  searchConfiguration,
  setConfigParams,
  setConfigurationsList,
  setIsConfigLoading, setSearchedConfigList
} from './reducer';
import { ISearchConfigPayload } from '../../types/producVersionConfigurations';
import { IGridFilterSetting } from '../../types/common';

function* getConfigurationsSaga({payload: productVersionId}: PayloadAction<string>): any {
  try {
    const dataState = yield select((state: IState) => state.configurations.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})
    const idParam = `&productVersionId=${productVersionId}`
    yield put(setIsConfigLoading(true))
    const response = yield* call(Api.getConfigurations, dataString + idParam)
    yield put(setConfigurationsList(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsConfigLoading(false))
  }
}
function* searchConfigurationsSaga({payload: {id, value, skip, take}}: PayloadAction<ISearchConfigPayload>): any {
  try {
    const dataState = yield select((state: IState) => state.configurations.dataState)
    const searchDataState = {
      ...dataState,
      filter: {
        filters: [{value: value, operator: 'contains', field:'configurationName'}] as IGridFilterSetting[],
        logic: ''
      },
      skip: skip?? dataState.skip,
      take: take?? dataState.take
    }
    const dataString: string = toDataSourceRequestString({...searchDataState})
    const idParam = `&productVersionId=${id}`
    yield put(setIsConfigLoading(true))
    const response = yield* call(Api.getConfigurations, dataString + idParam)
    yield put(setSearchedConfigList(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsConfigLoading(false))
  }
}
function* getConfigParamsSaga({payload: productVersionId}: PayloadAction<string>) {
  try {
    const response = yield* call(Api.getConfigParams,  productVersionId)
    yield put(setConfigParams(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* configurationsWatcher() {
  yield all([
    takeLatest(getConfigurations.type, getConfigurationsSaga),
    takeLatest(getConfigParams.type, getConfigParamsSaga),
    throttle(1000, searchConfiguration.type, searchConfigurationsSaga)
  ])
}

export default configurationsWatcher