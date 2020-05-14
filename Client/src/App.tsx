import React from 'react';
import { useAuthorization } from './context/Authorization/AuthorizationContext';
import AuthenticatedApp from './AuthenticatedApp';
import UnauthenticatedApp from './UnauthenticatedApp';
import { IApplicationUser } from './services/AuthorizationApi';

const App = () => {
  const user: IApplicationUser = useAuthorization();
  return user.userId ? <AuthenticatedApp /> : <UnauthenticatedApp />;
};

export default App;
