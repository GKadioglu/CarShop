import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthContext";

function PrivateRoute({ children, roles }) {
  const { role } = useAuth(); // Rol bilgisini bağlamdan al

  // Kullanıcının rolü uygun değilse hata sayfasına yönlendir
  if (!role || !roles.includes(role)) {
    return <Navigate to="/unauthorized" />;
  }

  // Kullanıcı giriş yapmış ve uygun role sahip
  return children;
}

export default PrivateRoute;