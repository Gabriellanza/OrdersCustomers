import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Auth0ProviderWithNavigate } from "./auth/Auth0ProviderWithNavigate";

import { LoginButton } from "./components/Loginbutton";
import { LogoutButton } from "./components/LogoutButton";
import { UserProfile } from "./components/UserProfile";
import LoginPage from "./components/LoginPage";

import { PrivateRoute } from "./routes/PrivateRoute";

import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Auth0ProviderWithNavigate>
        <Routes>
          <Route path="/login" element={<LoginPage />} />

          <Route
            path="/"
            element={
              <PrivateRoute>
                <div className="p-4">
                  <LoginButton />
                  <LogoutButton />
                  <UserProfile />
                </div>
              </PrivateRoute>
            }
          />
        </Routes>
      </Auth0ProviderWithNavigate>
    </BrowserRouter>
  );
}

export default App;
