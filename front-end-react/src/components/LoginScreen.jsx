import { motion } from "framer-motion";
import PropTypes from "prop-types";

export default function LoginScreen({ onLogin }) {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-indigo-500 to-purple-600 p-4">
      <motion.div
        initial={{ opacity: 0, y: 40 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.6 }}
        className="bg-white/20 backdrop-blur-xl p-10 rounded-2xl shadow-2xl w-full max-w-sm border border-white/30"
      >
        <h1 className="text-3xl font-bold text-white text-center mb-6">
          Bem-vindo 👋
        </h1>
        <p className="text-white/80 text-center mb-8">
          Faça login para acessar o sistema
        </p>

        <button
          onClick={onLogin}
          className="w-full py-3 rounded-xl bg-white font-semibold text-indigo-600 shadow-lg hover:bg-indigo-100 transition"
        >
          Entrar com Auth0
        </button>
      </motion.div>
    </div>
  );
}

LoginScreen.propTypes = {
  onLogin: PropTypes.node.isRequired,
};
