import React, { useEffect, useState } from "react";
import "../Components/Css/HighlightedCar.css";
import logo from "../Components/Images/car shop (2)-Photoroom.png";
import { FaArrowRight } from "react-icons/fa"; // React Icons'dan ikon import edilmesi

function HighlightedCar() {
  const [highlightedCars, setHighlightedCars] = useState([]); // API'den gelen araçlar
  const [loading, setLoading] = useState(true); // Yükleniyor durumu
  const [currentCarIndex, setCurrentCarIndex] = useState(0); // Şu anda gösterilen aracın indeksi

  // API'den veri çekme
  useEffect(() => {
    const fetchHighlightedCars = async () => {
      try {
        const response = await fetch(
          "http://localhost:5000/api/home/HighlightedCars"
        );
        const data = await response.json();
        setHighlightedCars(data.highlightedCars || []);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching data:", error);
        setLoading(false);
      }
    };

    fetchHighlightedCars();
  }, []);

  // 10 saniyede bir otomatik değişim
  useEffect(() => {
    if (highlightedCars.length > 0) {
      const interval = setInterval(() => {
        setCurrentCarIndex(
          (prevIndex) => (prevIndex + 1) % highlightedCars.length
        );
      }, 5000); // 10 saniye
      return () => clearInterval(interval); // Component unmount olduğunda temizle
    }
  }, [highlightedCars]);

  // Manuel değişim için bir sonraki araca geç
  const handleNextCar = () => {
    setCurrentCarIndex((prevIndex) => (prevIndex + 1) % highlightedCars.length);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  // Şu anda gösterilecek araç
  const currentCar = highlightedCars[currentCarIndex];
  return (
    <div
      className="magazine-cover"
      style={{
        backgroundImage: `url(http://localhost:5000/${currentCar.imageUrl})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
        minHeight: "100vh", // Ekranın tamamını kapsar
        width: "100%", // Tam genişlikte yer kaplar
        margin: 0, // Varsayılan boşlukları kaldırır
        padding: 0, // Varsayılan iç boşlukları kaldırır
      }}
    >
      <div className="overlay">
        <h2 className="magazine-title">Haftanın Öne Çıkanları</h2>
        <div className="highlighted-car">
          <h3 className="highlighted-car-title">
            {currentCar.year} Model {currentCar.brand} {currentCar.model}
          </h3>
          <p className="highlighted-car-description">
            {currentCar.description}
          </p>
          <button className="next-car-button" onClick={handleNextCar}>
            <FaArrowRight /> {/* İkon ile metin birleştirildi */}
          </button>
        </div>
        <div className="magazine-details">
          <p>
            <strong>Tarih:</strong> 5 Aralık 2024
          </p>
          <p>
            <strong>Kategori:</strong> İlgi Görenler
          </p>
          <img
            src={logo}
            className="magazine-logo"
            style={{ cursor: "default" }}
          />
        </div>
        {/* Buton */}
      </div>
    </div>
  );
}

export default HighlightedCar;
