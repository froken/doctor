import * as React from 'react';
import './App.css';
import RegisterPage from './containers/RegisterPage';

class App extends React.Component {
  public render() {
    return (
        <div className="App">
            <RegisterPage />
        </div>
    );
  }
}

export default App;
