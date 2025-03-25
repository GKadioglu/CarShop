import "./Css/AdminMessage.css";
import React, { useEffect, useState } from "react";
import { fetchWithToken } from "./Shared/api";

function AdminMessage() {
  const [messages, setMessages] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedMessage, setSelectedMessage] = useState(null);
  const [sender] = useState("admin"); 
  const [receiver, setReceiver] = useState(""); 
  const [message, setMessage] = useState(""); 

  useEffect(() => {
    const fetchMessages = async () => {
      try {
        const response = await fetchWithToken(
          "http://localhost:5000/api/admin/getMessage"
        );
        if (!response.ok) {
          throw new Error("Mesajları getirirken hata oluştu.");
        }
        const data = await response.json();
        setMessages(data);
        setSelectedMessage(data[0]); 
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchMessages();
  }, []);

  const handleSelectMessage = (message) => {
    setSelectedMessage(message);
    setReceiver(message.fullName); 
  };

  const handleReplySubmit = async () => {
    try {
      const response = await fetchWithToken(
        "http://localhost:5000/api/admin/answerMessage",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            sender,
            receiver,
            message,
            messageId: selectedMessage.messageId,
          }),
        }
      );

      if (!response.ok) {
        throw new Error("Mesajı gönderirken hata oluştu.");
      }

      const data = await response.json();
      console.log("Yanıt başarıyla gönderildi:", data);

      const updatedMessages = messages.map((msg) =>
        msg.messageId === selectedMessage.messageId
          ? { ...msg, isReplied: true }
          : msg
      );
      setMessages(updatedMessages);
      setMessage(""); // Mesajı sıfırla
    } catch (err) {
      setError(err.message);
    }
  };

  if (loading) {
    return <div className="admin-message-loading">Yükleniyor...</div>;
  }

  if (error) {
    return <div className="admin-message-error">Hata: {error}</div>;
  }

  if (messages.length === 0) {
    return <div className="admin-message-empty">Hiç mesaj bulunamadı.</div>;
  }

  return (
    <div className="body-message">
      <div className="admin-message-container">
        <div className="admin-message-list">
          <h1 className="admin-message-panel-title">Mesajlar</h1>
          {messages.map((message, index) => (
            <div
              key={index}
              className="admin-message-item"
              onClick={() => handleSelectMessage(message)} 
            >
              <div className="admin-message-header">
                <h3>{message.fullName}</h3> {/* Yalnızca kişinin adı */}
                {message.isReplied && (
                  <span className="admin-message-replied-tag">Cevaplandı</span>
                )}
              </div>
            </div>
          ))}
        </div>

        {selectedMessage && (
          <div className="admin-message-detail">
            <div className="admin-message-car">
              <div className="admin-message-car-details">
                <img
                  src={`http://localhost:5000/${selectedMessage.anonimMessageCars[0].imageUrl}`}
                  alt={selectedMessage.anonimMessageCars[0].model}
                  className="admin-message-car-img"
                />
                <p className="admin-message-car-details-p">
                  {selectedMessage.anonimMessageCars[0].brand}{" "}
                  {selectedMessage.anonimMessageCars[0].model} (
                  {selectedMessage.anonimMessageCars[0].year})
                </p>
                <p>{selectedMessage.anonimMessageCars[0].price} $</p>
              </div>
            </div>
            <div className="admin-message-content">
              <h2>{selectedMessage.fullName}</h2>
              <p>{selectedMessage.message}</p>
              <div className="admin-message-contact-info">
                <p>Email: {selectedMessage.email}</p>
                <p>Telefon: {selectedMessage.phoneNumber}</p>
              </div>
            </div>
          </div>
        )}
      </div>

      <div className="admin-message-reply-container">
        <h2 className="admin-message-reply-container-h2">Cevapla</h2>

        <div className="admin-message-input-group">
          <label className="admin-message-input-group-label">Gönderen</label>
          <input type="text" value={sender} readOnly />

          <label className="admin-message-input-group-label">Alıcı</label>
          <input
            type="text"
            value={receiver}
            readOnly
            onChange={(e) => setReceiver(e.target.value)}
          />
        </div>

        <div>
          <label className="admin-message-textarea-label">Mesaj</label>
          <textarea
            value={message}
            onChange={(e) => setMessage(e.target.value)}
          ></textarea>
        </div>
        <button className="admin-message-submit" onClick={handleReplySubmit}>
          Gönder
        </button>
      </div>
    </div>
  );
}

export default AdminMessage;