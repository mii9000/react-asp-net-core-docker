import React from 'react';
import { Switch, Redirect, Route, BrowserRouter as Router, RouteProps } from "react-router-dom";
import Login from "../../features/login/Login";
import { Container, Grid, GridRow, GridColumn } from "semantic-ui-react";
import { SemanticToastContainer } from 'react-semantic-toasts';
import { UserGroupContainer } from "../../features/user-group/UserGroupContainer";
import './AppContainer.css';
import 'react-semantic-toasts/styles/react-semantic-alert.css';

interface PrivateRouteProps extends RouteProps {
  // tslint:disable-next-line:no-any
  component?: any;
  // tslint:disable-next-line:no-any
  children?: any;
}

const ProtectedRoute = (props: PrivateRouteProps) => {
  const { component: Component, children, ...rest } = props;
  const token = localStorage.getItem('token');
  return (
    <Route
      {...rest}
      render={routeProps =>
        token ? (
          Component ? (
            <Component {...routeProps} />
          ) : (
              children
            )
        ) : (
            <Redirect
              to={{
                pathname: '/login',
                state: { from: routeProps.location },
              }}
            />
          )
      }
    />
  );
};

const AppContainer = () => {
  return (
    <Container>
      <SemanticToastContainer />
      <Grid>
        <GridColumn>
          <GridRow className='container-row'>
            <Router>
              <Switch>
                <Route path="/login" component={Login} />
                <ProtectedRoute exact path="/" component={UserGroupContainer} />
              </Switch>
            </Router>
          </GridRow>
        </GridColumn>
      </Grid>
    </Container>
  )
}

export default AppContainer;
