import React from 'react';
import { render, RenderResult, cleanup } from '@testing-library/react';
import App from './App';

describe('App', () => {
  let app: RenderResult;

  beforeEach(() => {
    app = render(<App />);
  });

  afterEach(cleanup);

  it('matches the initial snapshot', () => {
    expect(app.container.firstChild).toMatchSnapshot();
  });
});
