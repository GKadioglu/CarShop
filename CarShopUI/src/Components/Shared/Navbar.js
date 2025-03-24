import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Css/Navbar.css";
import { useAuth } from "./AuthContext";
import logo from "../Images/car shop (2)-Photoroom.png";

function Navbar() {
  const [searchTerm, setSearchTerm] = useState("");
  const [menuOpen, setMenuOpen] = useState(false); 
  const navigate = useNavigate();
  const { isAuthenticated, logout, userName, role } = useAuth();

  useEffect(() => {}, [isAuthenticated, userName, role]);

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleSearchSubmit = (event) => {
    event.preventDefault();
    navigate(`/search?query=${searchTerm}`);
    setMenuOpen(false); 
  };

  const handleLogout = () => {
    logout();
    window.location.reload();
    navigate("/");
  };

  const toggleMenu = () => {
    setMenuOpen(!menuOpen);
  };

  return (
    <nav className={`navbar ${menuOpen ? "active" : ""}`}>
      {/* Hamburger Menü */}
      <div className="hamburger" onClick={toggleMenu}>
        <div></div>
        <div></div>
        <div></div>
      </div>

      <div className={`navbar-left ${menuOpen ? "open" : ""}`}>
        <ul className="navbar-list">
          <li>
            <Link className="navbar-link-left" to="/" onClick={() => setMenuOpen(false)}>
              Ana Sayfa
            </Link>
          </li>
          <li>
            <Link className="navbar-link-left" to="/category" onClick={() => setMenuOpen(false)}>
              Kategoriler
            </Link>
          </li>
          <li>
            <Link className="navbar-link-left" to="/about" onClick={() => setMenuOpen(false)}>
              Hakkımızda
            </Link>
          </li>
        </ul>
      </div>

      <div className="navbar-logo" style={{ cursor: "default" }}>
        <Link to="/" onClick={() => setMenuOpen(false)}>
          <img src={logo} alt="Logo" />
        </Link>
      </div>

      <div className={`navbar-right ${menuOpen ? "open" : ""}`}>
        {isAuthenticated ? (
          <>
            {role === "admin" ? (
              <Link className="navbar-link" to="/admin" onClick={() => setMenuOpen(false)}>
                Admin Paneli
              </Link>
            ) : (
              <Link className="navbar-link" to={`/myAccount/${userName}`} onClick={() => setMenuOpen(false)}>
                Hesabım
              </Link>
            )}
            <Link className="navbar-link navbar-register" onClick={() => { handleLogout(); setMenuOpen(false); }}>
              Çıkış Yap
            </Link>
            <div className="search-bar">
              <form onSubmit={handleSearchSubmit}>
                <input
                  className="search-input"
                  type="text"
                  placeholder="Ara..."
                  value={searchTerm}
                  onChange={handleSearchChange}
                />
                <button className="search-button" type="submit">
                  Ara
                </button>
              </form>
            </div>
          </>
        ) : (
          <>
            <Link className="navbar-link navbar-link-giris-yap" to="/login" onClick={() => setMenuOpen(false)}>
              Giriş Yap
            </Link>
            <Link className="navbar-link navbar-register" to="/register" onClick={() => setMenuOpen(false)}>
              Kayıt Ol
            </Link>
            <div className="search-bar">
              <form onSubmit={handleSearchSubmit}>
                <input
                  className="search-input"
                  type="text"
                  placeholder="Ara..."
                  value={searchTerm}
                  onChange={handleSearchChange}
                />
                <button className="search-button" type="submit">
                  Ara
                </button>
              </form>
            </div>
          </>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
