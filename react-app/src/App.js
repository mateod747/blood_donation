import React, { useState } from 'react';
import './App.css';
import Nav from './components/Nav';
import { BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import Dashboard from './components/Dashboard';
import Login from './components/Login';

function App() {
  const [token, setToken] = React.useState(
    sessionStorage.getItem('loginToken') || ''
  );

  if(token.length < 1) {
    return <Login setToken={setToken} />
  }

  return (
    <Router>
      <div className="App">
        <Nav />
        <Switch>
          <Route path="/dashboard" exact component={Dashboard} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
