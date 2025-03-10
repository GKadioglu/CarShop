import React, { useEffect, useState } from "react";
import { fetchWithToken } from "./Shared/api";
import { useAuth } from "./Shared/AuthContext";
import "./Css/Notifications.css";

function Notifications() {
  const { token, userName } = useAuth(); // userName'i ekledik
  const [notifications, setNotifications] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (!token || !userName) return; // Kullanıcı giriş yapmamışsa çalıştırma

    const fetchNotifications = async () => {
      try {
        const response = await fetchWithToken(
          "http://localhost:5000/api/users/getNotifications",
          { method: "GET" }
        );
        const data = await response.json();

        if (response.ok) {
          const formattedNotifications = data.data.map((notification) => {
            const userNotification = notification.addNotificationUserModels.find(
              (n) => n.userName === userName // Dinamik hale getirildi
            );
            return {
              ...notification,
              reads: userNotification ? userNotification.reads : false,
              notificationsId: userNotification ? userNotification.notificationsId : null,
            };
          });

          setNotifications(formattedNotifications);
        } else {
          setError(data.message || "Failed to fetch notifications");
        }
      } catch (error) {
        setError("An error occurred while fetching notifications");
        console.error("Error fetching notifications:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchNotifications();
  }, [token, userName]); // userName'i bağımlılığa ekledik

  const toggleReadStatus = async (notificationsId, currentStatus) => {
    try {
      const newStatus = !currentStatus;
      const updatedNotifications = notifications.map((notification) =>
        notification.notificationsId === notificationsId
          ? { ...notification, reads: newStatus }
          : notification
      );

      setNotifications(updatedNotifications);

      const response = await fetchWithToken(
        "http://localhost:5000/api/users/updateNotifications",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            NotificationsId: notificationsId,
            Reads: newStatus,
          }),
        }
      );

      const data = await response.json();

      if (!response.ok) {
        console.error("Bildirim güncelleme hatası:", data);
      }
    } catch (error) {
      console.error("toggleReadStatus Hatası:", error);
    }
  };

  if (loading) return <p>Loading notifications...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div className="user-notifications-container">
      <h2 className="user-notification-h2">Bildirimlerim</h2>
      {notifications.length === 0 ? (
        <p className="user-notification-no-notifications">No notifications found.</p>
      ) : (
        <ul className="user-notification-ul">
          {notifications.map((notification) => (
            <li
              key={notification.notificationsId}
              className={`user-notification-item ${
                notification.reads ? "notification-read" : "notification-unread"
              }`}
            >
              <h3 className="user-notification-h3">{notification.title}</h3>
              <p className="user-notification-p">{notification.description}</p>
              <small className="user-notification-small">
                From: {notification.sender} | {new Date(notification.createdDate).toLocaleString()}
              </small>
              <button
                className={`user-notification-toggle-read-status ${
                  notification.reads ? "notification-button-read" : "notification-button-unread"
                }`}
                onClick={() => toggleReadStatus(notification.notificationsId, notification.reads)}
              >
                {notification.reads ? "Okunmadı" : "Okundu"}
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default Notifications;