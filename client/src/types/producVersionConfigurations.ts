import { IKendoResp } from './common';

export interface IGetConfigurations extends IKendoResp {
  data: IConfigurationListItem[],
}
export interface IConfiguration{
  id: string,
  name: string,
  componentName: string,
  comment: string,
  modelState: EModelState,
  productVersionId: string,
  targetFileId: string,
  parentId: string,
  created: string,
  status: EConfigurationStatus,
  svfStatus: ESvfStatus,
  parameterDefinitions: IConfigParam,
}

export interface IPostConfigurations{
  name: string,
  comment: string,
  baseConfigurationId: string,
  parameterValues: IPostParamValues[]
}
export interface IPostParamValues {
  parameterDefinitionId: string,
  value: string
}

export interface IConfigurationListItem {
  id: string,
  configurationName: string,
  componentName: string,
  comment: string,
  parentId: string,
  created: string,
  status: EConfigurationStatus,
  svfStatus: ESvfStatus
}

export interface IConfigurationParamData {
  childs: IConfigurationParamData[],
  componentId: string,
  componentName: string,
  parameters: IConfigParam[]
}

export interface IConfigParam {
  id: string,
  displayPriority: number,
  name: string,
  displayName: string,
  valueType: EParameterValueType,
  units: string,
  configurationId: string,
  isReadOnly: boolean,
  isHidden: boolean,
  allowCustomValues: boolean,
  value: string,
  valueOptions: IConfParamOptions[],
  childs: string[]
}

export interface IConfParamOptions {
  id: string,
  value: string,
  parameterDefinitionId: string,
  created: string,
}


export interface ISearchConfigPayload {
  id: string,
  value: string,
  take?: number,
  skip?: number,
}

export enum EModelState {
  Master = 0
}

export enum ESvfStatus {
  InQueue = 1,
  InProcess = 2,
  ServiceUnavailableError = 4,
  IncorrectRequestError = 8,
  Ready = 16
}

export enum EConfigurationStatus {
  InQueue = 1,
  InProcess = 2,
  ServiceUnavailableError = 4,
  IncorrectRequestError = 8,
  InvalidConfiguration = 16,
  Ready = 32
}

export enum EParameterValueType
{
  String = 1,
  Int,
  Double
}