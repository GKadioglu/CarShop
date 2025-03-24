import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import "./Css/HomePage.css";
import { FaHeart, FaArrowUp, FaChevronCircleUp  } from "react-icons/fa";
import HighlightedCar from "./HighlightedCar";
import HomePageNews from "./HomePageNews";
import { useAuth } from "./Shared/AuthContext";
import { fetchWithToken } from "./Shared/api";

const HomePage = () => {
  const { token } = useAuth();
  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const carsPerPage = 6;
  const [favoriteCars, setFavoriteCars] = useState([]);
  const [showWarning, setShowWarning] = useState(false); 

  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const pageParam = new URLSearchParams(location.search).get("page");
    if (pageParam) {
      setCurrentPage(Number(pageParam));
    }

    fetchWithToken(
      `http://localhost:5000/api/home/index?page=${currentPage}&pageSize=${carsPerPage}`,
      { method: "GET" }
    )
      .then((response) => response.json())
      .then((data) => {
        setCars(data.cars);
        setTotalPages(
          data.totalPages || Math.ceil(data.totalCars / carsPerPage)
        );
        setLoading(false);
      })
      .catch((error) => {
        console.error("Veri çekme hatası:", error);
        setLoading(false);
      });
  }, [currentPage, location, token]);

  useEffect(() => {
    if (!token) return;

    fetchWithToken("http://localhost:5000/api/users/getFavoriteCars", {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data)) {
          setFavoriteCars(data.map((car) => car.carId)); // FavoriteId'leri sadece alıyoruz
        } else {
          console.error("Favori araçlar verisi bir array değil:", data);
        }
      })
      .catch((error) => {
        console.error("Favori araçlar alınamadı:", error);
      });
  }, [token]);

  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

  const handlePageChange = (page) => {
    if (page > 0 && page <= totalPages) {
      setCurrentPage(page);
      navigate(`?page=${page}`);
    }
  };

  const handleFavoriteToggle = (carId) => {
    if (!token) {
      setShowWarning(true);
      setTimeout(() => setShowWarning(false), 5000); // 5 saniye sonra uyarıyı gizle
      return;
    }

    const isAlreadyFavorite = favoriteCars.includes(carId);

    const url = isAlreadyFavorite
      ? "http://localhost:5000/api/users/removeFavoriteCar"
      : "http://localhost:5000/api/users/addFavoriteCar";

    fetchWithToken(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ carId: carId }),
    })
      .then((response) => response.json())
      .then((data) => {
        if (
          data.message === "Car added to favorites successfully" ||
          data.message === "Car removed from favorites successfully"
        ) {
          setFavoriteCars((prevFavorites) => {
            if (isAlreadyFavorite) {
              return prevFavorites.filter((id) => id !== carId); // Favorilerden çıkar
            } else {
              return [...prevFavorites, carId]; // Favorilere ekle
            }
          });
        }
      })
      .catch((error) => console.error("Favori işlemi başarısız:", error));
  };

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  const [isVisible, setIsVisible] = useState(false);

  const handleScroll = () => {
    if (window.scrollY > 50) {
      setIsVisible(true);
    } else {
      setIsVisible(false);
    }
  };

  useEffect(() => {
    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  const isFavorite = (carId) => {
    return favoriteCars.includes(carId);
  };

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div className="home-page">
      <h1 className="title">Katalog | 2024 </h1>

      <div className="card-container">
        {cars.map((car) => (
          <div key={car.id} className="card">
            <div className="card-image-wrapper">
              <img
                src={`http://localhost:5000/${car.imageUrl}`}
                className="car-image"
                alt={`${car.brand} ${car.model}`}
              />
            </div>
            {/* Favori butonu */}
            <button
              onClick={() => handleFavoriteToggle(car.carId)}
              className={`favorite-btn ${
                isFavorite(car.carId) ? "favorite" : ""
              }`}
            >
              <FaHeart />
              {showWarning && (
                <div className="warning-bubble">
                  Giriş yaparak favoriye ekleyebilirsiniz!
                </div>
              )}
            </button>
            <div className="card-content">
              <h2 className="card-title">
                {car.brand} {car.model}
              </h2>
              <p className="price-text">
                Fiyat: <span className="price">{car.price} $</span>
                <button
                  className="btn-incele"
                  onClick={() => handleInceleClick(car.carId)}
                >
                  İncele
                </button>
              </p>
            </div>
          </div>
        ))}
      </div>

      <div className="pagination">
        <button
          onClick={() => handlePageChange(currentPage - 1)}
          disabled={currentPage === 1}
          className="homepage-pagination-left"
        >
          {"<"} Önceki
        </button>
        {Array.from({ length: totalPages }, (_, index) => (
          <button
            key={index}
            onClick={() => handlePageChange(index + 1)}
            className={currentPage === index + 1 ? "active" : ""}
          >
            {index + 1}
          </button>
        ))}
        <button
          onClick={() => handlePageChange(currentPage + 1)}
          disabled={currentPage === totalPages}
          className="homepage-pagination-right"
        >
          Sonraki {">"}
        </button>
      </div>
      <HighlightedCar />
      <div style={{ marginTop: "5px" }}></div>
      <HomePageNews />

      {/* Scroll to top button */}
      {isVisible && (
        <button className="scroll-to-top" onClick={scrollToTop}>
          <FaChevronCircleUp className="scroll-to-top-icon" />
        </button>
      )}
    </div>
  );
};

export default HomePage;
