import { Navigate } from "react-router-dom";
import { useAuth } from "../components/AuthContext";

function PrivateRoute({ children }) {
  const { isLoggedIn } = useAuth();
  return isLoggedIn ? children : <Navigate to="/login" replace />;
}
export default PrivateRoute;