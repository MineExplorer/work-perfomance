import React, { createContext } from 'react';
import { useLocalObservable } from 'mobx-react-lite';

import { AuthStore } from './AuthStore';
import Loading from '../../components/Loading';
import { observer } from 'mobx-react';

export const AuthContext = createContext<AuthStore>(null);

export const AuthProvider = observer(({ children }) => {
  const store = useLocalObservable(() => new AuthStore());
  const { state } = store;

  return (
    <AuthContext.Provider value={store}>
      {state === 'loading' ? <Loading /> : null}
      {state === 'error' ? 'An error occurred during loading user data, please try again later.' : null}
      {state === 'loaded' ? children : null}
    </AuthContext.Provider>
  );
});
