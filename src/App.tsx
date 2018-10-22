import * as React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import './App.css';
import { LoginPage } from './components/LoginPage';
import { PrivateRoute } from './components/PrivateRoute';
import { HomePage } from './containers/HomePage';
import RegisterPage from './containers/RegisterPage';

class App extends React.Component {
  public render() {
    return (
      <BrowserRouter>
        <div>
            <PrivateRoute path="/" component={HomePage} />
            <Route path="/register" component={RegisterPage}/>
            <Route path="/login" component={LoginPage} />
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
