import { SortDescriptor } from '@progress/kendo-data-query';

export type  IGridFilterSetting = {
  value: string,
  operator: string,
  field: string
}

export type  IGridDataFilter = {
  filters: IGridFilterSetting[],
  logic: string
}

export type  IGridDataStateFilter = {
  filter: IGridDataFilter
  group: string
  skip: number
  sort: SortDescriptor[]
  take: number
}

export type IGridDataState = {
  take: number,
  skip: number,
  sort: SortDescriptor[]
}

export type  IProduct = {
  id: string,
  name: string,
  description: string,
  organizationId?: string,
  currentVersion: string,
  productVersions: IProductVersion[],
}

export interface IProductVersion {
  id: string,
  sequenceNumber: number,
  name?: string,
  version?: string,
  designGearVersion?: string
  inventorVersion?: string,
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

export interface IPostProduct {
  name: string,
  description: string,
  organizationId: string
}

export interface IPutProduct extends IPostProduct {
  id: string
}

export interface IDelProduct {
  prodId: string,
  organisationId?: string
}

export enum eModelState {
  Master = 0
}

export enum IValueType {
  String = 1,
  Int,
  Double
}