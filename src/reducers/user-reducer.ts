
import { Reducer } from "redux";
import { UserAction, UserActionTypes } from "../actions/user-actions";
import { User } from '../types/user';

// State Definition
export interface IUserState {
    user: User,
};

// Initial State
export const initialUserState: IUserState = {
  user: {
      email: null,
      password: null,
      userName: null
  }
};

// Reducer
export let reducer: Reducer<IUserState, UserAction> = 
    (state: IUserState = initialUserState, action: UserAction) => {
        switch (action.type) {
            case UserActionTypes.REGISTER_USER_SUCCESS:
                return { ...state, user: action.payload.user };
            default:
                return state;
        }
    };