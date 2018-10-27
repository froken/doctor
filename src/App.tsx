import * as React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import './App.css';
import { PrivateRoute } from './components/PrivateRoute';
import { HomePage } from './containers/HomePage';
import LoginPage from './containers/LoginPage';
import RegisterForm from './containers/RegisterForm';

class App extends React.Component {
  public render() {
    return (
      <BrowserRouter>
        <div>
            <Switch>
              <Route path="/login" component={LoginPage} />
              <Route path="/register" component={RegisterForm} />
              <PrivateRoute path="/" component={HomePage}/>
            </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
