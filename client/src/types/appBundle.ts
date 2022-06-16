import { IKendoResp } from './common';

export interface IGetAppBundleResp extends IKendoResp{
  data: IAppBundle[],
}
export interface IAppBundle {
  id: string,
  name: string,
  description: string,
  designGearVersion: string,
  inventorVersion: string,
  fileName?: string
}

export interface IPostAppBundle extends FormData{
  Name: string,
  Description: string,
  DesignGearVersion: string,
  InventorVersion: string,
  File: string
}

export interface IPuttAppBundle extends IPostAppBundle{
  id:string
}