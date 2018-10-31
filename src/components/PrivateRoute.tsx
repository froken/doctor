import * as React from 'react';
import { Redirect, Route } from 'react-router-dom';

export interface IProps {
    path: string,
    component: React.ComponentClass<any, any>
}

export class PrivateRoute extends React.Component<IProps, {}> {

    public render() {
        const userName = localStorage.getItem('userName');
        const location = (this.props as any).location as Location;

        if (userName) {
            return <Route path={this.props.path} component={this.props.component}/>;
        }

        return <Redirect to={{ pathname: '/login', state: { from: location } }} />;
    }
}
