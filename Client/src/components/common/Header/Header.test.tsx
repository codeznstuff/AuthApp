import React from 'react';
import { render, RenderResult, cleanup } from '@testing-library/react';
import { Header } from './Header';
import { AuthorizationContext } from '../../../context/Authorization/AuthorizationContext';
import { BrowserRouter } from 'react-router-dom';
import { AbilityContext } from '../../../context/CASL/AbilityContext';
import { AbilityBuilder } from '@casl/ability';

describe('Header', () => {
  let header: RenderResult;
  afterEach(cleanup);

  describe('when user is not an account admin', () => {
    beforeEach(() => {
      const data: any = { user: { userId: 0 } };
      const ability = AbilityBuilder.define((can: any) => {
        can(['read'], 'user');
      });
      header = render(
        <BrowserRouter>
          <AuthorizationContext.Provider value={data}>
            <AbilityContext.Provider value={ability}>
              <Header />
            </AbilityContext.Provider>
          </AuthorizationContext.Provider>
        </BrowserRouter>
      );
    });

    it('matches snapshot', () => {
      expect(header.container.firstChild).toMatchSnapshot();
    });
  });

  describe('when user is an account admin', () => {
    beforeEach(() => {
      const data: any = { user: { userId: 0 } };
      const ability = AbilityBuilder.define((can: any) => {
        can(['delete'], 'user');
      });
      header = render(
        <BrowserRouter>
          <AuthorizationContext.Provider value={data}>
            <AbilityContext.Provider value={ability}>
              <Header />
            </AbilityContext.Provider>
          </AuthorizationContext.Provider>
        </BrowserRouter>
      );
    });

    it('matches snapshot', () => {
      expect(header.container.firstChild).toMatchSnapshot();
    });
  });
});
