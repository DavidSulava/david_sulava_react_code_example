import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';
import {
  IConfiguration,
  IConfigurationParamData,
  IGetConfigurations,
  IPostConfigurations,
  ISearchConfigPayload
} from '../../types/producVersionConfigurations';

export interface IConfigurationsState {
  isConfigLoading: boolean,
  isParamsLoading: boolean,
  configurationsList: Nullable<IGetConfigurations>,
  searchedConfigList: Nullable<IGetConfigurations>,
  configuration: Nullable<IConfiguration>,
  configParams:  Nullable<IConfigurationParamData>,
  svfPath: string,
  dataState: IGridDataStateFilter,
}

export const initialConfigurationsState: IConfigurationsState = {
  isConfigLoading: false,
  isParamsLoading: false,
  configurationsList: null,
  searchedConfigList: null,
  configuration: null,
  configParams: null,
  svfPath: '',
  dataState:{
    filter: {
      filters: [] as IGridFilterSetting[],
      logic: ''
    } as IGridDataFilter,
    group: '',
    take: 5,
    skip: 0,
    sort: [{field: 'created', dir: 'desc'}]
  }
}

const configurationsSlice = createSlice({
  name: "configurations",
  initialState: initialConfigurationsState,
  reducers: {
    getConfigurationList: (state, action: PayloadAction<string>) => {
    },
    getConfigurationById: (state, action: PayloadAction<string>) => {
    },
    searchConfiguration: (state, action: PayloadAction<ISearchConfigPayload>) => {
    },
    getConfigParams: (state, action: PayloadAction<string>) => {
    },
    getSvfPath: (state, action: PayloadAction<string>) => {
    },
    postConfig: (state, action: PayloadAction<IPostConfigurations>) => {
    },
    setConfigParams: (state, action: PayloadAction<IConfigurationParamData>) => {
      state.configParams = action.payload
    },
    setConfigurationsList: (state, action: PayloadAction<IGetConfigurations>) => {
      state.configurationsList = action.payload
    },
    setConfiguration: (state, action: PayloadAction<IConfiguration|null>) => {
      state.configuration = action.payload
    },
    setSearchedConfigList: (state, action: PayloadAction<IGetConfigurations>) => {
      state.searchedConfigList = action.payload
    },
    setSvfPath:  (state, action: PayloadAction<string>) => {
      state.svfPath = action.payload
    },
    setConfigurationsDataState: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
    setIsConfigLoading: (state, action: PayloadAction<boolean>) => {
      state.isConfigLoading = action.payload
    },
    setIsParamsLoading: (state, action: PayloadAction<boolean>) => {
      state.isParamsLoading = action.payload
    },
  }
});

export const {
  getConfigurationList,
  getConfigurationById,
  getConfigParams,
  searchConfiguration,
  getSvfPath,
  postConfig,
  setConfiguration,
  setConfigParams,
  setConfigurationsList,
  setSearchedConfigList,
  setSvfPath,
  setConfigurationsDataState,
  setIsConfigLoading,
  setIsParamsLoading
} = configurationsSlice.actions

export default configurationsSlice.reducer