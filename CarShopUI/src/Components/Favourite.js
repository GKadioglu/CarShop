import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { fetchWithToken } from "./Shared/api";
import { useAuth } from "./Shared/AuthContext";
import "./Css/FavoriteCar.css"

function Favourite() {
  const { token } = useAuth();
  const [favoriteCars, setFavoriteCars] = useState([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const visibleCars = 3;
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate("/login");
    }
  }, [token, navigate]);

  
  const handleCikarClick = (carId) => {
    if (!token) return;
  
    fetchWithToken("http://localhost:5000/api/users/removesFavoriteCar", {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ carId }),
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.message && data.message.includes("successfully")) {
          setFavoriteCars((prevCars) => {
            return prevCars.filter((car) => car.carId !== carId);
          });
        } else {
          console.error("Araç favorilerden çıkarılamadı:", data.message);
        }
      })
      .catch((error) => {
        console.error("Favori araç çıkarma hatası:", error);
      });
  };

  useEffect(() => {
    if (!token) return;

    fetchWithToken("http://localhost:5000/api/users/getFavoriteCars", {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data)) {
          setFavoriteCars(data);
        } else {
          console.error("Favori araçlar verisi bir array değil:", data);
        }
      })
      .catch((error) => {
        console.error("Favori araçlar alınamadı:", error);
      });
  }, [token]);
  const handleNext = () => {
    if (currentIndex + visibleCars < favoriteCars.length) {
      setCurrentIndex(currentIndex + visibleCars);
    }
  };

  const handlePrev = () => {
    if (currentIndex - visibleCars >= 0) {
      setCurrentIndex(currentIndex - visibleCars);
    }
  };

  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

  return (
    <section id="favorite-car-panel" className="favorite-car-panel">
      <h3 className="favorite-car-panel-h3">Favori Araçlarım</h3>
      <div className="favorite-car-panel-list">
        <button className="favorite-car-panel-btn-prev" onClick={handlePrev}>
          {"<"}
        </button>

        {favoriteCars && favoriteCars.length > 0 ? (
          favoriteCars
            .slice(currentIndex, currentIndex + visibleCars)
            .map((car) => (
              <div key={car.carId} className="favorite-car-panel-card">
                <img
                  src={`http://localhost:5000/${car.imageUrl}`}
                  alt={car.model}
                  className="favorite-car-panel-image"
                />
                <div className="favorite-car-panel-details">
                  <p>
                    {car.brand} {car.model}
                  </p>
                  <p className="favorite-car-panel-price">
                    <strong>Fiyat:</strong> {car.price}$
                  </p>
                  <button
                    className="favorite-car-panel-btn-incele"
                    onClick={() => handleInceleClick(car.carId)}
                  >
                    İncele
                  </button>
                  <button
                    className="favorite-car-panel-btn-cikar"
                    onClick={() => handleCikarClick(car.carId)}
                  >
                    Çıkar
                  </button>
                </div>
              </div>
            ))
        ) : (
          <p>Favori araç bulunamadı.</p>
        )}

        <button className="favorite-car-panel-btn-next" onClick={handleNext}>
          {">"}
        </button>
      </div>
    </section>
  );
}

export default Favourite;
