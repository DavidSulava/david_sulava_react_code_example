import { IOrganisation, IPostOrganisation } from '../../types/OrganisationPage';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface IOrganisationState {
  organisations: IOrganisation[],
  isLoadingOrganisation: boolean,
}

const initialState: IOrganisationState = {
  organisations: [],
  isLoadingOrganisation: false
}

const organisationSlice = createSlice({
  name: "organisation",
  initialState: initialState,
  reducers: {
    getOrganisations: (state, action: PayloadAction<string>) => {
    },
    postOrganisation: (state, action: PayloadAction<IPostOrganisation>) => {
    },
    isLoadingOrganisation: (state, action: PayloadAction<boolean>) => {
      state.isLoadingOrganisation = action.payload
    },
    saveOrganisations: (state, action: PayloadAction<IOrganisation[]>) => {
      state.organisations = action.payload
    }
  }
});

export const {isLoadingOrganisation, saveOrganisations, getOrganisations, postOrganisation} = organisationSlice.actions

export default organisationSlice.reducer