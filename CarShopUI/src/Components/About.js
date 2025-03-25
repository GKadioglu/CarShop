import React from "react";
import "./Css/About.css";
import gallery1 from "../Components/Images/gallery1.jpg";
import gallery2 from "../Components/Images/gallery2.jpg";
import gallery3 from "../Components/Images/gallery3.jpg";
import gallery4 from "../Components/Images/gallery4.jpg";
import gallery5 from "../Components/Images/gallery5.jpg";
import gallery6 from "../Components/Images/gallery6.jpg";

function About() {
  return (
    <div className="about-container">
      {/* Sol taraf */}
      <div className="about-left">
        <section className="about-section">
          <h1>Hakkımızda</h1>
          <p>
            Biz, araçları daha erişilebilir hale getirmek ve kullanıcılarımıza
            kaliteli bir alışveriş deneyimi sunmak için buradayız. Gelişen
            teknoloji ve araç endüstrisindeki trendleri takip ederek en iyi
            seçenekleri sunmayı hedefliyoruz.
          </p>
        </section>

        <section className="about-section">
          <h2>Vizyonumuz</h2>
          <p>
            Sizin için en uygun aracı bulmak, güvenli, hızlı ve sorunsuz bir
            deneyim sunmaktır. Amacımız, sektördeki lider firmalardan biri
            olmaktır ve her müşterimize en yüksek kalitede hizmeti sunmak.
          </p>
        </section>

        <section className="about-section">
          <h2>Ekibimiz</h2>
          <p>
            Yetenekli ve tutkulu bir ekip olarak, birlikte çalışarak her zaman
            daha iyiye ulaşmayı amaçlıyoruz. Her biri kendi alanında uzman olan
            profesyonellerden oluşan ekibimiz, sizi dinlemeye ve ihtiyaçlarınıza
            uygun çözümler üretmeye hazır.
          </p>
        </section>
      </div>

      {/* Sağ taraf */}
      <div className="about-right">
        <section className="contact-section">
          <h2>Bize Ulaşın</h2>
          <p>
            Herhangi bir sorunuz varsa veya bizimle iletişime geçmek isterseniz,
            lütfen bizimle iletişime geçin. Yardımcı olmaktan memnuniyet
            duyarız.
          </p>
        </section>

        {/* Resim galerisi */}
        <div className="gallery">
          <img src={gallery1} alt=""/>
          <img src={gallery2} alt=""/>
          <img src={gallery3} alt=""/>
        </div>
        <div className="gallery">
        <img src={gallery4} alt=""/>
          <img src={gallery5} alt=""/>
          <img src={gallery6} alt=""/>
        </div>

        {/* Adres */}
        <div className="address">
          <h3>Adresimiz</h3>
          <p>İstanbul | Türkiye | CarShop Şube 34000.</p>
          <p>İletişim Numarası: 0216 029 21 12</p>
        </div>
      </div>
    </div>
  );
}

export default About;
