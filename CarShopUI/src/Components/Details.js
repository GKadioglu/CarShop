import axios from "axios";
import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import "./Css/Details.css";
import { Suspense } from "react";
import logo from "../Components/Images/car shop (2)-Photoroom.png";
import { OrbitControls, useGLTF } from "@react-three/drei";

function CarDetails() {
  const { carId } = useParams(); // URL'den carId parametresini alır
  const [carDetails, setCarDetails] = useState(null);
  const [error, setError] = useState(null);
  const [modelUrl, setModelUrl] = useState("");
  const navigate = useNavigate(); // Navig ate kullanarak sayfa yönlendirme

  useEffect(() => {
    const fetchCarDetails = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5000/api/car/details/${carId}`
        );
        setCarDetails(response.data.data); // API'den gelen veriyi state'e ata
      } catch (err) {
        setError("Car not found");
      }
    };

    fetchCarDetails();
  }, [carId]);

 

  function Model({ modelUrl }) {
    const { scene } = useGLTF(modelUrl);
    return <primitive object={scene} scale={8.5} />;
  }

  if (error) {
    return <div className="error-message">Error: {error}</div>;
  }

  if (!carDetails) {
    return <div className="loading-message">Loading...</div>; // Veriler yüklenirken gösterilecek
  }

  const handleContactUs = () => {
    navigate(`/contact/${carDetails.brand}-${carDetails.model}`); // İletişim sayfasına yönlendirme
  };
  const handleMakeAnOffer = () => {
    navigate(`/makeAnOffer/${carDetails.brand}-${carDetails.model}`); // İletişim sayfasına yönlendirme
  };
  return (
    <div className="car-details-container">
      <div className="car-image-container">
        <div className="car-image-wrapper">
            <img
              src={`http://localhost:5000/${carDetails.imageUrl}`}
              className="car-image"
              alt="Car"
            />     
        </div>
      </div>

      <div className="car-info-container">
        <div className="car-info">
          <h1 className="car-info-container-h1">{carDetails.brand}</h1>
          <p>
            <strong>Model:</strong> {carDetails.model}
          </p>
          <p>
            <strong>Year:</strong> {carDetails.year}
          </p>
          <p>
            <strong>Price:</strong> ${carDetails.price}
          </p>
          <h2>Related Models</h2>
          {carDetails.models.map((model) => (
            <li key={model.modelId}>
              <strong>Origin:</strong> {model.origin}
            </li>
          ))}
          <button className="btn-contactUs" onClick={handleContactUs}>
            İletişime Geç
          </button>
          <button className="btn-contact-make-an-offer" onClick={handleMakeAnOffer}>
            Teklif Ver
          </button>
          <img src={logo} className="cart-logo" style={{ cursor: "default" }} />
        </div>
      </div>
    </div>
  );
}

export default CarDetails;
