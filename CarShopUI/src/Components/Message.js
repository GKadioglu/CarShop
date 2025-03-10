import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "./Shared/AuthContext";
import { fetchWithToken } from "./Shared/api";
import "./Css/MessagePage.css";

const MessagesPage = () => {
  const { token, email } = useAuth();
  const [messages, setMessages] = useState([]);
  const [currentMessageIndex, setCurrentMessageIndex] = useState(0);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

  
  const deleteMessage = async (messageId) => {
    try {
      const response = await fetchWithToken(
        `http://localhost:5000/api/users/deleteMessage/${messageId}`,
        { method: "DELETE" }
      );
  
      const data = await response.json();
  
      if (!response.ok) {
        alert(data.message || "Mesaj silinirken bir hata oluştu.");
      } else {
        setMessages((prevMessages) =>
          prevMessages.filter((msg) => msg.messageId !== messageId)
        );
        window.location.reload(); // ✅ Sayfayı yenile
      }
    } catch (err) {
      alert("Sunucu ile bağlantı hatası.");
    }
  };

  useEffect(() => {
    if (!token || !email) {
      navigate("/login");
      return;
    }

    const fetchAdminMessages = async () => {
      try {
        const response = await fetchWithToken(
          `http://localhost:5000/api/users/getAdminMessage?email=${email}`,
          { method: "GET" }
        );

        const data = await response.json();
        console.log("Gelen mesajlar:", data.data); // 🔍 Konsolda mesajları kontrol et

        if (!response.ok) {
          setError(data.data.Message || "Mesajlar alınırken bir hata oluştu.");
        } else {
          setMessages(data.data);
        }
      } catch (err) {
        setError("Sunucu ile bağlantı hatası.");
      }
    };

    fetchAdminMessages();
  }, [token, email, navigate]);

  const handleNextMessage = () => {
    if (messages.length === 0) return;
    setCurrentMessageIndex((prevIndex) => (prevIndex + 1) % messages.length);
  };

  const handlePrevMessage = () => {
    if (messages.length === 0) return;
    setCurrentMessageIndex((prevIndex) =>
      prevIndex === 0 ? messages.length - 1 : prevIndex - 1
    );
  };

  return (
    <div className="account-messagepage-container">
      <h2 className="account-messagepage-title">Mesajlarım</h2>
      {error && <p className="account-messagepage-error">{error}</p>}
      {messages.length > 0 ? (
        <div className="account-messagepage-card">
          <h4 className="account-messagepage-sender">
            Gönderen: {messages[currentMessageIndex]?.sender}
          </h4>
          <p className="account-messagepage-message">
            <h5 className="account-messagepage-message-title">Mesaj:</h5>
            <p className="account-messagepage-message-answer">
              {messages[currentMessageIndex]?.message}
            </p>
          </p>
          <p className="account-messagepage-receiver">
            <strong>Alıcı:</strong> {messages[currentMessageIndex]?.receiver}
          </p>

          {/* Araç Bilgisi */}
          {messages[currentMessageIndex]?.car && (
            <div className="account-messagepage-car">
              <img
                src={messages[currentMessageIndex].car.imageUrl}
                alt={`${messages[currentMessageIndex].car.brand} ${messages[currentMessageIndex].car.model}`}
                className="account-messagepage-car-image"
              />
              <p className="account-messagepage-car-info">
                {messages[currentMessageIndex].car.brand}{" "}
                {messages[currentMessageIndex].car.model} (
                {messages[currentMessageIndex].car.year})
              </p>
            </div>
          )}

          <div className="account-messagepage-navigation">
            <button
              onClick={handlePrevMessage}
              className="account-messagepage-button-left"
            >
              Önceki
            </button>
            <button
              onClick={() => {
                const messageId = messages[currentMessageIndex]?.adminMessageId; 
                if (!messageId) {
                  console.error("Hata: Silinecek mesajın ID'si bulunamadı.");
                  alert("Bu mesajın ID'si bulunamadı, lütfen tekrar deneyin.");
                  return;
                }
                deleteMessage(messageId);
              }}
              className="account-messagepage-button-delete"
            >
              Sil
            </button>
            <button
              onClick={handleNextMessage}
              className="account-messagepage-button-right"
            >
              Sonraki
            </button>
          </div>

          {/* Alıntılanan mesajlar */}
          {messages[currentMessageIndex]?.anonimMessages &&
            messages[currentMessageIndex].anonimMessages.length > 0 && (
              <div className="account-messagepage-quoted-user">
                <h5 className="account-messagepage-quoted-title">
                  Alıntılanan Mesaj:
                </h5>
                <div className="account-messagepage-quoted-card-user">
                  <p>
                    <strong className="account-messagepage-quoted-sender">
                      {messages[currentMessageIndex].anonimMessages[0].fullName}
                      :
                    </strong>
                    {messages[currentMessageIndex].anonimMessages[0].message}
                    {messages[
                      currentMessageIndex
                    ].anonimMessages[0].anonimMessageCars.map((car) => (
                      <button
                        key={car.carId}
                        className="account-messagepage-quoted-button"
                        onClick={() => handleInceleClick(car.carId)}
                      >
                        {car.brand} {car.model} ({car.year})
                      </button>
                    ))}
                  </p>
                </div>
              </div>
            )}
        </div>
      ) : (
        <p className="account-messagepage-no-message">Henüz mesajınız yok.</p>
      )}
    </div>
  );
};

export default MessagesPage;
