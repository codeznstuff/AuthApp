import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import { getMsalToken } from '../utils/authentication';
import { config as appConfig } from '../utils/config';
import { handleError } from '../utils/handleError';

export const authorizationAxios = async (): Promise<AxiosInstance> => {
  const headers: any = {
    'Content-Type': 'application/json',
    'Cache-Control': 'no-cache',
    Pragma: 'no-cache'
  };
  //const token = await getMsalToken();

  // if (token) {
  //   headers.Authorization = `Bearer ${token}`;
  // }

  return axios.create({
    baseURL: appConfig.authorizationBaseUrl,
    timeout: 15000,
    headers
  });
};

export const setAxiosDefaults = (): void => {
  axios.defaults.headers.common['Content-Type'] = 'application/json';
  axios.defaults.headers.common['Cache-Control'] = 'no-cache';
  axios.defaults.headers.common['Pragma'] = 'no-cache';
  axios.defaults.baseURL = appConfig.apiBaseUrl;
  axios.defaults.timeout = 15000;

  axios.interceptors.request.use(
    async (config: AxiosRequestConfig) => {
      if (config.baseURL === appConfig.apiBaseUrl && !config.headers.Authorization) {
        const token: string = await getMsalToken();
        config.headers.Authorization = `Bearer ${token}`;
      }
      if (config.method === 'get') {
        if (!config.params) {
          config.params = {};
        }
        config.params._ = new Date().getTime();
        config.params.format = 'json';
      }
      return config;
    },
    error => {
      handleError(error);
      return Promise.reject(error);
    }
  );
};
