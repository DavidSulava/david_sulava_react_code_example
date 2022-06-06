export interface IOrganisation{
  id: string,
  name: string,
  description: string,
  orgType: EOrganisationTypes,
  tariffId: string
}
export interface IPostOrganisation{
  name: string,
  description: string,
  orgType: EOrganisationTypes,
  userId: string,
  tariffId?: string
}

export enum EOrganisationTypes {
  Design = 0,
  Manufacturing = 1,
  Sales = 2,
}