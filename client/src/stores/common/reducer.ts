import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface ICommonState {
  error: string,
}

const initialState: ICommonState = {
  error: '',
}


const commonSlice = createSlice({
  name: "common",
  initialState: initialState,
  reducers: {
    setError: (state, action: PayloadAction<string>) => {
      state.error = action.payload
    }
  }
});

export const { setError } =commonSlice.actions

export default commonSlice.reducer