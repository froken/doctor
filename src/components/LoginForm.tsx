import * as React from 'react';
import { Link } from 'react-router-dom';


export interface IDispatchProps {
    login: (userName: string, password: string) => {}
}

export interface IState {
    userName: string,
    password: string
}

export class LoginForm extends React.Component<IDispatchProps, IState> {
    public constructor(props: IDispatchProps) {
        super(props);  

        this.handleLogin = this.handleLogin.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    public handleLogin() {
        this.props.login(this.state.userName, this.state.password);
    }

    public handleChange(event: any) {
        const { name, value } = event.target;
        this.setState((state: IState) => ({...state,  [name]: value }));
    }

    public render() {
        return (<div className="login-page">
                    <div className="form">
                        <form className="login-form">
                            <input type="text" placeholder="username" value={this.state.userName} onChange={this.handleChange}/>
                            <input type="password" placeholder="password" value={this.state.password} onChange={this.handleChange}/>
                            <button onClick={this.handleLogin}>login</button>
                            <p className="message">Not registered? <Link to="/register">Create an account</Link></p>
                        </form>
                    </div>
                </div>)
    
    }
}