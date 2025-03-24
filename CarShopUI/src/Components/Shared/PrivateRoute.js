import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthContext";

function PrivateRoute({ children, roles }) {
  const { role } = useAuth(); 

  if (!role || !roles.includes(role)) {
    return <Navigate to="/unauthorized" />;
  }

  return children;
}

export default PrivateRoute;