import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface ICommonState {
  error: any,
  postReqResp: string,
}

const initialState: ICommonState = {
  error: '',
  postReqResp: '',
}

const commonSlice = createSlice({
  name: "common",
  initialState: initialState,
  reducers: {
    setPostReqResp: (state, action: PayloadAction<string>) => {
      state.postReqResp = action.payload
    },
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload
    }
  }
});

export const {setError, setPostReqResp} = commonSlice.actions

export default commonSlice.reducer