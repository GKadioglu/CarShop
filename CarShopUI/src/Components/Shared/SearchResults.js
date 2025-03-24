import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import '../Css/HomePage.css';

const SearchResults = () => {
  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const query = new URLSearchParams(location.search).get('query');
    
    if (query) {
      fetch(`http://localhost:5000/api/car/search?name=${query}`)
        .then(response => response.json())
        .then(data => {
          setCars(data.cars);  
          setLoading(false);
        })
        .catch(error => {
          console.error('Arama hatası:', error);
          setLoading(false);
        });
    }
  }, [location]);

  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div className="home-page">
      <h1 className="title">Katalog | 2024 </h1>
      <div className="card-container">
        {cars.length > 0 ? (
          cars.map((car) => (
            <div key={car.carId} className="card">
              <div className="card-image-wrapper">
                <img
                  src={`http://localhost:5000/${car.imageUrl}`}
                  className="car-image"
                  alt={`${car.brand} ${car.model}`}
                />
              </div>
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
          ))
        ) : (
          <div className="no-results">
            <p>Hiçbir sonuç bulunamadı.</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default SearchResults;