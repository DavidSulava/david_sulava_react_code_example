import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IAppBundle } from '../../types/appBundle';

export interface ICommonState {
  error: any,
  postReqResp: any,
  appBundle: IAppBundle[],
}

const initialState: ICommonState = {
  error: '',
  postReqResp: '',
  appBundle: [],
}

const commonSlice = createSlice({
  name: "common",
  initialState: initialState,
  reducers: {
    setPostReqResp: (state, action: PayloadAction<any>) => {
      state.postReqResp = action.payload
    },
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload
    }
  }
});

export const {setError, setPostReqResp} = commonSlice.actions

export default commonSlice.reducer