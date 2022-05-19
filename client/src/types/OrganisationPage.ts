export interface IOrganisation{
  id: string,
  name: string,
  description: string,
  orgType: eOrganisationTypes,
  tariffId: string
}
export interface IPostOrganisation{
  name: string,
  description: string,
  orgType: eOrganisationTypes,
  userId: string,
  tariffId?: string
}

export enum eOrganisationTypes {
  Design = 0,
  Manufacturing = 1,
  Sales = 2,
}