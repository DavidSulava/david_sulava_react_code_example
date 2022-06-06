import React from 'react';
import App from './App';
import { store } from './stores/configureStore';
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom';

ReactDOM.render(
    <Provider store={store}>
      <App/>
    </Provider>,
  document.getElementById('root')
);

