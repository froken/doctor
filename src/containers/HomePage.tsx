import * as React from 'react';
import { connect } from 'react-redux';
import { Action } from 'redux';
import { ThunkDispatch } from 'redux-thunk';
import { HelloMessage } from '../components/HelloMessage';
import { IRootState } from '../reducers/root';
import { User } from '../types/user';

export interface IStateProps {
    user: User
}

export class HomePage extends React.Component<IStateProps, {}> {
    public render() {
        return (
            <HelloMessage name={this.props.user.login} />
        )
    }
}

const mapStateToProps = (state: IRootState, props: IStateProps): IStateProps => ({
    user: state.userState.user,
});

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, Action>): any => {
    return {
    };
};

export default connect<any, {}, any, any>(mapStateToProps, mapDispatchToProps)(HomePage as any);