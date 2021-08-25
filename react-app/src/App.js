import React from 'react';
import './App.scss';
import Nav from './components/Nav';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Dashboard from './components/Dashboard';
import Login from './components/Login';
import { Redirect } from 'react-router-dom';
import DonationList from './components/DonationList';
import AdminPage from './components/AdminPage';

function App() {
  const [token, setToken] = React.useState(
    sessionStorage.getItem('loginToken') || ''
  );

  const [admin, setAdmin] = React.useState(
    sessionStorage.getItem('admin') || ''
  );

  if (token.length < 1) {
    return <Login setToken={setToken} />
  }
  
  if(admin === "true") {
    return <AdminPage />
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
