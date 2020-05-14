import React, { createContext, useContext, useReducer, useCallback } from 'react';

export interface IAlert {
  variant: 'primary' | 'secondary' | 'success' | 'danger' | 'warning' | 'info' | 'dark' | 'light';
  heading?: string;
  text: string;
  id?: string;
}

interface IAlertAction {
  type: string;
  alert?: IAlert;
}

export const AlertsContext = createContext([]);

export const alertReducer = (state: Array<IAlert>, action: IAlertAction) => {
  switch (action.type) {
    case 'addAlert':
      return [...state, action.alert];
    case 'removeAlert': {
      const newState: Array<IAlert> = [...state];
      newState.shift();
      return newState;
    }
    default:
      return state;
  }
};

export const AlertsProvider = (props: any): any => {
  const [alerts, dispatch] = useReducer(alertReducer, []);
  const ALERT_TIMEOUT = 5000;

  const setRemoveTimeout = () => {
    setTimeout(() => {
      dispatch({ type: 'removeAlert' });
    }, ALERT_TIMEOUT);
  };

  const createAlert = useCallback((alert: IAlert) => {
    alert.id = `${Date.now()}-${alert.text}`;
    dispatch({ type: 'addAlert', alert });
    setRemoveTimeout();
  }, []);

  const context = {
    alerts,
    createAlert
  };

  return <AlertsContext.Provider value={context} {...props} />;
};

export const useAlerts = () => {
  const context: any = useContext(AlertsContext);
  if (context === undefined) {
    throw new Error(`useAlerts must be used within a AlertsProvider`);
  }
  return context;
};
