import React from 'react';
import './Css/Help.css';

function Help() {
  return (
    <div className="helppage-container">
      <h1 className="helppage-title">Yardım ve Destek</h1>
      <p className="helppage-intro">
        Aradığınız bilgileri bulmak için aşağıdaki kategorilere göz atabilirsiniz.
      </p>

      {/* FAQ Bölümü */}
      <section className="helppage-faq">
        <h2 className="helppage-faq-title">Sık Sorulan Sorular (FAQ)</h2>
        <div className="helppage-faq-item">
          <strong>Nasıl araç arayabilirim?</strong>
          <p>Arama çubuğuna araç adı ya da kategori yazarak istediğiniz aracı hızlıca bulabilirsiniz.</p>
        </div>
        <div className="helppage-faq-item">
          <strong>Favori araçları nasıl eklerim?</strong>
          <p>Herhangi bir aracı beğendiğinizde, araç sayfasının üst kısmındaki "Favorilere Ekle" butonuna tıklayarak o aracı favorilerinize ekleyebilirsiniz.</p>
        </div>
        <div className="helppage-faq-item">
          <strong>Hesap oluşturma işlemi nasıl yapılır?</strong>
          <p>Üye ol butonuna tıklayarak gerekli bilgilerinizi girerek hesap oluşturabilirsiniz.</p>
        </div>
      </section>

      {/* Yardımcı Kılavuzlar */}
      <section className="helppage-guides">
        <h2 className="helppage-guides-title">Yardımcı Kılavuzlar</h2>
        <p>Adım adım rehberler ve videolar ile araçları nasıl keşfedeceğinizi öğrenin:</p>
        <ul className="helppage-guides-list">
          <li><a  className= "helppage-guides-list-li" href="#">Araç Arama ve Filtreleme Kılavuzu</a></li>
          <li><a  className= "helppage-guides-list-li" href="#">Favorilere Araç Ekleme ve Yönetme</a></li>
          <li><a className= "helppage-guides-list-li"  href="#">Hesap ve Üyelik Ayarları</a></li>
        </ul>
      </section>

      {/* Canlı Destek ve İletişim */}
      <section className="helppage-contact">
        <h2 className="helppage-contact-title">Canlı Destek ve İletişim</h2>
        <p className="helppage-contact-info">
          Yardım almak için aşağıdaki yollarla bizimle iletişime geçebilirsiniz:
        </p>
        <ul className="helppage-contact-list">
          <li><strong>Canlı Sohbet:</strong> 7/24 hizmetinizdeyiz. Sağ altta bulunan sohbet penceresini kullanabilirsiniz.</li>
          <li><strong>E-posta:</strong> carshop.businessmail@gmail.com</li>
          <li><strong>Telefon:</strong> +90 123 456 7890</li>
        </ul>
      </section>

      {/* Ekstra Yardım ve Geri Bildirim */}
      <section className="helppage-feedback">
        <h2 className="helppage-feedback-title">Geri Bildirim Gönderin</h2>
        <p>Yardım almak için bizimle iletişime geçebilirsiniz. Ayrıca öneri veya şikayetlerinizi de buradan iletebilirsiniz.</p>
        <button className="helppage-feedback-button">Geri Bildirim Gönder</button>
      </section>
    </div>
  );
}

export default Help;