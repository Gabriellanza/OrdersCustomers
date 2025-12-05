import LoginScreen from "../components/LoginScreen";
import { useAuth } from "../auth/useAuth";

export default function LoginPage() {
  const { loginWithRedirect } = useAuth();

  return <LoginScreen onLogin={() => loginWithRedirect()} />;
}
