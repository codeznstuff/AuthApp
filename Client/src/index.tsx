import React from 'react';
import ReactDOM from 'react-dom';
import './bootstrap';
import App from './App';
import AppProviders from './context';

ReactDOM.render(
  <AppProviders>
    <App />
  </AppProviders>,
  document.getElementById('root')
);
