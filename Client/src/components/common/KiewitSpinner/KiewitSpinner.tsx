import React from 'react';
import styles from './KiewitSpinner.module.scss';
import Spinner from 'react-bootstrap/Spinner';

interface SpinnerProps {
  animation?: 'border' | 'grow';
  role?: string;
  show?: boolean;
}

/* You can either toggle the "show" prop or just return the component without it */
export const KiewitSpinner = ({ animation = 'border', role = 'status', show = true, ...props }: SpinnerProps) => {
  if (!show) return null;

  return (
    <div className={styles.spinnerContainer}>
      <Spinner animation={animation} role={role} {...props} />
    </div>
  );
};
