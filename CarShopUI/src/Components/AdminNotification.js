import React, { useState } from "react";
import { fetchWithToken } from "./Shared/api";
import "./Css/AdminNotification.css";

function AdminNotification() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [createdDate, setCreatedDate] = useState(new Date().toISOString());
  const [responseMessage, setResponseMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newNotification = {
      sender: "admin", // sender kısmı her zaman "admin"
      title,
      description,
      createdDate
    };

    try {
      const response = await fetchWithToken("http://localhost:5000/api/admin/addNewNotification", {
        method: "POST",
        body: JSON.stringify(newNotification),
        headers: {
          "Content-Type": "application/json"
        }
      });

      if (response.ok) {
        setResponseMessage("Bildirim başarıyla gönderildi!");

        setTitle("");
        setDescription("");
        setCreatedDate(new Date().toISOString());
      } else {
        setResponseMessage("Bildirim gönderilirken bir hata oluştu.");
      }
    } catch (error) {
      setResponseMessage("Bir hata oluştu.");
      console.error("Error:", error);
    }
  };

  return (
    <div className="admin-notification-page">
      <h2 className="admin-notification-page-title">Yeni Bildirim Gönder</h2>
      <form className="admin-notification-page-form" onSubmit={handleSubmit}>
        <div className="admin-notification-page-form-div">
          <label className="admin-notification-page-form-label" htmlFor="title">Başlık</label>
          <input
            className="admin-notification-page-form-input"
            type="text"
            id="title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
        </div>

        <div className="admin-notification-page-form-div">
          <label className="admin-notification-page-form-label" htmlFor="description">Açıklama</label>
          <textarea
            className="admin-notification-page-form-textarea"
            id="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          ></textarea>
        </div>

        <div className="admin-notification-page-form-div">
          <label className="admin-notification-page-form-label" htmlFor="createdDate">Tarih</label>
          <input
            className="admin-notification-page-form-input"
            type="datetime-local"
            id="createdDate"
            value={createdDate}
            onChange={(e) => setCreatedDate(e.target.value)}
            required
          />
        </div>

        <button className="admin-notification-page-form-button" type="submit">Gönder</button>

        {/* Başarı mesajı */}
        {responseMessage && <p className="admin-notification-page-response-message">{responseMessage}</p>}
      </form>
    </div>
  );
}

export default AdminNotification;