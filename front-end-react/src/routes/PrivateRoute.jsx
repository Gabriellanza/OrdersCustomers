import { useAuth0 } from "@auth0/auth0-react";
import { Navigate } from "react-router-dom";
import PropTypes from "prop-types";

export function PrivateRoute({ children }) {
  const { isAuthenticated, isLoading } = useAuth0();

  if (isLoading) {
    return <div className="text-center p-4 text-white">Carregando...</div>;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  return children;
}

PrivateRoute.propTypes = {
  children: PropTypes.node.isRequired,
};
