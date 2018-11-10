import { connect } from "react-redux";
import { Action } from "redux";
import { ThunkDispatch } from "redux-thunk";
import { login } from "../actions/user-actions";
import { IDispatchProps, IState, LoginForm } from "../components/LoginForm";

const mapStateToProps = (state: IState, props: any): any => ({ });
  
const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, Action>): IDispatchProps => {
  return {
      login: (userName: string, password: string) => dispatch(login(userName, password, dispatch)),
  };
};

export default connect<any, IDispatchProps, any, any>(mapStateToProps, mapDispatchToProps)(LoginForm as any);