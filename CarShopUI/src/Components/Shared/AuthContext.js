import React, { createContext, useState, useContext } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(
    !!localStorage.getItem("jwtToken")
  );
  const [userName, setUserName] = useState(
    localStorage.getItem("userName") || ""
  );
  const [role, setRole] = useState(localStorage.getItem("role") || "");
  const [token, setToken] = useState(localStorage.getItem("jwtToken") || "");
  const [email, setEmail] = useState(localStorage.getItem("email") || ""); // E-mail ekleniyor


  const login = (userName, role, token, email) => {
    setIsAuthenticated(true);
    setUserName(userName);
    setRole(role);
    setToken(token);
    setEmail(email); // E-mail'i de set ediyoruz
    localStorage.setItem("jwtToken", token);
    localStorage.setItem("role", role);
    localStorage.setItem("userName", userName);
    localStorage.setItem("email", email); // E-mail'i localStorage'a ekliyoruz
  };

  const logout = () => {
    setIsAuthenticated(false);
    setUserName("");
    setRole("");
    setToken("");
    setEmail(""); // E-mail'i sıfırlıyoruz
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("role");
    localStorage.removeItem("userName");
    localStorage.removeItem("email"); // E-mail'i de localStorage'dan siliyoruz

  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        userName,
        role,
        email,
        token,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
