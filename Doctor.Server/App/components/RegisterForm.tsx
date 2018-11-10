import * as React from 'react';
import { Link } from 'react-router-dom';
import { User } from '../types/user';

export interface IState {
    user: User
}

export interface IDispatchProps {
    register: (user: User) => void;
}

export class RegisterForm extends React.Component<IDispatchProps, IState> {

    public constructor(props: IDispatchProps) {
        super(props);

        this.state = {
            user: {
                email: "",
                password: "",
                userName: ""
            }
        }

        this.onChange = this.onChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    public onChange(event: any) {
        const { name, value } = event.target;
        const user = { ...this.state.user, [name]: value };

        this.setState((state:IState) => {
            return {...state, user }
        });
    }

    public onSubmit(e: any) {
        e.preventDefault();
        this.props.register(this.state.user);
    }

    public render() {
        return (
                <div className="login-page">
                    <div className="form">
                        <form className="register-form">
                            <input type="text" placeholder="email address" name="email" 
                                value={this.state.user.email} onChange={this.onChange}/>

                            <input type="text" placeholder="user name" name="userName" 
                                value={this.state.user.userName} onChange={this.onChange}/>

                            <input type="password" placeholder="password" name="password" 
                                value={this.state.user.password} onChange={this.onChange}/>
                            
                            <button onClick={this.onSubmit}>Register</button>
                            <p className="message">Already registered? <Link to="/login">Sign In</Link></p>
                        </form>
                    </div>
                </div>
        )
    }
}