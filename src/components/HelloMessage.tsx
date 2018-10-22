import * as React from 'react';

export interface IProps {
    name: string | null
}

export class HelloMessage extends React.Component<IProps, {}> {
    public render() {
        return (
            <div>Hello, {this.props.name}</div>
        )
    }
}