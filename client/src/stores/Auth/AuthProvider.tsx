import React, { createContext, useEffect, useState } from 'react';
import { useLocalObservable } from 'mobx-react-lite';

import { AuthStore } from './AuthStore';
import Loading from '../../components/Loading';

export const AuthContext = createContext<AuthStore>(null);

export const AuthProvider = ({ children }) => {
  const store = useLocalObservable(() => new AuthStore());
  const [state, setState] = useState(store.state);

  useEffect(() => {
    store.fetchUserInfo()
      .then(() => setState(store.state));
  });

  return (
    <AuthContext.Provider value={store}>
      {state === 'loading' ? <Loading /> : null}
      {state === 'error' ? 'An error occurred during loading user data, please try again later.' : null}
      {state === 'loaded' ? children : null}
    </AuthContext.Provider>
  );
};
