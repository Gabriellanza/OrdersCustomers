import { useAuth } from "../auth/useAuth";

export function UserProfile() {
  const { user, isAuthenticated } = useAuth();

  if (!isAuthenticated) return null;

  return (
    <div className="p-4 bg-gray-100 rounded-lg shadow-md">
      <img
        src={user.picture}
        alt={user.name}
        className="w-16 h-16 rounded-full mb-2"
      />
      <p>
        <strong>Nome:</strong> {user.name}
      </p>
      <p>
        <strong>Email:</strong> {user.email}
      </p>
    </div>
  );
}
