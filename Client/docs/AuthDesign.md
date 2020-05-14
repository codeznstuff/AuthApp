# React AzureAD Authentication Pattern

The following is a simplified implementation of authentication and authorization for React apps. Authentication is defined as the identification of a user as a valid user. Authorization is defined as the actions that the user is allowed to perform, based on their roles and rights.

This solution uses the following libraries and techniques:

1. [MSAL.js](https://github.com/AzureAD/microsoft-authentication-library-for-js)
1. [OAuth2.0 Implicit Grant Flow](https://docs.microsoft.com/en-us/azure/active-directory/develop/v1-oauth2-implicit-grant-flow)
1. [Authentication in React Applications](https://kentcdodds.com/blog/authentication-in-react-applications)
1. [React Context](https://reactjs.org/docs/context.html)
1. [React Hooks](https://reactjs.org/docs/hooks-overview.html)

Rather than enforcing authentication and authorization through routes that redirect a user when they attempt to access content without authorization. This implementation prevents the rest of the app from being rendered until the user identity and access token is retrieved or it's determined that there is no logged-in user. It can return a spinner until the user identity and access token is retrieved, then the rest of the app will be rendered.

## There following structure make this possible:

### ./src/index.tsx

Loads the Context providers for the app.

```tsx
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
```

### ./src/App.tsx

Uses the useAuth hook to load the AuthenticatedApp if the user identity and access token is retrieved otherwise the UnauthenticatedApp is loaded.

```tsx
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
```

### ./src/utils/authentication.ts

Utilizes the MSAL.js library to login to Azure AD and get the users identity token and the access token based on the configured Client ID.

```ts
import { config } from './config';
import * as Msal from 'msal';
import { authResponseCallback } from 'msal/lib-commonjs/UserAgentApplication';

const msalConfig: Msal.Configuration = {
  auth: { clientId: config.authConfig.clientId, authority: config.authConfig.authority, redirectUri: config.authConfig.redirectUri }
};

// create MSAL msalAgentApplication instance
const msalAgent: Msal.UserAgentApplication = new Msal.UserAgentApplication(msalConfig);

// register Callbacks for redirect flow
const msalRedirectCallBack: authResponseCallback = (authError: Msal.AuthError, authResponse?: Msal.AuthResponse) => {
  if (authResponse) {
    return authResponse;
  } else {
    throw authError;
  }
};
msalAgent.handleRedirectCallback(msalRedirectCallBack);

export const getMsal = async (): Promise<Msal.Account> => {
  try {
    if (!msalAgent.getAccount()) {
      if (window === window.parent && window === window.top && !msalAgent.isCallback(window.location.hash)) {
        await msalAgent.loginRedirect();
      }
      return msalAgent.getAccount();
    } else {
      return msalAgent.getAccount();
    }
  } catch (error) {
    throw error;
  }
};

export const getMsalToken = async (): Promise<any> => {
  const request = {
    scopes: config.scopes
  };
  try {
    const token = await msalAgent.acquireTokenSilent(request);
    if (token.accessToken) {
      return token.accessToken;
    }
  } catch (error) {
    throw error;
  }
};
```

### ./src/context/Authorization/AuthorizationContext.tsx

Establishes the AuthContext and useAuth hook, which contains the user identity and access token. Utilizes react-async to render a loading state, spinner, while retriving the user identity and access token.

```tsx
import React, { createContext, useContext } from 'react';
import { useAsync } from 'react-async';
import { fetchUser, IApplicationUser } from '../../services/AuthorizationApi';
import { getMsal } from '../../utils/authentication';
import { KiewitSpinner } from '../../components/common/KiewitSpinner/KiewitSpinner';

export const AuthorizationContext = createContext<any>({
  currentUser: { UserId: 0 }
});

const loadData = async (): Promise<IApplicationUser> => {
  try {
    // Make API call for MSAL AAD Authentication
    const authUser = await getMsal();
    // Make API call to get user data from Authorization API
    const user = await fetchUser(authUser.accountIdentifier);
    return user;
  } catch (error) {
    return error;
  }
};

export const AuthorizationProvider = (props: any): any => {
  const { data, error, isRejected, isPending } = useAsync({
    promiseFn: loadData
  });

  if (isPending) {
    return <KiewitSpinner />;
  }
  if (isRejected) {
    return <pre>{error}</pre>;
  }
  return <AuthorizationContext.Provider value={data} {...props} />;
};

export const useAuthorization = (): IApplicationUser => {
  const context: any = useContext(AuthorizationContext);
  if (context === undefined) {
    throw new Error(`useAuthorization must be used within an AuthorizationProvider`);
  }
  return context;
};
```

### ./src/context/CASL/AbilityContext.tsx

Sets the permissions for the app.

```tsx
import React, { createContext, useContext } from 'react';
import { createContextualCan } from '@casl/react';
import { AbilityBuilder } from '@casl/ability';
import { useAuthorization } from '../Authorization/AuthorizationContext';
import { IClaimData, IApplicationUser } from '../../services/AuthorizationApi';

// Create Ability Context
export const AbilityContext = createContext(null);

// Create Can context that uses Ability Context
export const Can = createContextualCan(AbilityContext.Consumer);

// Creates the Ability Context Provider
export const AbilityProvider = (props: any): any => {
  const user = useAuthorization();
  const ability = defineRules(user);
  return <AbilityContext.Provider value={ability} {...props} />;
};

// Returns the type for the object passed into Can
const subjectName = (subject: any) => {
  if (!subject || typeof subject === 'string') {
    return subject;
  }
  if (subject.hasOwnProperty('userId')) {
    return 'user';
  }
  return typeof subject;
};

// Establishes the Permissions for the application
const defineRules = (user: IApplicationUser) => {
  return AbilityBuilder.define({ subjectName }, (can: any) => {
    // Manager roles gives full access
    const adminClaim = user.claims ? user.claims.filter((claim: IClaimData) => claim.claimType === 'Role' && claim.claimValue === 'Manager') : [];
    if (adminClaim.length > 0) {
      can(['read', 'create', 'edit', 'delete'], 'user');
      can('manage', 'all');
    }
    // Standard user permissions
    else {
      can(['edit', 'delete'], 'user', { userId: user.userId });
      can(['read', 'create'], 'user');
    }
  });
};

// Context Hook
export const useAbility = (): any => {
  const context: any = useContext(AbilityContext);
  if (context === undefined) {
    throw new Error(`useAbility must be used within an AbilityProvider`);
  }
  return context;
};
```

### ./src/content/index.tsx

Applies the AuthContext context provider as parent component.

```tsx
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
```

### ./src/AuthenticatedApp.tsx

The app for when the user identity and access token is retrieved.

```tsx
import React from 'react';

const AuthenticatedApp = () => {
  return <h1>AuthenticatedApp</h1>;
};

export default AuthenticatedApp;
```

### ./src/UnauthenticatedApp.tsx

The app for when there is no logged-in user.

```tsx
import React from 'react';

const UnauthenticatedApp = () => {
  return <h1>UnauthenticatedApp</h1>;
};

export default UnauthenticatedApp;
```
