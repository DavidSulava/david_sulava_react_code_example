import { ICommonActions } from './actions';
import { SET_ERROR } from './constants';

export interface ICommonState {
  error: string,
}

const initialState: ICommonState = {
  error: '',
}

export const commonReducer = (
  state: ICommonState = initialState,
  action: ICommonActions
) => {
  switch(action.type) {
    case SET_ERROR:
      return {
        ...state,
        error: action.payload
      }
    default:
      return state
  }
}