import { connect } from "react-redux";
import { Action } from "redux";
import { ThunkDispatch } from "redux-thunk";
import { User } from 'src/types/user';
import { register } from "../actions/user-actions";
import { IDispatchProps, IState, RegisterForm } from "../components/RegisterForm";

const mapStateToProps = (state: IState, props: any): any => ({ });
  
const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, Action>): IDispatchProps => {
  return {
      register: (user: User) => dispatch(register(user, dispatch)),
  };
};

export default connect<any, IDispatchProps, any, any>(mapStateToProps, mapDispatchToProps)(RegisterForm as any);