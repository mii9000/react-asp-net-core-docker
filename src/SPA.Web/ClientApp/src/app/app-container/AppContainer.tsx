import React from 'react';
import logo from './logo.svg';
import { Counter } from '../../features/counter/Counter';
import { Switch, Route, BrowserRouter as Router } from "react-router-dom";
import Login from "../../features/login/Login";
import './AppContainer.css';

const AppContainer = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={Login} />
        <Route exact path="/counter" component={Counter} />
      </Switch>
    </Router>
  )
}

export default AppContainer;
