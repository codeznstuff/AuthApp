import { alertReducer } from './AlertsContext';

describe('AlertsContext', () => {
  describe('alert reducer', () => {
    it('returns the state when no action.types match', () => {
      const state = alertReducer([{ variant: 'primary', text: 'text' }], { type: 'null' });
      expect(state.length).toBe(1);
    });

    it('adds an alert on action.addAlert', () => {
      const state = alertReducer([], { type: 'addAlert', alert: { variant: 'primary', text: 'text' } });
      expect(state.length).toBe(1);
    });

    it('removes the first alert on action.removeAlert', () => {
      const state = alertReducer([{ variant: 'primary', text: 'text' }], { type: 'removeAlert' });
      expect(state.length).toBe(0);
    });
  });
});
