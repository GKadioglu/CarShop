import React, { useEffect, useState } from "react";
import { useAuth } from "./Shared/AuthContext";
import { fetchWithToken } from "./Shared/api";
import { FaArrowLeft, FaArrowRight } from "react-icons/fa";
import "./Css/Agreements.css";

function Agreements() {
  const { email } = useAuth();
  const [offers, setOffers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [currentAcceptedIndex, setCurrentAcceptedIndex] = useState(0);
  const [currentRejectedIndex, setCurrentRejectedIndex] = useState(0);

  useEffect(() => {
    const fetchOffers = async () => {
      try {
        const response = await fetchWithToken(
          `http://localhost:5000/api/users/getAdminOffer?email=${email}`
        );
        const data = await response.json();
        if (response.ok) {
          setOffers(data.data || []); // Verinin boş olmasını önlemek için
        } else {
          setError(data.message || "Teklifler alınamadı.");
        }
      } catch (err) {
        setError("Bir hata oluştu, lütfen tekrar deneyin.");
      } finally {
        setLoading(false);
      }
    };

    if (email) {
      fetchOffers();
    }
  }, [email]);

  if (loading) return <p>Yükleniyor...</p>;
  if (error) return <p>{error}</p>;

  const acceptedOffers = offers?.filter((offer) => offer.acceptance === true);
  const rejectedOffers = offers?.filter((offer) => offer.acceptance === false);

  const handleNext = () => {
    if (currentAcceptedIndex < acceptedOffers.length - 1) {
      setCurrentAcceptedIndex(currentAcceptedIndex + 1);
    }
  };

  const handlePrev = () => {
    if (currentAcceptedIndex > 0) {
      setCurrentAcceptedIndex(currentAcceptedIndex - 1);
    }
  };

  // Reddedilenler için ileri geri fonksiyonları
  const handleNextRejected = () => {
    if (currentRejectedIndex < rejectedOffers.length - 1) {
      setCurrentRejectedIndex(currentRejectedIndex + 1);
    }
  };

  const handlePrevRejected = () => {
    if (currentRejectedIndex > 0) {
      setCurrentRejectedIndex(currentRejectedIndex - 1);
    }
  };
  return (
    <div className="user-panel-myoffer-container">
      <h1 className="user-panel-myoffer-title">Tekliflerim</h1>
      <div className="user-panel-myoffer-grid">
        {/* Kabul edilen teklifler */}
        <div className="user-panel-myoffer-section">
          <h2 className="user-panel-myoffer-subtitle-left">Kabul Edilenler</h2>
          {acceptedOffers.length > 0 ? (
            <div className="user-panel-myoffer-item-left">
              <img
                src={`http://localhost:5000/${acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]?.userMakeOfferCars?.[0]?.imageUrl}`}
                alt={
                  acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                    ?.userMakeOfferCars?.[0]?.brand || "Araba resmi"
                }
                className="user-panel-myoffer-img-left"
              />
              <p className="user-panel-myoffer-item-left-p">
                <strong>
                  {" "}
                  {acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                    ?.userMakeOfferCars?.[0]?.brand || "Bilinmiyor"}{" "}
                </strong>
                {acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.model || "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-left-p">
                <strong>Yıl:</strong>{" "}
                {acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.year || "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-left-p ">
                <strong>Fiyat:</strong>{" "}
                {acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.price
                  ? `${acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]?.userMakeOfferCars?.[0]?.price} $`
                  : "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-left-p price-left-offer">
                <strong>Teklifim:</strong>{" "}
                {acceptedOffers[currentAcceptedIndex]?.userMakeOffers?.[0]
                  ?.offer || "Bilinmiyor"}{" "}
                $
              </p>
              <div className="user-panel-myoffer-nav"></div>
            </div>
          ) : (
            <p className="user-panel-myoffer-empty">
              Henüz kabul edilen teklif yok.
            </p>
          )}
          <div className="button-container-left">
            <button
              className="btn-user-offer-pref-left"
              onClick={handlePrev}
              disabled={currentAcceptedIndex === 0}
            >
              <FaArrowLeft />
            </button>
            <button
              className="btn-user-offer-next-left"
              onClick={handleNext}
              disabled={currentAcceptedIndex === acceptedOffers.length - 1}
            >
              <FaArrowRight />
            </button>
          </div>
        </div>

        {/* Reddedilen teklifler */}
        <div className="user-panel-myoffer-section">
          <h2 className="user-panel-myoffer-subtitle-right">Reddedilenler</h2>
          {rejectedOffers.length > 0 ? (
            <div className="user-panel-myoffer-item-right">
              <img
                src={`http://localhost:5000/${rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]?.userMakeOfferCars?.[0]?.imageUrl}`}
                alt={
                  rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                    ?.userMakeOfferCars?.[0]?.brand || "Araba resmi"
                }
                className="user-panel-myoffer-img-right"
              />
              <p className="user-panel-myoffer-item-right-p">
                <strong>
                  {rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                    ?.userMakeOfferCars?.[0]?.brand  || "Bilinmiyor"} 
                </strong>
                {" "}
                {rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.model || "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-right-p">
                <strong>Yıl:</strong>{" "}
                {rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.year || "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-right-p">
                <strong>Fiyat:</strong>{" "}
                {rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                  ?.userMakeOfferCars?.[0]?.price
                  ? `${rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]?.userMakeOfferCars?.[0]?.price} $`
                  : "Bilinmiyor"}
              </p>
              <p className="user-panel-myoffer-item-right-p price-right-offer">
                <strong>Teklifim:</strong>{" "}
                {rejectedOffers[currentRejectedIndex]?.userMakeOffers?.[0]
                  ?.offer || "Bilinmiyor"}{" "}
                $
              </p>
            </div>
          ) : (
            <p className="user-panel-myoffer-empty">
              Henüz reddedilen teklif yok.
            </p>
          )}
          <div className="button-container-right">
            <button
              className="btn-user-offer-prev-right"
              onClick={handlePrevRejected}
              disabled={currentRejectedIndex === 0}
            >
              <FaArrowLeft />
            </button>
            <button
              className="btn-user-offer-next-right"
              onClick={handleNextRejected}
              disabled={currentRejectedIndex === rejectedOffers.length - 1}
            >
              <FaArrowRight />
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Agreements;
