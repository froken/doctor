
import { Reducer } from "redux";
import { UserAction, UserActionTypes } from "../actions/user-actions";

// State Definition
export interface IUserState {
    userName: string | undefined,
};

// Initial State
export const initialUserState: IUserState = {
    userName: undefined
};

// Reducer
export let reducer: Reducer<IUserState, UserAction> = 
    (state: IUserState = initialUserState, action: UserAction) => {
        switch (action.type) {
            case UserActionTypes.REGISTER_USER_SUCCESS:
                return { ...state, userName: action.payload.user.userName };
            case UserActionTypes.LOGIN_USER_SUCCESS:
                return { ...state, userName: action.payload.userName };
            default:
                return state;
        }
    };