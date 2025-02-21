import { createContext, useContext, useState } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isLoggedIn, setLoggedIn] = useState(false);
  const [email, setEmail] = useState('');

  const login = (email) => {
    setLoggedIn(true);
    setEmail(email);
  };

  const logout = () => {
    setLoggedIn(false);
    setEmail('');
  };

  return (
    <AuthContext.Provider value={{ isLoggedIn, email, login, logout  }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }

  return context;
};