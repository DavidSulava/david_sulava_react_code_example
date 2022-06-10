import { SortDescriptor } from '@progress/kendo-data-query';
import { JwtPayload } from 'jwt-decode';

declare global {
  interface Window {
    API_URL: string;
    Autodesk:  typeof Autodesk,
  }
}

export type Nullable<T> = T|undefined|null

export interface IKendoResp {
  aggregateResults: any,
  data: any,
  errors: any,
  total: number
}
export interface IKendoOnChangeEvent {
  target?: any,
  value?: any
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

export interface IAppJwtPayload extends JwtPayload{
  OrganisationId: string,
  UserId: string,
  Email: string,
  FirstName: string,
  LastName: string,
  Phone: string,
  Created: string,
  Role: string,
}

export interface ICommonModalProps {
  isOpen: boolean
  onSubmit?: <T>(data?: T) => void,
  onClose: () => void,
}
export interface ICommonObject {
  [p: string]: any
}

export interface IStdEnum {
  [key: string|number]: string|number
}