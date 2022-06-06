import { IKendoResp } from './common';

export interface IGetConfigurations extends IKendoResp {
  data: IConfigurations[],
}
export interface IPostConfigurations{
  organizationId: string,
  productId: string,
  productVersionId: string,
  appBundleId: string,
  status: EConfigurationStatus,
  svfStatus: ESvfStatus,
  name: string,
  comment: string,
  baseConfigurationId: string,
  parameterValues: IPostParamValues[]
}
export interface IPostParamValues {
  parameterDefinitionId: string,
  value: string
}

export interface IConfigurations {
  id: string,
  configurationName: string,
  componentName: string,
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
  InQueue = 0,
  InProcess = 1,
  Error = 2,
  Ready = 3
}

export enum EConfigurationStatus {
  InQueue = 0,
  InProcess = 1,
  Error = 2,
  Ready = 3
}

export enum EParameterValueType
{
  String = 1,
  Int,
  Double
}