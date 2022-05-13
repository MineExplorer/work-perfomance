import React from 'react';
import { Box, CircularProgress, Typography } from '@mui/material';

interface LoadingProps {
  message?: string;
}

export default function Loading(props: LoadingProps) {
  const { message = 'Loading...' } = props;

  return (
    <Box height="100vh" display="flex" flexDirection="column" alignItems="center" justifyContent="center">
      <Box margin={2}>
        <CircularProgress />
      </Box>
      {message ? <Typography>{message}</Typography> : null}
    </Box>
  );
}
