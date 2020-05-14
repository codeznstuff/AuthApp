import React from 'react';
import { render, RenderResult, cleanup } from '@testing-library/react';
import UnauthenticatedApp from './UnauthenticatedApp';

describe('UnauthenticatedApp', () => {
  let unauthenticatedApp: RenderResult;

  beforeEach(() => {
    unauthenticatedApp = render(<UnauthenticatedApp />);
  });

  afterEach(cleanup);

  it('matches the initial snapshot', () => {
    expect(unauthenticatedApp.container.firstChild).toMatchSnapshot();
  });
});
