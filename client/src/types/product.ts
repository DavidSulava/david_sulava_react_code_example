import { IKendoResp } from './common';
import { IProductVersion } from './productVersion';

export interface IGetProdResp extends IKendoResp{
  data: IProduct[],
}

export type  IProduct = {
  id: string,
  name: string,
  description: string,
  organizationId?: string,
  currentVersion: string,
  productVersions: IProductVersion[],
}

export interface IPostProduct {
  name: string,
  description: string,
}

export interface IPutProduct extends IPostProduct {
  id: string
}

export interface IDelProduct {
  prodId: string,
  organisationId?: string
}