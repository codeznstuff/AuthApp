import React from 'react';
import { Alert } from 'react-bootstrap';
import styles from './PageAlerts.module.scss';
import { useAlerts, IAlert } from '../../../context/AlertsContext/AlertsContext';

const PageAlerts = () => {
  const { alerts } = useAlerts();

  if (!alerts || !alerts.length) return null;

  return (
    <div className={styles.wrapper}>
      <div className={styles.alertsColumn}>
        {alerts.map((alert: IAlert) => {
          return (
            <Alert show variant={alert.variant} key={alert.id}>
              {alert.heading && <Alert.Heading>{alert.heading}</Alert.Heading>}
              {alert.text}
            </Alert>
          );
        })}
      </div>
    </div>
  );
};

export default PageAlerts;
