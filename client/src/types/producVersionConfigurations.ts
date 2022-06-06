import { IKendoResp } from './common';

export interface IGetConfigurations extends IKendoResp {
  data: IConfigurations[],
}

export interface IConfigurations {
  id: string,
  name: string,
  comment: string,
  modelState?: EModelState,
  productVersionId: string,
  modelFile?: string,
  targetFileId: string,
  parameterDefinitions: IParameterDefinition[]
}

export interface IConfigurationParamData {
  componentId: string,
  componentName: string,
  parameters: IConfigParam[]
}

export interface IConfigParam {
  id: string,
  displayPriority: 0,
  name: string,
  displayName: string,
  valueType: 1,
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

export interface IParameterDefinition {
  id: string,
  displayPriority: number,
  name?: string,
  displayName?: string,
  valueType: EValueType,
  units?: string,
  configurationId: string,
  isReadOnly: boolean,
  isHidden: boolean,
  allowCustomValues: boolean,
  value?: string,
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

export enum EValueType {
  String = 1,
  Int,
  Double
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