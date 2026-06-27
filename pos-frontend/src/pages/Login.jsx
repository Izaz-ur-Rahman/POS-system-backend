import { useState } from "react";
import { motion } from "framer-motion";
import { FiMail, FiLock } from "react-icons/fi";
import api from "../api/axios";
import { useNavigate } from "react-router-dom";
import "./login.css";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");

    try {
      const res = await api.post("/auth/login", {
        email,
        password,
      });

      localStorage.setItem("token", res.data.token);

      navigate("/dashboard");
    } catch (err) {
      setError(
        err?.response?.data || "Invalid email or password"
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-wrapper">

      {/* LEFT BRAND PANEL */}
      <motion.div
        className="login-left"
        initial={{ x: -100, opacity: 0 }}
        animate={{ x: 0, opacity: 1 }}
        transition={{ duration: 0.6 }}
      >
        <h1>POS SYSTEM</h1>
        <p>Inventory • Sales • Reports • Analytics</p>

        <div className="glow-circle"></div>
      </motion.div>

      {/* RIGHT LOGIN PANEL */}
      <motion.div
        className="login-right"
        initial={{ x: 100, opacity: 0 }}
        animate={{ x: 0, opacity: 1 }}
        transition={{ duration: 0.6 }}
      >
        <form className="login-card" onSubmit={handleLogin}>
          <h2>Welcome Back</h2>
          <p>Login to continue</p>

          {/* ERROR BOX */}
          {error && (
            <motion.div
              className="error-box"
              initial={{ opacity: 0, y: -10 }}
              animate={{ opacity: 1, y: 0 }}
            >
              {error}
            </motion.div>
          )}

          {/* EMAIL */}
          <div className="input-box">
            <FiMail />
            <input
              type="email"
              placeholder="Email address"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          {/* PASSWORD */}
          <div className="input-box">
            <FiLock />
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          {/* BUTTON */}
          <button type="submit" disabled={loading}>
            {loading ? <span className="loader"></span> : "Login"}
          </button>

          <p className="footer-text">
            Secure POS Login • Admin Access Only
          </p>
        </form>
      </motion.div>
    </div>
  );
}