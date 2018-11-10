import * as React from 'react';
import { createRef } from 'react';
import { Link } from 'react-router-dom';

export interface IDispatchProps {
    login: (userName: string, password: string) => {}
}

export interface IState {
    userName: string | undefined,
    password: string | undefined
}

export class LoginForm extends React.Component<IDispatchProps, IState> {
    userNameRef: React.RefObject<HTMLInputElement>;
    passwordRef: React.RefObject<HTMLInputElement>;

    public constructor(props: IDispatchProps) {
        super(props);  

        this.state = {
            password: "",
            userName: ""
        }

        this.userNameRef = createRef<HTMLInputElement>();
        this.passwordRef = createRef<HTMLInputElement>();

        this.handleLogin = this.handleLogin.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.getRefValue = this.getRefValue.bind(this);
    }

    public getRefValue(ref: React.RefObject<HTMLInputElement>) {
        return ref && ref.current ? ref.current.value : undefined;
    } 

    public handleLogin(event: any) {
        event.preventDefault();

        const userName = this.getRefValue(this.userNameRef);
        const password = this.getRefValue(this.passwordRef);

        if (userName && password) {
            this.props.login(userName, password);
        }
    }

    public handleChange(event: any) {
        const { name, value } = event.target;
        this.setState((state: IState) => ({...state,  [name]: value }));
    }

    public render() {
        return (<div className="login-page">
                    <div className="form">
                        <form className="login-form">
                            <input type="text" name="userName" ref={this.userNameRef} placeholder="User name" value={this.state.userName} onChange={this.handleChange}/>
                            <input type="password" name="password" ref={this.passwordRef} placeholder="Password" value={this.state.password} onChange={this.handleChange}/>
                            <button onClick={this.handleLogin}>login</button>
                            <p className="message">Not registered? <Link to="/register">Create an account</Link></p>
                        </form>
                    </div>
                </div>)
    
    }
}