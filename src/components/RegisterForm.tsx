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
                <div className="login-page">
                    <div className="form">
                        <form className="register-form">
                            <input type="text" placeholder="email address" name="email" value={this.props.user && this.props.user.email != null ? this.props.user.email : ""} onChange={this.props.onChange}/>
                            <input type="text" placeholder="user name" name="userName" value={this.props.user && this.props.user.userName != null ? this.props.user.userName : ""} onChange={this.props.onChange}/>
                            <input type="password" placeholder="password" name="password" value={this.props.user && this.props.user.password != null ? this.props.user.password : ""} onChange={this.props.onChange}/>
                            
                            <button onClick={this.props.onSubmit}>Register</button>
                            <p className="message">Already registered? <Link to="/login">Sign In</Link></p>
                        </form>
                    </div>
                </div>
        )
    }
}