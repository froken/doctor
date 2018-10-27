import * as React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import './App.css';
import { PrivateRoute } from './components/PrivateRoute';
import { HomePage } from './containers/HomePage';
import LoginPage from './containers/LoginPage';
import { RegisterPage } from './containers/RegisterPage';

class App extends React.Component {
  public render() {
    return (
      <BrowserRouter>
        <div>
            <Switch>
              <Route path="/login" component={LoginPage} />
              <Route path="/register" component={RegisterPage} />
              <PrivateRoute path="/" component={HomePage}/>
            </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
