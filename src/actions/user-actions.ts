
import { createBrowserHistory } from 'history';
import { Action } from "redux";
import { ThunkDispatch } from "redux-thunk";
import Configuration from 'src/types/configuration';
import { User } from "../types/user";

// Action Type Enum
export enum UserActionTypes {
    REGISTER_USER = "[user] REGISTER_USER",
    REGISTER_USER_SUCCESS = "[user] REGISTER_USER_SUCCESS",
    REGISTER_USER_ERROR = "[user] REGISTER_USER_ERROR"
}

// Action Interfaces
export interface IRegisterUser extends Action {
    type: UserActionTypes.REGISTER_USER;
    payload: {
        user: User;
    }
}

export interface IRegisterUserSuccess extends Action {
    type: UserActionTypes.REGISTER_USER_SUCCESS;
    payload: {
        user: User;
    }
}


export interface IRegisterUserError extends Action {
    type: UserActionTypes.REGISTER_USER_ERROR;
    payload: {
        error: string;
    }
}

// Action Type
export type UserAction = 
    IRegisterUser |
    IRegisterUserError |
    IRegisterUserSuccess;


// Action Creators
export function register(user: User, d: ThunkDispatch<any, any, Action>) {
    return async (dispatch: ThunkDispatch<any, any, Action>) => {
        dispatch({ type: UserActionTypes.REGISTER_USER });

        try {
            const response: Response = await fetch(
                `${Configuration.ApiUrl}/api/account`, 
                { 
                    body: JSON.stringify(user),
                    headers: {
                        'Accept': 'application/json',
                        'Access-Control-Allow-Credentials': 'true',
                        'Access-Control-Allow-Origin': 'localhost',
                        'Content-Type': 'application/json'
                    },
                    method: "post",
                });

            const result: any = await response.json();
            const addedTask: IRegisterUserSuccess = {
                payload: {
                    user: result
                },
                type: UserActionTypes.REGISTER_USER_SUCCESS,
            };

            dispatch(addedTask);
            localStorage.setItem('user', JSON.stringify(user));

            const browserHistory = createBrowserHistory();
            browserHistory.push('/');
        }
        catch (e) {
            dispatch({ type: UserActionTypes.REGISTER_USER_ERROR, payload: { e } })
        }
    }
}
