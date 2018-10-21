import * as React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import './App.css';
import { PrivateRoute } from './components/PrivateRoute';
import RegisterPage from './containers/RegisterPage';
import { HomePage } from './pages/HomePage';
import { LoginPage } from './pages/LoginPage';

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
