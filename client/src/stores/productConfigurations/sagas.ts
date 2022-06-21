import { all, call, delay, put, select, takeLatest, throttle } from 'typed-redux-saga';
import { PayloadAction } from '@reduxjs/toolkit';
import { IState } from '../configureStore';
import { toDataSourceRequestString } from '@progress/kendo-data-query';
import Api from '../../services/api/api';
import { setError, setPostReqResp } from '../common/reducer';
import {
  getConfigParams, getConfigurationById,
  getConfigurationList, getSvfPath, postConfig,
  searchConfiguration,
  setConfigParams, setConfiguration,
  setConfigurationsList,
  setIsConfigLoading, setIsParamsLoading, setSearchedConfigList, setSvfPath
} from './reducer';
import { IPostConfigurations, ISearchConfigPayload } from '../../types/producVersionConfigurations';
import { IGridFilterSetting } from '../../types/common';

function* getConfigurationListSaga({payload: productVersionId}: PayloadAction<string>): any {
  try {
    const dataState = yield select((state: IState) => state.configurations.dataState)
    const dataString: string = toDataSourceRequestString({...dataState})
    const idParam = `&productVersionId=${productVersionId}`
    yield put(setIsConfigLoading(true))
    const response = yield* call(Api.getConfigurationList, dataString + idParam)
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
        filters: [{value: value, operator: 'contains', field: 'configurationName'}] as IGridFilterSetting[],
        logic: ''
      },
      skip: skip ?? dataState.skip,
      take: take ?? dataState.take
    }
    const dataString: string = toDataSourceRequestString({...searchDataState})
    const idParam = `&productVersionId=${id}`
    yield put(setIsConfigLoading(true))
    const response = yield* call(Api.getConfigurationList, dataString + idParam)
    yield put(setSearchedConfigList(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsConfigLoading(false))
  }
}

function* getConfigurationByIdSaga({payload: configId}: PayloadAction<string>) {
  try {
    yield put(setIsConfigLoading(true))
    const response = yield* call(Api.getConfigurationById, configId)
    yield* call(getConfigParamsSaga, {payload: configId, type: getConfigParams.type})
    yield put(setConfiguration(response))
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
    yield put(setIsParamsLoading(true))
    const response = yield* call(Api.getConfigParams, productVersionId)
    yield* call(delay, 500)
    yield put(setConfigParams(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
  finally {
    yield put(setIsParamsLoading(false))
  }
}

function* getSvfPathSaga({payload: configId}: PayloadAction<string>) {
  try {
    const response = yield* call(Api.getSvfPath, configId)
    yield put(setSvfPath(response))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* postConfigSaga({payload: formData}: PayloadAction<IPostConfigurations>) {
  try {
    const responce = yield* call(Api.postConfig, formData)

    if(responce)
      yield put(setPostReqResp(responce))
    else
      yield put(setPostReqResp(null))
  }
  catch(e: any) {
    yield put(setError(e))
  }
}

function* configurationsWatcher() {
  yield all([
    takeLatest(getConfigurationList.type, getConfigurationListSaga),
    takeLatest(getConfigurationById.type, getConfigurationByIdSaga),
    takeLatest(getConfigParams.type, getConfigParamsSaga),
    throttle(1000, searchConfiguration.type, searchConfigurationsSaga),
    takeLatest(getSvfPath.type, getSvfPathSaga),
    takeLatest(postConfig.type, postConfigSaga),
  ])
}

export default configurationsWatcher