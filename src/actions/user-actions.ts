
import { Action } from "redux";
import { ThunkDispatch } from "redux-thunk";
import { history } from "../actions/history";
import Configuration from '../types/configuration';
import { User } from "../types/user";

// Action Type Enum
export enum UserActionTypes {
    REGISTER_USER = "[user] REGISTER_USER",
    REGISTER_USER_SUCCESS = "[user] REGISTER_USER_SUCCESS",
    REGISTER_USER_ERROR = "[user] REGISTER_USER_ERROR",
    LOGIN_USER = "[user] LOGIN_USER",
    LOGIN_USER_SUCCESS = "[user] LOGIN_USER_SUCCESS",
    LOGIN_USER_ERROR = "[user] LOGIN_USER_ERROR"
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

export interface ILoginUser extends Action {
    type: UserActionTypes.LOGIN_USER;
 }

export interface ILoginUserSuccess extends Action {
    type: UserActionTypes.LOGIN_USER_SUCCESS;
    payload: {
        userName: string;
    }
}

export interface ILoginUserError extends Action {
    type: UserActionTypes.LOGIN_USER_ERROR;
    payload: {
        error: string;
    }
}

// Action Type
export type UserAction = 
    IRegisterUser |
    IRegisterUserError |
    IRegisterUserSuccess |
    ILoginUser |
    ILoginUserError |
    ILoginUserSuccess;


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

            if (response.status !== 200) {
                throw new Error("Couldn't register user. Response " + response.status);
            }

            const action: IRegisterUserSuccess = {
                payload: {
                    user
                },
                type: UserActionTypes.REGISTER_USER_SUCCESS,
            };

            dispatch(action);
            localStorage.setItem('user', JSON.stringify(user));

            history.push('/');
        }
        catch (e) {
            dispatch({ type: UserActionTypes.REGISTER_USER_ERROR, payload: { e } })
        }
    }
}

export function login(userName: string, password: string, d: ThunkDispatch<any, any, Action>) {
    return async (dispatch: ThunkDispatch<any, any, Action>) => {
        dispatch({ type: UserActionTypes.REGISTER_USER });

        try {
            const loginUser = {
                password,
                userName
            };

            const response: Response = await fetch(
                `${Configuration.ApiUrl}/api/auth/login`, 
                { 
                    body: JSON.stringify(loginUser),
                    headers: {
                        'Accept': 'application/json',
                        'Access-Control-Allow-Credentials': 'true',
                        'Access-Control-Allow-Origin': 'localhost',
                        'Content-Type': 'application/json'
                    },
                    method: "post",
                });

            if (response.status !== 200) {
                throw new Error("Couldn't login user. Response " + response.status);
            }

            const action: ILoginUserSuccess = {
                payload: {
                    userName
                },
                type: UserActionTypes.LOGIN_USER_SUCCESS,
            };

            dispatch(action);
            localStorage.setItem('userName', userName);
            history.push('/');
        }
        catch (e) {
            dispatch({ type: UserActionTypes.REGISTER_USER_ERROR, payload: { e } })
        }
    }
}