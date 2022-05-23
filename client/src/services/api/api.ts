import { apiRoutes } from "./routes"
import { ISignInData, ISignUpData, ITariff, IUser } from "../../types/user";
import client from './http.client';
import { IOrganisation, IPostOrganisation } from '../../types/OrganisationPage';
import { IPostProduct, IProduct, IPutProduct } from '../../types/product';

export default class Api {
  public static async signIn(formData: ISignInData): Promise<IUser> {
    const response = await client.post(apiRoutes.signIn, formData)
    return response.data
  }

  public static async signUp(formData: ISignUpData): Promise<string> {
    const response = await client.post(apiRoutes.signUp, formData)
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

  public static async getProduct(dataString?: string): Promise<IProduct[]> {
    const response = await client.get(apiRoutes.product.root(dataString))
    return response.data?.data
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
}