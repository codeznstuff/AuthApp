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
