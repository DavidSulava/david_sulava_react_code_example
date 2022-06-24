import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface ICommonState {
  error: any,
  postReqResp: any,
  inPendingList: string[],
}

const initialState: ICommonState = {
  error: '',
  postReqResp: '',
  inPendingList: [],
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
    },
    pushInPending: (state, action: PayloadAction<string>) => {
      state.inPendingList.push(action.payload)
    },
    popFromPending: (state, action: PayloadAction<string>) => {
      state.inPendingList = state.inPendingList.filter(el=> el !== action.payload)
    },
  }
});

export const {setError, setPostReqResp, pushInPending, popFromPending} = commonSlice.actions

export default commonSlice.reducer