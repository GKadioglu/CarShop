import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom"; 
import { useNavigate } from "react-router-dom"; 
import "./Css/CategoryDetails.css";

const CategoryDetails = () => {
  const { categoryName } = useParams(); 
  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    fetch(`http://localhost:5000/api/category/categories/${categoryName}`) 
      .then((response) => {
        if (!response.ok) {
          throw new Error("API hatası!");
        }
        return response.json();
      })
      .then((data) => {
        setCars(data.data.cars); 
        setLoading(false);
      })
      .catch((error) => {
        console.error("Veri çekme hatası:", error);
        setLoading(false);
      });
  }, [categoryName]); 
  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div className="category-details-container">
      <div className="category-details-left">
        <h2 className="category-left-title">Kategoriler Hakkında</h2>
        <p className="category-left-description">
          Araç kategorileri, araçların türüne ve kullanım amacına göre
          gruplandırıldığı sınıflandırma sistemidir. Bu kategoriler, alıcıların
          ihtiyaçlarına en uygun araçları hızlı ve kolay bir şekilde
          seçebilmeleri için geliştirilmiştir. Her araç kategorisi, sahip olduğu
          özelliklerle özel bir kullanım deneyimi sunar.
        </p>
        <h3 className="category-subtitle">SUV (Sport Utility Vehicle)</h3>
        <p className="category-left-description">
          SUV'ler, çok yönlü kullanım için tasarlanmış araçlardır. Genellikle
          büyük boyutları, geniş iç mekanları ve dört tekerlekten çekiş (4x4)
          özellikleri ile bilinir. Bu özellikleri sayesinde SUV'ler, zorlu yol
          koşullarında bile rahatlıkla sürüş yapılabilen araçlardır.
        </p>
        <h3 className="category-subtitle">SEDAN</h3>
        <p className="category-left-description">
          Sedan araçlar, klasik ve zarif tasarımlarıyla bilinen, genellikle 4
          kapılı ve geniş iç hacme sahip araçlardır. Sedanın tasarımı, konforlu
          bir sürüş deneyimi sunmaya yöneliktir. Şehir içi kullanımda kolay
          manevra kabiliyeti ve yol tutuşu sağlarken, uzun yolculuklarda da
          konforlu bir seyahat deneyimi sunar.
        </p>
        <h3 className="category-subtitle">HATCHBACK</h3>
        <p className="category-left-description">
          Hatchback araçlar, pratiklik ve kompaktlık arayanlar için ideal bir
          tercihtir. Arka bagaj kapağının yükselmesiyle kolayca erişilebilen
          geniş yük alanı sunar. Bu araçlar, şehir içi sürüşlerde manevra
          kabiliyetine sahip olup, dar park yerlerinde de rahatlıkla park
          edilebilirler. Hatchback'ler, genellikle küçük ve orta sınıf araçlar
          olarak tercih edilse de, geniş iç mekanları sayesinde aileler için de
          uygun olabilir.
        </p>
        <h3 className="category-subtitle">CONVERTIBLE</h3>
        <p className="category-left-description">
          Convertible araçlar, açık hava sürüşü keyfini yaşamak isteyenler için
          tasarlanmış araçlardır. Çatısı açılabilir olan bu araçlar, sürücülere
          hem şıklık hem de özgürlük sunar. Özellikle yaz aylarında açık hava
          sürüşünün tadını çıkarmak isteyen kişiler için tercih edilir.
        </p>
        <h3 className="category-subtitle">TRUCK</h3>
        <p className="category-left-description">
        Truck (kamyonet) araçlar, genellikle ağır yük taşıma kapasitesi ile bilinir ve pratiklik açısından oldukça işlevseldir. Genelde büyük kamyonetler, çift kabinli ya da tek kabinli seçeneklerle gelir ve yüksek taşıma kapasitesine sahiptir. Ayrıca, kamyonetler, tatile çıkanlar ya da açık hava etkinlikleri için geniş alan sunarak mükemmel birer araç olabilir. Truck kategorisi, gücü, dayanıklılığı ve geniş taşıma alanları ile öne çıkar.

        </p>
      </div>

      <div className="category-details-right">
        <h1 className="category-details-title">{categoryName} Araçlar</h1>
        <div className="car-detail-slist">
          {cars.map((car) => (
            <div className="car-details-card" key={car.carId}>
              <div className="car-details-image-wrapper">
                <img
                  src={`http://localhost:5000/${car.imageUrl}`}
                  alt={`${car.brand} ${car.model}`}
                  className="car-details-image"
                />
              </div>
              <div className="car-details-info">
                <h2 className="car-details-title">
                  {car.brand} {car.model}
                </h2>
                <p className="car-details-year">Yıl: {car.year}</p>
                <p className="car-details-price">Fiyat: {car.price} $</p>
                <button
                  className="btn-category-details-incele"
                  onClick={() => handleInceleClick(car.carId)}
                >
                  İncele
                </button>
              </div>
            </div>
          ))}
        </div>
        {/* Geri dönüş formu */}
        <div className="category-feedback-form">
          <h2 className="form-title">
            Kategorilerle İlgili Sorularınız İçin Not Bırakınız
          </h2>
          <form className="feedback-form">
            <div className="form-group">
              <label htmlFor="first-name">İsim</label>
              <input
                type="text"
                id="first-name"
                placeholder="Adınızı giriniz"
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="last-name">Soyisim</label>
              <input
                type="text"
                id="last-name"
                placeholder="Soyadınızı giriniz"
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="email">E-posta</label>
              <input
                type="email"
                id="email"
                placeholder="E-posta adresinizi giriniz"
                required
              />
            </div>
            <div className="form-group">
              <label htmlFor="message">Mesaj</label>
              <textarea
                id="message"
                placeholder="Sorularınızı buraya yazınız"
                required
              ></textarea>
            </div>
            <button type="submit" className="btn-submit">
              Gönder
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default CategoryDetails;
