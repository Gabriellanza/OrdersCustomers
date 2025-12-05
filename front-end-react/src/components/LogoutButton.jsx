import { useAuth } from "../auth/useAuth";

export function LogoutButton() {
  const { logout } = useAuth();

  return (
    <button
      className="px-4 py-2 bg-red-600 text-white rounded-lg"
      onClick={() =>
        logout({ logoutParams: { returnTo: window.location.origin } })
      }
    >
      Sair
    </button>
  );
}
