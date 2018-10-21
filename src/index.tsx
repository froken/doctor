import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import './index.css';
import registerServiceWorker from './registerServiceWorker';
import { Store } from "./types/store";

ReactDOM.render(
  <Provider store={Store}>
    <BrowserRouter>
      <div>
        <Route path='/' component={App} />
      </div>
    </BrowserRouter>
   </Provider>,
  document.getElementById('root') as HTMLElement
);
registerServiceWorker();
