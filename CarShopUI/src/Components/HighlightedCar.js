import React, { useEffect, useState } from "react";
import "../Components/Css/HighlightedCar.css";
import logo from "../Components/Images/car shop (2)-Photoroom.png";
import { FaArrowRight } from "react-icons/fa"; 

function HighlightedCar() {
  const [highlightedCars, setHighlightedCars] = useState([]); 
  const [loading, setLoading] = useState(true); 
  const [currentCarIndex, setCurrentCarIndex] = useState(0); 

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

  useEffect(() => {
    if (highlightedCars.length > 0) {
      const interval = setInterval(() => {
        setCurrentCarIndex(
          (prevIndex) => (prevIndex + 1) % highlightedCars.length
        );
      }, 5000); // 10 saniye
      return () => clearInterval(interval); 
    }
  }, [highlightedCars]);

  const handleNextCar = () => {
    setCurrentCarIndex((prevIndex) => (prevIndex + 1) % highlightedCars.length);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  const currentCar = highlightedCars[currentCarIndex];
  return (
    <div
      className="magazine-cover"
      style={{
        backgroundImage: `url(http://localhost:5000/${currentCar.imageUrl})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
        minHeight: "100vh", 
        width: "100%", 
        margin: 0, 
        padding: 0, 
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
          alt=""
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
