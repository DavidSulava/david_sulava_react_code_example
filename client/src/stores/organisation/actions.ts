import { GET_ORGANISATIONS, SAVE_ORGANISATIONS, IS_LOADING_ORGANISATION, POST_ORGANISATION } from './constants';
import { IOrganisation, IPostOrganisation } from '../../types/OrganisationPage';

export interface IGetOrganisation {
  type: typeof GET_ORGANISATIONS,
  userId: string
}

export interface ISaveOrganisationAction {
  type: typeof SAVE_ORGANISATIONS,
  payload: IOrganisation[]
}

export interface IPostOrganisationAction {
  type: typeof POST_ORGANISATION,
  organisation: IPostOrganisation
}

export interface IIsLoadingOrganisationAction {
  type: typeof IS_LOADING_ORGANISATION,
  payload: boolean
}

export type IOrganisationActions = ISaveOrganisationAction
  |IIsLoadingOrganisationAction

export class OrganisationActions {
  public static isLoadingOrganisation(isLoading: boolean): IIsLoadingOrganisationAction {
    return {
      type: IS_LOADING_ORGANISATION,
      payload: isLoading
    }
  }

  public static getOrganisations(userId: string): IGetOrganisation {
    return {
      type: GET_ORGANISATIONS,
      userId
    }
  }

  public static postOrganisation(organisation: IPostOrganisation): IPostOrganisationAction {
    return {
      type: POST_ORGANISATION,
      organisation
    }
  }

  public static saveOrganisations(organisations: IOrganisation[]): ISaveOrganisationAction {
    return {
      type: SAVE_ORGANISATIONS,
      payload: organisations
    }
  }
}