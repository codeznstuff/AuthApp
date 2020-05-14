import React from 'react';
import { render, RenderResult, cleanup } from '@testing-library/react';
import PageAlerts from './PageAlerts';
import { AlertsContext, IAlert } from '../../../context/AlertsContext/AlertsContext';

describe.only('PageAlerts', () => {
  let pageAlerts: RenderResult;
  afterEach(cleanup);

  describe('when alerts exist', () => {
    beforeEach(() => {
      const alerts: Array<IAlert> = [{ variant: 'success', text: 'success', id: 1 }, { variant: 'warning', text: 'warning', id: 2 }];
      const context: any = {
        alerts,
        createAlert: () => {}
      };

      pageAlerts = render(
        <AlertsContext.Provider value={context}>
          <PageAlerts />
        </AlertsContext.Provider>
      );
    });

    it('matches snapshot', () => {
      expect(pageAlerts.container.firstChild).toMatchSnapshot();
    });
  });

  describe('when alerts do not exist', () => {
    beforeEach(() => {
      const alerts: Array<IAlert> = [];
      const context: any = {
        alerts,
        createAlert: () => {}
      };

      pageAlerts = render(
        <AlertsContext.Provider value={context}>
          <PageAlerts />
        </AlertsContext.Provider>
      );
    });

    it('matches snapshot', () => {
      expect(pageAlerts.container.firstChild).toMatchSnapshot();
    });
  });
});
