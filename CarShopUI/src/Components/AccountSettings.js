import React, { useEffect, useState } from "react";
import { useAuth } from "./Shared/AuthContext";
import { fetchWithToken } from "./Shared/api";
import { useNavigate } from "react-router-dom";
import "./Css/AccountSettings.css";

function AccountSettings() {
  const { token } = useAuth(); // AuthContext'ten token çekiyoruz
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    const fetchUserDetails = async () => {
      try {
        const response = await fetchWithToken(
          `http://localhost:5000/api/users/getUserDetail`, 
          { method: "GET" }
        );
        
        if (!response.ok) throw new Error("Kullanıcı bilgileri alınamadı.");
        const data = await response.json();
        setUser(data);
      } catch (error) {
        console.error("Hata:", error);
      }
    };

    fetchUserDetails();
  }, [token, navigate]);

  const handleDeleteUser = async () => {
    if (window.confirm("Hesabınızı silmek istediğinizden emin misiniz?")) {
      try {
        const response = await fetchWithToken(
          `http://localhost:5000/api/users/deleteUser`,
          { method: "DELETE" }
        );
  
        if (response.ok) {
          alert("Hesabınız başarıyla silindi.");
          navigate("/"); 
          window.location.reload(); 
        } else {
          throw new Error("Hesap silinemedi.");
        }
      } catch (error) {
        alert("Hata: " + error.message);
      }
    }
  };

  return (
    <div className="account-settingspage">
      <h2 className="account-settingspage-h2">Hesap Bilgileri</h2>
      {user ? (
        <div className="account-settingspage-user-details">
          <div className="account-settingspage-user-detail-item">
            <strong>Kullanıcı Adı:</strong> {user.userName}
          </div>
          <div className="account-settingspage-user-detail-item">
            <strong>Ad:</strong> {user.firstName}
          </div>
          <div className="account-settingspage-user-detail-item">
            <strong>Soyad:</strong> {user.lastName}
          </div>
          <div className="account-settingspage-user-detail-item">
            <strong>Email:</strong> {user.email}
          </div>
          <div className="account-settingspage-user-detail-item">
            <strong>Email Onay Durumu:</strong>{" "}
            {user.emailConfirmed ? "Onaylı" : "Onaylanmadı"}
          </div>
          <div className="account-settingspage-user-detail-item">
            <button className="account-settingspage-user-detail-button-left">
              Bilgileri Düzenle
            </button>
            <button
              className="account-settingspage-user-detail-button-right"
              onClick={handleDeleteUser} 
            >
              Hesabımı Sil
            </button>
          </div>
        </div>
      ) : (
        <p>Bilgiler yükleniyor...</p>
      )}
    </div>
  );
}

export default AccountSettings;