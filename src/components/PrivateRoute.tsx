import * as React from 'react';
import { Redirect, Route } from 'react-router-dom';

export interface IProps {
    path: string,
    component: React.ComponentClass<any, any>
}

export class PrivateRoute extends React.Component<IProps, {}> {

    public render() {
        const user = localStorage.getItem('user');
        const location = (this.props as any).location as Location;

        if (user) {
            return <Route path={this.props.path} component={this.props.component}/>;
        }

        if (location && location.pathname === '/login') {
            return null;
        }

        return <Redirect to={{ pathname: '/login', state: { from: location } }} />;
    }
}
