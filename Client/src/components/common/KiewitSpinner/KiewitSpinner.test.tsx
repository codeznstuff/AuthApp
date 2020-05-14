import React from 'react';
import { render } from '@testing-library/react';
import { KiewitSpinner } from './KiewitSpinner';

describe('KiewitSpinner', () => {
  it('does not render when show prop is false', () => {
    const spinner = render(<KiewitSpinner show={false} />);
    expect(spinner.container.firstChild).toBeNull();
  });

  it('renders when show is true or not passsed in', () => {
    const spinner = render(<KiewitSpinner />);
    expect(spinner.container.firstChild).not.toBeNull();
    expect(spinner.container.firstChild).toMatchSnapshot();
  });
});
