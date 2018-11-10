import { combineReducers, Reducer } from "redux";
import * as UserReducer from "./user-reducer";

// Root State Definition
export interface IRootState {
    userState: UserReducer.IUserState,
}

// Initial State
export const initialState: IRootState = {
    userState: UserReducer.initialUserState,
}

// Reducer
export const reducer: Reducer<IRootState> = combineReducers<IRootState>({
    userState: UserReducer.reducer
});