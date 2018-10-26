import * as React from 'react';
import { Link } from 'react-router-dom';

export class LoginForm extends React.Component<{}, {}> {
    public render() {
        return (<div className="login-page">
                    <div className="form">
                        <form className="login-form">
                            <input type="text" placeholder="username"/>
                            <input type="password" placeholder="password"/>
                            <button>login</button>
                            <p className="message">Not registered? <Link to="/register">Create an account</Link></p>
                        </form>
                    </div>
                </div>)
    
    }
}