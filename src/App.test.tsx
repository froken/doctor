import 'jest-localstorage-mock';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import App from './App';
import { Store } from './types/store';

describe("App component tests", () => {
  it('renders without crashing', () => {
    const div = document.createElement('div');
    ReactDOM.render(<Provider store={Store}><App /></Provider>, div);
    ReactDOM.unmountComponentAtNode(div);
  });
})


