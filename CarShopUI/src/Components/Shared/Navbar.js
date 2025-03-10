import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Css/Navbar.css";
import { useAuth } from "./AuthContext";
import logo from "../Images/car shop (2)-Photoroom.png";

function Navbar() {
  const [searchTerm, setSearchTerm] = useState("");
  const navigate = useNavigate();
  const { isAuthenticated, logout, userName, role } = useAuth(); // role burada alınacak

  useEffect(() => {
  }, [isAuthenticated, userName, role]);

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleSearchSubmit = (event) => {
    event.preventDefault();
    navigate(`/search?query=${searchTerm}`);
  };

  const handleLogout = () => {
    logout();
    window.location.reload();
    navigate("/");
  };

  return (
    <nav className="navbar">
      <div className="navbar-left">
    
        <ul className={`navbar-list `}>
          <li>
            <Link className="navbar-link-left" to="/">
              Ana Sayfa
            </Link>
          </li>
          <li>
            <Link className="navbar-link-left" to="/category">
              Kategoriler
            </Link>
          </li>
          <li>
            <Link className="navbar-link-left" to="/about">
              Hakkımızda
            </Link>
          </li>
        </ul>
      </div>

      <div className="navbar-logo" style={{ cursor: "default" }}>
        <Link to="/">
          <img src={logo} alt="Logo" />
        </Link>
      </div>

      <div className="navbar-right">
        {isAuthenticated ? (
          <>
            {role === "admin" ? ( // Role göre admin kontrolü
              <Link className="navbar-link" to="/admin">
                Admin Paneli
              </Link>
            ) : (
              <Link className="navbar-link" to={`/myAccount/${userName}`}>
                Hesabım
              </Link>
            )}
            <Link
              className="navbar-link navbar-register"
              onClick={handleLogout}
            >
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
            <Link className="navbar-link" to="/login">
              Giriş Yap
            </Link>
            <Link className="navbar-link navbar-register" to="/register">
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
