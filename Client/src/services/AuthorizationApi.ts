import { AxiosInstance } from 'axios';
import { authorizationAxios } from './AxiosInstances';
import { handleError } from '../utils/handleError';

export const fetchUser = async (userId: string): Promise<IApplicationUser> => {
  try {
    const http: AxiosInstance = await authorizationAxios();
    const response = await http.get('/api/Applications/GetApplicationUser', {
      params: {
        applicationName: 'App',
        userId: userId
      }
    });
    return response.data;
  } catch (e) {
    handleError(e);
    throw new Error(`AuthorizationAPI.fetchUser error: ${e.message}`);
  }
};

export const fetchUsers = async (): Promise<Array<IApplicationUser>> => {
  try {
    const http: AxiosInstance = await authorizationAxios();
    const response = await http.get('/api/Applications/GetApplicationUsers', {
      params: {
        applicationName: 'App'
      }
    });
    return response.data;
  } catch (e) {
    handleError(e);
    throw new Error(`AuthorizationAPI.fetchUser error: ${e.message}`);
  }
};

export interface IApplicationUser {
  userId: string;
  displayName: string;
  firstName: string;
  lastName: string;
  emailAddress: string;
  claims: Array<IClaimData>;
}

export interface IClaimData {
  claimType: string;
  claimValue: string;
}
