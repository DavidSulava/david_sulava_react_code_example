import { apiRoutes } from "./routes"
import { ISignInData, ISignUpData, ITariff, IUser } from "../../types/user";
import client from './http.client';
import { IOrganisation, IPostOrganisation } from '../../types/organisationPage';
import { IGetProdResp, IPostProduct, IProduct, IPutProduct } from '../../types/product';
import { IGetProductVersion, IPostProductVersion, IProductVersion } from '../../types/productVersion';
import { IAppBundle } from '../../types/common';

export default class Api {
  public static async signIn(formData: ISignInData): Promise<IUser> {
    const response = await client.post(apiRoutes.signIn, formData)
    return response.data
  }

  public static async signUp(formData: ISignUpData): Promise<string> {
    const response = await client.post(apiRoutes.signUp, formData)
    return response.data
  }

  public static async authOrg(orgId: string): Promise<IUser> {
    const response = await client.post(apiRoutes.authOrg(orgId))
    return response.data
  }

  public static async getOrganisations(userId: string): Promise<IOrganisation[]> {
    const response = await client.get(apiRoutes.organisations.list(userId))
    return response.data
  }

  public static async postOrganisation(organisation: IPostOrganisation): Promise<string> {
    const response = await client.post(apiRoutes.organisations.root, organisation)
    return response.data
  }

  public static async getTariff(): Promise<ITariff[]> {
    const response = await client.get(apiRoutes.tariff.root)
    return response.data
  }

  public static async getProduct(dataString?: string): Promise<IGetProdResp> {
    const response = await client.get(apiRoutes.product.root(dataString))
    return response.data
  }

  public static async postProduct(formData: IPostProduct): Promise<string> {
    const response = await client.post(apiRoutes.product.root(), formData)
    return response.data
  }

  public static async putProduct(formData: IPutProduct): Promise<string> {
    const response = await client.put(apiRoutes.product.root(), formData)
    return response.data
  }

  public static async delProduct(id: string): Promise<string> {
    const response = await client.delete(apiRoutes.product.root(id))
    return response.data
  }

  public static async getProdVersion(params: string): Promise<IGetProductVersion> {
    const response = await client.get(apiRoutes.productVersion.root(params))
    return response.data
  }
  public static async postProdVersion(formData: IPostProductVersion): Promise<string> {
    const response = await client.post(apiRoutes.productVersion.root(), formData)
    return response.data
  }
  public static async putProdVersion(formData: IPostProductVersion): Promise<string> {
    const response = await client.put(apiRoutes.productVersion.root(), formData)
    return response.data
  }
  public static async delProductVersion(id: string): Promise<string> {
    const response = await client.delete(apiRoutes.productVersion.root(id))
    return response.data
  }

  public static async getAppBundle(): Promise<IAppBundle[]> {
    const response = await client.get(apiRoutes.appBundle)
    return response.data
  }
}