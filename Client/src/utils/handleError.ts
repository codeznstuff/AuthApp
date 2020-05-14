export const handleError = (error: any) => {
  // Let's be clear: this is a bad error handler.
  // However, we can centralize it here, and call handleError
  // elsewhere, and then later on (if we log them or handle them better),
  //  we can update it here rather then in all those other files.
  // eslint-disable-next-line no-console
  console.log(error);
};
