import React from "react";
import image from "../Images/navbar-background2.jpg"; // Arka plan resmi
import "../Css/Footer.css"; // CSS dosyasını dahil ediyoruz

function Footer() {
  return (
    <footer className="footer" style={{ backgroundImage: `url(${image})` }}>
      <div className="footer-container">
        {/* Kurumsal */}
        <div className="footer-card">
          <h4 className="footer-title">Kurumsal</h4>
          <ul>
            <li><a href="/about">Hakkımızda</a></li>
            <li><a href="/dealers">Bayilerimiz</a></li>
            <li><a href="/news">Haberler</a></li>
            <li><a href="/services">Hizmetlerimiz</a></li>
          </ul>
        </div>

        {/* Gizlilik ve Kullanım */}
        <div className="footer-card">
          <h4 className="footer-title">Gizlilik ve Kullanım</h4>
          <ul>
            <li><a href="/terms">Sözleşmeler ve Kurallar</a></li>
            <li><a href="/account-terms">Hesap Sözleşmesi</a></li>
            <li><a href="/privacy">Kişisel Verilerin Korunması</a></li>
            <li><a href="/help">Yardım Rehberi</a></li>
            <li><a href="/email">Email İşlemleri</a></li>
          </ul>
        </div>

        {/* Bağlantılar */}
        <div className="footer-card">
          <h4 className="footer-title">Bağlantılar</h4>
          <ul>
            <li><a href="/contact">İletişim</a></li>
            <li><a href="/add-listing">İlan Ver</a></li>
            <li><a href="/services">Servis</a></li>
            <li><a href="/blog">Blog</a></li>
            <li><a href="/faq">SSS</a></li>
          </ul>
        </div>

        {/* Vizyon */}
        <div className="footer-card">
          <h4 className="footer-title">Vizyon</h4>
          <ul>
            <li><a href="/history">Tarihçe</a></li>
            <li><a href="/dedications">İthaflar</a></li>
            <li><a href="/application">Uygulama</a></li>
            <li><a href="/mission">Misyonumuz</a></li>
          </ul>
        </div>

        {/* Sosyal Medya */}
        <div className="footer-card">
          <h4 className="footer-title">Bizi Takip Edin</h4>
          <ul>
          <li><a href="https://facebook.com">Facebook</a></li>
          <li><a href="https://twitter.com">X</a></li>
          <li><a href="https://instagram.com">Instagram</a></li>
          <li><a href="https://linkedin.com">LinkedIn</a></li>
          <li><a href="https://youtube.com">YouTube</a></li>
          </ul>
        </div>
      </div>

      {/* Alt Bilgi */}
      <div className="footer-bottom">
        <p>© 2024 CarShop | Gürkan Kadıoğlu</p>
      </div>
    </footer>
  );
}

export default Footer;