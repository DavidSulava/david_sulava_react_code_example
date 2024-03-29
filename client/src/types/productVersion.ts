import { IKendoResp } from './common';
import { IConfigurationListItem } from './producVersionConfigurations';

export interface IGetProductVersionList extends IKendoResp {
  data: IProductVersionList[],
}

export interface IGetProductVersionById extends IKendoResp {
  data: IProductVersion,
}

export interface IPostProductVersion extends FormData {
  ProductId: string,
  SequenceNumber: number,
  AppBundleId: string,
  Name: string,
  Version: string,
  DesignGearVersion: string
  InventorVersion: string,
  IsCurrent: boolean,
  UseAsTemplateConfiguration?: boolean,
  ImageFiles: string[],
  ModelFile: string,
}

export interface IProductVersion {
  id: string,
  sequenceNumber: number,
  appBundleId: string,
  name: string,
  productName: string,
  version: string,
  designGearVersion: string
  inventorVersion: string,
  created: string,
  productId: string,
  isCurrent: boolean,
  imageFiles: string[],
  configurations: IConfigurationListItem[],
}

export interface IProductVersionList {
  id: string,
  sequenceNumber: number,
  appBundleId: string,
  name: string,
  version: string,
  designGearVersion: string
  inventorVersion: string,
  created: string,
  productId: string,
  isCurrent: boolean,
}