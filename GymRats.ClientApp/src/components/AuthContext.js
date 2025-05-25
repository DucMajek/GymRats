
import { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {

  const tokenFromStorage = localStorage.getItem('token');
  const emailFromStorage = localStorage.getItem('email');

  const [token, setToken]         = useState(tokenFromStorage);
  const [email, setEmail]         = useState(emailFromStorage || '');
  const [isLoggedIn, setLoggedIn] = useState(!!tokenFromStorage);


  useEffect(() => {
    if (token) {
      localStorage.setItem('token', token);
      localStorage.setItem('email', email);
    } else {
      localStorage.removeItem('token');
      localStorage.removeItem('email');
    }
  }, [token, email]);

  const login = (emailArg, tokenArg) => {
    setLoggedIn(true);
    setEmail(emailArg);
    setToken(tokenArg);
  };

  const logout = () => {
    setLoggedIn(false);
    setEmail('');
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ isLoggedIn, email, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth musi być użyte wewnątrz AuthProvider");
  return ctx;
};
