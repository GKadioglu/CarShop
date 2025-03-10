import React from "react";
import { useNavigate } from "react-router-dom";
import "./Css/Admin.css";
import adminImage3 from "./Images/adminPanelImages/adminPanel3.png";
import adminImage4 from "./Images/adminPanelImages/adminPanel4.jpg";

function Admin() {
  const navigate = useNavigate(); // Yönlendirme için hook

  return (
    <div className="admin-panel-container">
      <div className="admin-panel-header">
        <h1>Admin Panel</h1>
        <p>Admin işlemlerinizi bu sayfadan yapabilirsiniz</p>
      </div>

      <div className="admin-panel-grid">
        <div className="admin-panel-card">
          <div
            className="admin-panel-card-header"
            style={{ backgroundImage: `url(${adminImage3})` }}
          >
            <h3>Kullanıcı İşlemlerİ</h3>
          </div>
          <div className="admin-panel-subtitles">
            <div className="admin-panel-subtitle">
              <span>Kullanıcıları Görüntüle</span>
              <button
                className="admin-panel-btn"
                onClick={() => navigate("/admin/useredit")}
              >
                Git
              </button>
            </div>
            <div className="admin-panel-subtitle">
              <span>Kullanıcıları Düzenle</span>
              <button
                className="admin-panel-btn"
                onClick={() => navigate("/admin/useredit")}
              >
                Git
              </button>
            </div>
          </div>
        </div>

        <div className="admin-panel-card">
          <div className="admin-panel-card-header">
            <h3>Araç İşlemlerİ</h3>
          </div>
          <div className="admin-panel-subtitles">
            <div className="admin-panel-subtitle">
              <span>Kataloğu Görüntüle</span>
              <button
                className="admin-panel-btn"
                onClick={() => navigate("/admin/caredit")}
              >
                Git
              </button>
            </div>
            <div className="admin-panel-subtitle">
              <span>Teklifleri İncele</span>
              <button
                className="admin-panel-btn"
                onClick={() => navigate("/admin/replyoffer")}
              >
                Git
              </button>
            </div>
          </div>
        </div>

        <div className="admin-panel-card">
          <div
            className="admin-panel-card-header"
            style={{ backgroundImage: `url(${adminImage4})` }}
          >
            <h3>Mesaj İşlemlerİ</h3>
          </div>
          <div className="admin-panel-subtitles">
            <div className="admin-panel-subtitle">
              <span>Email Mesajları</span>
              <button className="admin-panel-btn"
              onClick={() => navigate("/admin/messages")}>Git</button>
            </div>
            <div className="admin-panel-subtitle">
              <span>Bildirim Yayınla</span>
              <button className="admin-panel-btn"
              onClick={() => navigate("/admin/notification")}>Git</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Admin;
