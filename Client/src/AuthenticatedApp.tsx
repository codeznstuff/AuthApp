import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import ManageUsers from './components/containers/ManageUsers/ManageUsers';
import { Header } from './components/common/Header/Header';

const AuthenticatedApp = () => {
  return (
    <>
      <Router>
        <Header />
        <ManageUsers />
      </Router>
    </>
  );
};

export default AuthenticatedApp;
