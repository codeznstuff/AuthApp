import { CacheLocation } from 'msal';

const configjson = require('./env-config.json') as Array<IConfig>;

interface IConfig {
  env: string;
  uiHostname: string;
  authConfig: {
    clientId: string;
    authority: string;
    tenant: string;
    redirectUri: string;
    cacheLocation: CacheLocation;
  };
  apiClientId: string;
  scopes: string[];
  apiBaseUrl: string;
  authorizationBaseUrl: string;
}
const hostname = window && window.location && window.location.hostname;
const config: IConfig = configjson.find(i => {
  return i.uiHostname === hostname;
}) || {
  env: 'Config error',
  uiHostname: hostname,
  authConfig: {
    clientId: 'Config error',
    authority: 'Config error',
    tenant: 'Config error',
    redirectUri: 'Config error',
    cacheLocation: 'sessionStorage'
  },
  scopes: [],
  apiClientId: 'Config error',
  apiBaseUrl: 'config error',
  authorizationBaseUrl: 'config error'
};
if (config.env === 'Config error') {
  // eslint-disable-next-line no-console
  console.error('Unable to find valid config for host');
}

export { config };
