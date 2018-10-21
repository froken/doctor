import * as React from 'react';
import { Link } from 'react-router-dom';
import { User } from '../types/user';

export interface IStateProps {
    user: User
}

export interface IProps {
    onChange: (e: any) => void;
    onSubmit: (e: any) => void;
}

export class RegisterForm extends React.Component<IStateProps & IProps, {}> {

    public constructor(props: IStateProps & IProps) {
        super(props);
    }

    public render() {
        return (
            <div>
                <div>Register Page</div>
                <div>
                    <label htmlFor="login">Login</label>
                    <input type="text" name="login" value={this.props.user.login != null ? this.props.user.login : ""} onChange={this.props.onChange} />
                </div>
                <div>
                    <label htmlFor="email">Email</label>
                    <input type="text" name="email" value={this.props.user.email != null ? this.props.user.email : ""} onChange={this.props.onChange} />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input type="password" name="password" value={this.props.user.password != null ? this.props.user.password : ""} onChange={this.props.onChange} />
                </div>
                <div>
                    <button onClick={this.props.onSubmit}>Register</button>
                    <Link to="/login">Cancel</Link>
                    </div>
            </div>
        )
    }
}