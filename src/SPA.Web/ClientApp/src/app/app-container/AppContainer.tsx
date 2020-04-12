import React from 'react';
import logo from './logo.svg';
import { Counter } from '../../features/counter/Counter';
import { Switch, Route, BrowserRouter as Router } from "react-router-dom";
import Login from "../../features/login/Login";
import { Container, Grid, GridRow, GridColumn } from "semantic-ui-react";
import { SemanticToastContainer } from 'react-semantic-toasts';
import { UserGroupContainer } from "../../features/user-group/UserGroupContainer";
import './AppContainer.css';
import 'react-semantic-toasts/styles/react-semantic-alert.css';

const AppContainer = () => {
  return (
    <Container>
      <SemanticToastContainer />
      <Grid>
        <GridColumn>
          <GridRow className='container-row'>
            <Router>
              <Switch>
                <Route exact path="/" component={Login} />
                <Route path="/user-groups" component={UserGroupContainer} />
              </Switch>
            </Router>
          </GridRow>
        </GridColumn>
      </Grid>
    </Container>
  )
}

export default AppContainer;
