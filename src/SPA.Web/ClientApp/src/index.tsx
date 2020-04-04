import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import AppContainer from './app/app-container/AppContainer';
import { store } from './app/store';
import { Provider } from 'react-redux';

ReactDOM.render(
  <Provider store={store}>
    <AppContainer />
  </Provider>,
  document.getElementById('root')
);
