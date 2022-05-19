import { apiRoutes } from "./routes"
import { ISignInData, ISignUpData, ITariff, IUser } from "../../types/user";
import client from './http.client';
import { IOrganisation, IPostOrganisation } from '../../types/OrganisationPage';

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
}