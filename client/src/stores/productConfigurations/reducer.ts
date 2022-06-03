import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IGridDataFilter, IGridDataStateFilter, IGridFilterSetting, Nullable } from '../../types/common';
import { IConfigurationParamData, IGetConfigurations, ISearchConfigPayload } from '../../types/producVersionConfigurations';

export interface IConfigurationsState {
  isConfigLoading: boolean,
  configurationsList: Nullable<IGetConfigurations>,
  searchedConfigList: Nullable<IGetConfigurations>,
  configParams:  Nullable<IConfigurationParamData>,
  dataState: IGridDataStateFilter,
}

export const initialConfigurationsState: IConfigurationsState = {
  isConfigLoading: false,
  configurationsList: null,
  searchedConfigList: null,
  configParams: null,
  dataState:{
    filter: {
      filters: [] as IGridFilterSetting[],
      logic: ''
    } as IGridDataFilter,
    group: '',
    take: 4,
    skip: 0,
    sort: [{field: 'created', dir: 'desc'}]
  }
}

const configurationsSlice = createSlice({
  name: "configurations",
  initialState: initialConfigurationsState,
  reducers: {
    getConfigurations: (state, action: PayloadAction<string>) => {
    },
    searchConfiguration: (state, action: PayloadAction<ISearchConfigPayload>) => {
    },
    getConfigParams: (state, action: PayloadAction<string>) => {
    },
    setConfigParams: (state, action: PayloadAction<IConfigurationParamData>) => {
      state.configParams = action.payload
    },
    setIsConfigLoading: (state, action: PayloadAction<boolean>) => {
      state.isConfigLoading = action.payload
    },
    setConfigurationsList: (state, action: PayloadAction<IGetConfigurations>) => {
      state.configurationsList = action.payload
    },
    setSearchedConfigList: (state, action: PayloadAction<IGetConfigurations>) => {
      state.searchedConfigList = action.payload
    },
    setConfigurationsDataState: (state, action: PayloadAction<IGridDataStateFilter>) => {
      state.dataState = action.payload
    },
  }
});

export const {
  getConfigurations,
  getConfigParams,
  searchConfiguration,
  setConfigParams,
  setIsConfigLoading,
  setConfigurationsList,
  setSearchedConfigList,
  setConfigurationsDataState
} = configurationsSlice.actions

export default configurationsSlice.reducer