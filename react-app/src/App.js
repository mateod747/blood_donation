import React, { useState } from 'react';
import './App.scss';
import Nav from './components/Nav';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Dashboard from './components/Dashboard';
import Login from './components/Login';
import { Redirect } from 'react-router-dom';
import DonationList from './components/DonationList';

function App() {
  const [token, setToken] = React.useState(
    sessionStorage.getItem('loginToken') || ''
  );

  if (token.length < 1) {
    return <Login setToken={setToken} />
  }

  return (
    <Router>
      <div className="App">
        <Nav />
        <Switch>
          <Route path="/dashboard" exact component={Dashboard} />
          <Route path="/donation-list" exact component={DonationList} />
          <Route path="/">
            {token.length > 0 ? <Redirect to="/dashboard" /> : <Dashboard />}
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
