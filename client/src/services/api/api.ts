import { apiRoutes } from "./routes"
import { ISignInData, ISignUpData, ITariff, IUser } from "../../types/user";
import client from './http.client';
import { IOrganisation, IPostOrganisation } from '../../types/organisationPage';
import { IGetProdResp, IPostProduct, IProduct, IPutProduct } from '../../types/product';
import { IGetProductVersionList, IPostProductVersion, IProductVersion } from '../../types/productVersion';
import { IConfiguration, IConfigurationParamData, IGetConfigurations, IPostConfigurations } from '../../types/producVersionConfigurations';
import { IAppBundle } from '../../types/appBundle';

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

  public static async getProdVersionList(params: string): Promise<IGetProductVersionList> {
    const response = await client.get(apiRoutes.productVersion.root(params))
    return response.data
  }
  public static async getProdVersion(id: string): Promise<IProductVersion> {
    const response = await client.get(apiRoutes.productVersion.item(id))
    return response.data
  }
  public static async getConfigurationList(params: string): Promise<IGetConfigurations> {
    const response = await client.get(apiRoutes.configurations.root(params))
    return response.data
  }
  public static async getConfigurationById(id: string): Promise<IConfiguration> {
    const response = await client.get(apiRoutes.configurations.item(id))
    return response.data
  }
  public static async getConfigParams(configurationId: string): Promise<IConfigurationParamData> {
    const response = await client.get(apiRoutes.configurations.params(configurationId))
    return response.data
  }
  public static async postConfig(formData: IPostConfigurations): Promise<string> {
    const response = await client.post(apiRoutes.configurations.root(), formData)
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