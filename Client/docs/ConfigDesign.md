# React Configuration Pattern

React configuration with process.env is compile-time. Following is to provide a release pipeline solution. This config.ts can be imported to access configuration data throughout the app.

## There following structure make this possible:

### ./src/utils/config.ts

This returns a configuration object with the following defined data.

```ts
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
```

### ./src/utils/env-config.json

This is the config file that can also be used by pipelines.

```json
[
  {
    "env": "localdev",
    "uiHostname": "localhost",
    "authConfig": {
      "clientId": "",
      "authority": "",
      "tenant": "",
      "redirectUri": "",
      "cacheLocation": ""
    },
    "scopes": [""],
    "apiClientId": "",
    "apiBaseUrl": "https://localhost:5001",
    "authorizationBaseUrl": "https://localhost:5001"
  }
]
```
