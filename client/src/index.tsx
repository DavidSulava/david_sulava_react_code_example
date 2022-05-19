import React from 'react';
import App from './App';
import { configureStore } from './stores/configureStore';
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom';

const store = configureStore()


ReactDOM.render(
    <Provider store={store}>
      <App/>
    </Provider>,
  document.getElementById('root')
);

