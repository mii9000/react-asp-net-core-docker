import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import userGroupReducer from "../features/user-group/userGroupSlice";

export const store = configureStore({
  reducer: {
    userGroups: userGroupReducer
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppThunk = ThunkAction<void, RootState, unknown, Action<string>>;
