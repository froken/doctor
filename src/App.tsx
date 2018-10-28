import * as React from 'react';
import { Route, Router, Switch } from 'react-router-dom';
import { history } from "./actions/history";
import './App.css';
import { PrivateRoute } from './components/PrivateRoute';
import HomePage from './containers/HomePage';
import LoginPage from './containers/LoginPage';
import RegisterForm from './containers/RegisterForm';

class App extends React.Component {
  public render() {
    return (
      <Router history={history}>
        <div>
            <Switch>
              <Route path="/login" component={LoginPage} />
              <Route path="/register" component={RegisterForm} />
              <PrivateRoute path="/" component={HomePage}/>
            </Switch>
        </div>
      </Router>
    );
  }
}

export default App;
