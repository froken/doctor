import { applyMiddleware, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { RootAction } from "../actions/root-action";
import * as RootReducer from "../reducers/root";

const composeEnhancers = (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const Store = createStore<RootReducer.IRootState, RootAction, any, any> (
    RootReducer.reducer, 
    RootReducer.initialState,
    composeEnhancers(applyMiddleware(
        thunk
    ))
);