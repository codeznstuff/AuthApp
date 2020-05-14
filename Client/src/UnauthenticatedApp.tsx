import React from 'react';

const UnauthenticatedApp = () => {
  const outer = {
    width: '100%',
    height: '100vh',
    display: 'flex'
  };
  const inner = {
    margin: 'auto'
  };

  return (
    <div id="outer" className="container" style={outer}>
      <div id="inner" style={inner}>
        <h1 className="display-2 text-center">Sorry</h1>
        <p className="lead text-center">Looks like something went wrong on our end.</p>
        <p className="lead text-center">Please check your network connection and try back later.</p>
      </div>
    </div>
  );
};

export default UnauthenticatedApp;
