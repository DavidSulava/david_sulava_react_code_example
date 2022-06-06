import { IKendoResp } from './common';
import { IConfigurations } from './producVersionConfigurations';

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