import { connect } from 'react-redux';
import { Action } from 'redux';
import { ThunkDispatch } from 'redux-thunk';
import { HomePage } from '../components/HomePage';
import { IRootState } from '../reducers/root';

const mapStateToProps = (state: IRootState, props: any): any => ({
    name: state.userState.user.userName,
});

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, Action>): any => {
    return {
    };
};

export default connect<any, {}, any, any>(mapStateToProps, mapDispatchToProps)(HomePage as any);