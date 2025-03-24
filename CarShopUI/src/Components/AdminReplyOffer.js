import React, { useEffect, useState } from "react";
import "./Css/AdminReplyOffer.css";
import { fetchWithToken } from "./Shared/api";

function AdminReplyOffer() {
  const [offers, setOffers] = useState([]); 
  const [selectedOffer, setSelectedOffer] = useState(null);
  const [acceptedOffers, setAcceptedOffers] = useState([]); 
  const [rejectedOffers, setRejectedOffers] = useState([]); 
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedFilteredOffer, setSelectedFilteredOffer] = useState(null);

  useEffect(() => {
    const fetchOffers = async () => {
      try {
        const response = await fetchWithToken(
          "http://localhost:5000/api/admin/getUserOffer"
        );
        if (!response.ok) {
          throw new Error("Teklifleri getirirken hata oluştu.");
        }
        const data = await response.json();

        const unansweredOffers = data.filter(
          (offer) =>
            !offer.makeAnOfferCars.some(
              (car) => car.adminOffers && car.adminOffers.length > 0
            )
        );

        setOffers(unansweredOffers);
        setSelectedOffer(
          unansweredOffers.length > 0 ? unansweredOffers[0] : null
        );
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    const fetchAdminReplies = async () => {
      try {
        const response = await fetchWithToken(
          `http://localhost:5000/api/admin/getAdminOffer`
        );
        if (!response.ok) {
          throw new Error("Admin teklif yanıtlarını alırken hata oluştu.");
        }
        const data = await response.json();
        const accepted = data.data.filter((offer) => offer.acceptance === true);
        const rejected = data.data.filter(
          (offer) => offer.acceptance === false
        );
        setAcceptedOffers(accepted);
        setRejectedOffers(rejected);
      } catch (err) {
        console.error("Admin teklif yanıtları çekilirken hata:", err);
      }
    };

    fetchOffers();
    fetchAdminReplies();
  }, []);

  const handleReplyOffer = async (acceptance) => {
    if (!selectedOffer) return;

    try {
      const response = await fetchWithToken(
        "http://localhost:5000/api/admin/replyOffer",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            sender: "admin",
            receiver: selectedOffer.fullName,
            acceptance: acceptance,
            userOfferId: selectedOffer.userOfferId,
          }),
        }
      );

      const result = await response.json();
      if (!response.ok) {
        throw new Error(result.message || "Teklif yanıtlanırken hata oluştu.");
      }

      alert(result.message);

      window.location.reload();
    } catch (err) {
      alert(err.message);
    }
  };
  const handleSelectFilteredOffer = (offer) => {
    setSelectedFilteredOffer(offer);
    setSelectedOffer(null); 
  };

  const selectedDetailOffer = selectedOffer || selectedFilteredOffer;
  if (loading) return <div>Yükleniyor...</div>;
  if (error) return <div>Hata: {error}</div>;
  return (
    <div>
      <div className="admin-reply-offer-panel">
        {/* Yeni Teklifler */}
        <div className="admin-reply-offer-list">
          <h1 className="admin-reply-offer-list-title">Yeni Teklifler</h1>
          {/* Eğer yeni teklifler yoksa, mesaj göster */}
          {offers.length === 0 ? (
            <p className="admin-panel-no-offer">Yeni teklif yok.</p>
          ) : (
            offers.map((offer) => (
              <div
                key={offer.userOfferId}
                className={`admin-reply-offer-item ${
                  selectedOffer?.userOfferId === offer.userOfferId
                    ? "selected"
                    : ""
                }`}
                onClick={() => {
                  setSelectedOffer(offer);
                  setSelectedFilteredOffer(null); // Önceki yanıtlanmış teklif seçimlerini temizle
                }}
              >
                <p>
                  {offer.fullName} - {offer.offer} $
                </p>
              </div>
            ))
          )}
        </div>

        {/* Seçili Teklif Detayı */}
        {selectedDetailOffer && (
          <div className="admin-reply-offer-detail">
            <div className="admin-reply-offer-detail-left">
              <h2>Teklif Detayı</h2>
              <p>
                <strong>Ad Soyad:</strong>{" "}
                {selectedDetailOffer.fullName || selectedDetailOffer.receiver}
              </p>
              <p>
                <strong>Email:</strong>{" "}
                {selectedDetailOffer.email ||
                  selectedDetailOffer.userMakeOffers?.[0]?.email ||
                  "Bilinmiyor"}
              </p>
              <p>
                <strong>Telefon:</strong>{" "}
                {selectedDetailOffer.phoneNumber ||
                  selectedDetailOffer.userMakeOffers?.[0]?.phoneNumber ||
                  "Belirtilmemiş"}
              </p>
              <p className="admin-reply-offer-detail-price">
                <strong>Teklif:</strong>{" "}
                {selectedDetailOffer.offer ||
                  selectedDetailOffer.userMakeOffers?.[0]?.offer ||
                  "Bilinmiyor"}{" "}
                $
              </p>

              {/* Kabul veya Red durumu göster */}
              {selectedFilteredOffer?.acceptance !== undefined && (
                <p
                  className={`admin-offer-reply-status ${
                    selectedFilteredOffer.acceptance
                      ? "accepted-detail"
                      : "rejected-detail"
                  }`}
                >
                  {selectedFilteredOffer.acceptance
                    ? "✅ Kabul Edilmiş"
                    : "❌ Reddedilmiş"}
                </p>
              )}

              {/* Eğer teklif yeni değilse, butonları gösterme */}
              {selectedOffer && (
                <>
                  <button
                    className="admin-reply-offer-button-left"
                    onClick={() => handleReplyOffer(true)}
                  >
                    Kabul Et
                  </button>
                  <button
                    className="admin-reply-offer-button-right"
                    onClick={() => handleReplyOffer(false)}
                  >
                    Reddet
                  </button>
                </>
              )}
            </div>
            <div className="admin-reply-offer-detail-car">
              {(
                selectedDetailOffer.makeAnOfferCars ||
                selectedDetailOffer.userMakeOffers?.[0]?.userMakeOfferCars
              )?.map((car) => (
                <div key={car.carId} className="admin-reply-offer-car-info">
                  <img
                    src={`http://localhost:5000/${car.imageUrl}`}
                    alt={car.brand}
                    className="admin-reply-offer-car-image"
                  />
                  <p>
                    {car.brand} {car.model} ({car.year})
                  </p>
                  <p>
                    <strong>Fiyat:</strong> {car.price} $
                  </p>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
      {/* Kabul Edilen ve Reddedilen Teklifler */}
      <div className="admin-reply-offer-filtered">
        <div className="admin-reply-accepted">
          <h2 className="admin-reply-accepted-h2">Kabul Edilenler</h2>
          {acceptedOffers.map((offer) => (
            <div
              key={offer.userOfferId}
              className="admin-reply-offer-items accepted"
              onClick={() => {
                handleSelectFilteredOffer(offer);
                setSelectedOffer(null); // Önceki yeni teklif seçimlerini temizle
              }}
            >
              <p>
                {offer.receiver} -{" "}
                {offer.userMakeOffers?.[0]?.offer || "Bilinmiyor"} $
              </p>
            </div>
          ))}
        </div>

        <div className="admin-reply-rejected">
          <h2 className="admin-reply-rejected-h2">Reddedilenler</h2>
          {rejectedOffers.map((offer) => (
            <div
              key={offer.userOfferId}
              className="admin-reply-offer-items rejected"
              onClick={() => handleSelectFilteredOffer(offer)}
            >
              <p>
                {offer.receiver} -{" "}
                {offer.userMakeOffers?.[0]?.offer || "Bilinmiyor"} $
              </p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default AdminReplyOffer;
