import React from 'react';
import { render, RenderResult, cleanup } from '@testing-library/react';
import { AuthorizationContext } from './AuthorizationContext';

describe('AuthorizationContext', () => {
  let authorizationContext: RenderResult;
  const data: any = { user: { UserId: 0, Name: 'User0' } };

  beforeEach(() => {
    authorizationContext = render(<AuthorizationContext.Provider value={data} />);
  });

  afterEach(cleanup);

  it('matches the initial snapshot', () => {
    expect(authorizationContext.container.firstChild).toMatchSnapshot();
  });
});
