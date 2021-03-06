import * as React from 'react';
import { Route, Router, Switch } from 'react-router-dom';
import { history } from "./actions/history";
import { PrivateRoute } from './components/PrivateRoute';
import HomePage from './containers/HomePage';
import LoginForm from './containers/LoginForm';
import RegisterForm from './containers/RegisterForm';
import './App.css';

class App extends React.Component {
  public render() {
    return (
      <Router history={history}>
        <div>
            <Switch>
              <Route path="/login" component={LoginForm} />
              <Route path="/register" component={RegisterForm} />
              <PrivateRoute path="/" component={HomePage}/>
            </Switch>
        </div>
      </Router>
    );
  }
}

export default App;
