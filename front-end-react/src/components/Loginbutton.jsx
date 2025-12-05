import { useAuth } from "../auth/useAuth";

export function LoginButton() {
  const { loginWithRedirect, isLoading } = useAuth();

  if (isLoading) return <button disabled>Carregando...</button>;

  return (
    <button
      className="px-4 py-2 bg-blue-600 text-white rounded-lg"
      onClick={() => loginWithRedirect()}
    >
      Entrar
    </button>
  );
}
