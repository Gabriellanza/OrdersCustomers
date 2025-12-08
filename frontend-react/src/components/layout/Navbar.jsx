import { useAuth0 } from '@auth0/auth0-react';
import { LogOut, User } from 'lucide-react';

const Navbar = () => {
    const { user, logout, isAuthenticated } = useAuth0();

    if (!isAuthenticated) return null;
    console.log(user);
    return (
        <header className="h-16 bg-white border-b border-gray-200 flex items-center justify-between px-6 sticky top-0 z-10">
            <h2 className="text-lg font-semibold text-gray-800">Bem-Vindo, {user?.given_name} {user?.family_name} </h2>
            <div className="flex items-center gap-4">
                {user?.picture ? (
                    <img src={user.picture} alt={user.name} className="w-8 h-8 rounded-full border border-gray-200" />
                ) : (
                    <div className="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center">
                        <User size={20} className="text-gray-500" />
                    </div>
                )}
                <button
                    onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
                    className="flex items-center gap-2 text-sm text-gray-600 hover:text-red-600 transition-colors"
                    title="Logout"
                >
                    <LogOut size={18} />
                </button>
            </div>
        </header>
    );
};

export default Navbar;
