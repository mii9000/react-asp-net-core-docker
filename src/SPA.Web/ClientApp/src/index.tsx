import React from 'react';
import ReactDOM from 'react-dom';
import AppContainer from './app/app-container/AppContainer';
import { store } from './app/store';
import { Provider } from 'react-redux';
import './index.css';
import 'semantic-ui-css/semantic.min.css'

ReactDOM.render(
  <Provider store={store}>
    <AppContainer />
  </Provider>,
  document.getElementById('root')
);
