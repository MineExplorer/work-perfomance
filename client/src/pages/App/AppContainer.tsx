import React from 'react';

import { AuthProvider } from '../../stores/Auth';
import App from './App';

export default function AppContainer() {
  return (
    <AuthProvider>
    	<App />
    </AuthProvider>
  );
}
