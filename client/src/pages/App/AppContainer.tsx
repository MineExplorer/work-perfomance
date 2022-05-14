import { useLocalObservable } from 'mobx-react';
import React, { createContext } from 'react';

import { AuthProvider } from '../../stores/Auth';
import { DateIntervalStore } from '../../stores/DateIntervalStore';
import App from './App';

export const dateIntervalStore = new DateIntervalStore();

export const DateIntervalContext = createContext(dateIntervalStore);

export default function AppContainer() {
  return (
    <AuthProvider>
      <DateIntervalContext.Provider value={dateIntervalStore}>
    	  <App />
      </DateIntervalContext.Provider>
    </AuthProvider>
  );
}
