import React, { createContext, useContext } from 'react';
import { useAsync } from 'react-async';
import { fetchUser, IApplicationUser } from '../../services/AuthorizationApi';
import { getMsal } from '../../utils/authentication';
import { KiewitSpinner } from '../../components/common/KiewitSpinner/KiewitSpinner';
import UnauthenticatedApp from '../../UnauthenticatedApp';

export const AuthorizationContext = createContext<any>({
  currentUser: { UserId: 0 }
});

const loadData = async (): Promise<IApplicationUser | Error> => {
  try {
    // Make API call for MSAL AAD Authentication
    //const authUser = await getMsal();
    // Make API call to get user data from Authorization API
    //const user = await fetchUser(authUser.accountIdentifier);
    const user = await fetchUser('be64b27b-59b6-43bb-a824-51c16d328ffb');
    return user;
  } catch (error) {
    const ex: Error = error;
    return ex;
  }
};

export const AuthorizationProvider = (props: any): any => {
  const { data, isPending } = useAsync({
    promiseFn: loadData
  });

  if (isPending) {
    return <KiewitSpinner />;
  }
  if (data instanceof Error) {
    return <UnauthenticatedApp />;
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
