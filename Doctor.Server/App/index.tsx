import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import App from './App';
import { Store } from "./types/store";

if (module['hot']) {
    module['hot'].accept();
}

ReactDOM.render(
    <Provider store={Store}>
        <App />
    </Provider>,
    document.getElementById('root') as HTMLElement
);