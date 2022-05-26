import { SortDescriptor } from '@progress/kendo-data-query';
import { JwtPayload } from 'jwt-decode';

export type Nullable<T> = T|undefined|null

export interface IKendoResp {
  aggregateResults: any,
  data: any,
  errors: any,
  total: number
}

export interface  IGridFilterSetting {
  value: string,
  operator: string,
  field: string
}
export interface  IGridDataFilter {
  filters: IGridFilterSetting[],
  logic: string
}
export interface  IGridDataStateFilter {
  filter: IGridDataFilter
  group: string
  skip: number
  take: number
  sort: SortDescriptor[]
}
export interface IGridDataState {
  take: number,
  skip: number,
  sort: SortDescriptor[]
}

export interface IAppBundle {
  id: string,
  name: string,
  description: string,
  designGearVersion: string,
  inventorVersion: string,
}

export interface IAppJwtPayload extends JwtPayload{
  OrganisationId: string,
  UserId: string
}

export interface ICommonModalProps {
  isOpen: boolean
  onSubmit?: <T>(data?: T) => void,
  onClose: () => void,
}