import '@testing-library/jest-dom/extend-expect';
import '@testing-library/react/cleanup-after-each';
// this is just a little hack to silence a warning that we'll get until react
// fixes this: https://github.com/facebook/react/pull/14853
// eslint-disable-next-line no-console
const originalError = console.error;
beforeAll(() => {
  global.document.createRange = () => ({
    setStart: () => {},
    setEnd: () => {},
    commonAncestorContainer: {
      nodeName: 'BODY',
      ownerDocument: document
    }
  });

  // eslint-disable-next-line no-console
  console.error = (...args) => {
    if (/Warning.*not wrapped in act/.test(args[0])) {
      /* originalError.call(console, 'Waiting on async act - https://github.com/facebook/react/pull/14853 '); */
      return;
    }
    originalError.call(console, ...args);
  };
});

afterAll(() => {
  // eslint-disable-next-line no-console
  console.error = originalError;
});
