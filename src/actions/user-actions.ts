
import { Action } from "redux";
import { ThunkDispatch } from "redux-thunk";
import { User } from "../types/user";

// Action Type Enum
export enum UserActionTypes {
    REGISTER_USER = "[user] REGISTER_USER",
}

// Action Interfaces
export interface IRegisterUser extends Action {
    type: UserActionTypes.REGISTER_USER;
    payload: {
        user: User;
    }
}

// Action Type
export type UserAction = 
    IRegisterUser;


// Action Creators
export function register(user: User, d: ThunkDispatch<any, any, Action>) {
    return async (dispatch: ThunkDispatch<any, any, Action>) => {
        dispatch({ type: UserActionTypes.REGISTER_USER });
    }
}
