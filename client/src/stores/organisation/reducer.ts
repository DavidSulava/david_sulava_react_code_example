import { IOrganisation } from '../../types/OrganisationPage';
import { IOrganisationActions } from './actions';
import { IS_LOADING_ORGANISATION, SAVE_ORGANISATIONS } from './constants';

export interface IOrganisationState {
  organisations: IOrganisation[],
  isLoadingOrganisation: boolean,
}

const initialState: IOrganisationState = {
  organisations: [],
  isLoadingOrganisation: false
}

export const organisationReducer = (
  state: IOrganisationState = initialState,
  action: IOrganisationActions
) => {
  switch(action.type) {
    case SAVE_ORGANISATIONS:
      return {
        ...state,
        organisations: action.payload
      }
    case IS_LOADING_ORGANISATION:
      return {
        ...state,
        isLoadingOrganisation: action.payload
      }
    default:
      return state
  }
}