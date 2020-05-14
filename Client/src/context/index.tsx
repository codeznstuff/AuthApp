import React from 'react';
import { AuthorizationProvider } from './Authorization/AuthorizationContext';
import { AlertsProvider } from './AlertsContext/AlertsContext';
import { AbilityProvider } from './CASL/AbilityContext';

const AppProviders = ({ children }: any) => {
  return (
    <AuthorizationProvider>
      <AbilityProvider>
        <AlertsProvider>{children}</AlertsProvider>
      </AbilityProvider>
    </AuthorizationProvider>
  );
};

export default AppProviders;
