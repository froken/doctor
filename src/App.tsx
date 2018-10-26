import * as React from 'react';
import { BrowserRouter, Redirect, Route } from 'react-router-dom';
import './App.css';
import { LoginForm } from './components/LoginForm';
// import { PrivateRoute } from './components/PrivateRoute';
import { HomePage } from './containers/HomePage';
import RegisterPage from './containers/RegisterPage';

class App extends React.Component {
  public render() {
    const isUserLoggedIn = false;
    const homeRender = (props: any) => (isUserLoggedIn ? (<HomePage {...props} />) : (<Redirect to="/login"/>));
    

    return (
      <BrowserRouter>
        <div>
            <Route path="/" render={homeRender}/>
            <Route path="/register" component={RegisterPage}/>
            <Route path="/login" component={LoginForm} />
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
