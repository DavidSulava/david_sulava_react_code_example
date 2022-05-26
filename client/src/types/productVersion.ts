import { IKendoResp } from './common';

export interface IGetProductVersion extends IKendoResp {
  data: IProductVersion[],
}

export interface IPostProductVersion extends FormData{
}

export interface IProductVersion {
  id: string,
  sequenceNumber: number,
  appBundleId: string,
  name: string,
  version: string,
  designGearVersion: string
  inventorVersion: string,
  created: string,
  productId: string,
  imageFiles: string[],
  configurations: IConfigurations[],
}

export type  IConfigurations = {
  id: string,
  name: string,
  comment: string,
  modelState?: eModelState,
  productVersionId: string,
  modelFile?: string,
  targetFileId: string,
  parameterDefinitions: IParameterDefinition[]
}

export interface IParameterDefinition {
  id: string,
  displayPriority: number,
  name?: string,
  displayName?: string,
  valueType: IValueType,
  units?: string,
  configurationId: string,
  isReadOnly: boolean,
  isHidden: boolean,
  allowCustomValues: boolean,
  value?: string,
}

export enum eModelState {
  Master = 0
}

export enum IValueType {
  String = 1,
  Int,
  Double
}

