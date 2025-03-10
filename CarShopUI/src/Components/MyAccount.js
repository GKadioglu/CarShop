import React, { useEffect, useState } from "react";
import "./Css/MyAccount.css";
import profile from "./Images/accountMenuImages/profile.png";
import { useAuth } from "./Shared/AuthContext";
import { useNavigate, useParams } from "react-router-dom";
import { fetchWithToken } from "./Shared/api";

function MyAccount() {
  const { token, email } = useAuth();
  const [unreadCount, setUnreadCount] = useState(0);
  const { userName } = useParams();
  const navigate = useNavigate();
  const [acceptedOffersCount, setAcceptedOffersCount] = useState(0);

  // Hooks: En üst seviyede tanımlanmalı
  const [user, setUser] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    userName: "",
  });
  const [favoriteCars, setFavoriteCars] = useState([]);
  const [visibleCars, setVisibleCars] = useState(2);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [error, setError] = useState("");
  const [messages, setMessages] = useState([]);
  const [currentMessageIndex, setCurrentMessageIndex] = useState(0);
  const [favoriteCarsNumber, setFavoriteCarsNumber] = useState([]);

  // Kullanıcı verilerini almak için useEffect
  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    const fetchUserData = async () => {
      try {
        if (!userName) {
          setError("Kullanıcı adı bulunamadı.");
          return;
        }

        const response = await fetchWithToken(
          `http://localhost:5000/api/users/getUser/${userName}`,
          { method: "GET" }
        );

        const data = await response.json();

        if (!response.ok) {
          setError(
            data.message || "Kullanıcı bilgileri alınırken bir hata oluştu."
          );
        } else {
          setUser(data);
          setFormData({
            firstName: data.firstName,
            lastName: data.lastName,
            userName: data.userName,
          });
        }
      } catch (err) {
        setError("Sunucu ile bağlantı hatası");
      }
    };

    fetchUserData();
  }, [token, userName, navigate]);

  useEffect(() => {
    if (!token || !userName) return;

    const fetchNotifications = async () => {
      try {
        const response = await fetchWithToken(
          "http://localhost:5000/api/users/getNotifications",
          { method: "GET" }
        );
        const data = await response.json();

        if (response.ok) {
          const unreadNotifications = data.data.filter((notification) => {
            const userNotification =
              notification.addNotificationUserModels.find(
                (n) => n.userName === userName
              );
            return userNotification && !userNotification.reads;
          });
          setUnreadCount(unreadNotifications.length);
        }
      } catch (error) {
        console.error("Error fetching notifications:", error);
      }
    };

    fetchNotifications();
  }, [token, userName]);
  useEffect(() => {
    if (!token || !email) return;

    const fetchAdminOffers = async () => {
      try {
        const response = await fetch(
          `http://localhost:5000/api/users/getAdminOffer?email=${email}`,
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        const data = await response.json();

        if (response.ok) {
          // Filtrele, acceptance true olanları say
          const acceptedOffers = data.data.filter(
            (offer) => offer.acceptance === true
          );
          setAcceptedOffersCount(acceptedOffers.length);
        }
      } catch (error) {
        console.error("Error fetching admin offers:", error);
      }
    };

    fetchAdminOffers();
  }, [token, email]);
  // Favori araçları almak için useEffect
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
  useEffect(() => {
    if (!token) return;

    fetchWithToken("http://localhost:5000/api/users/getFavoriteCars", {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data)) {
          setFavoriteCarsNumber(data);
        } else {
          console.error("Favori araçlar verisi bir array değil:", data);
        }
      })
      .catch((error) => {
        console.error("Favori araçlar alınamadı:", error);
      });
  }, [token]);

  // Mesajları almak için useEffect
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

  // Mesajlar arasında geçiş yapmak için
  const handleNextMessage = () => {
    if (messages.length === 0) return;
    const nextIndex = (currentMessageIndex + 1) % messages.length;
    setCurrentMessageIndex(nextIndex);
  };

  const handleInceleClick = (carId) => {
    navigate(`/car/${carId}`);
  };

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

  // Formdaki değişiklikleri yönetmek için
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  // Formu kaydetmek için
  const handleSave = async () => {
    try {
      console.log("Gönderilen form verisi:", JSON.stringify(formData)); // Form verisini logla

      const response = await fetchWithToken(
        "http://localhost:5000/api/users/userEdit", // API URL'nizi burada belirtin
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(formData), // Form verilerini JSON formatında gönderiyoruz
        }
      );

      const data = await response.json(); // Yanıtı JSON'a dönüştür

      if (!response.ok) {
        setError(data.message || "Bilgiler güncellenirken bir hata oluştu.");
      } else {
        setUser(data.userName); // Kullanıcıyı güncelle
        setIsEditing(false); // Düzenleme modundan çık
        window.location.reload(); // Sayfayı yenileyerek güncellenmiş verilerle tekrar yüklenmesini sağla
      }
    } catch (err) {
      setError("Sunucu ile bağlantı hatası.");
    }
  };

  // Düzenleme modunu açıp kapatmak için
  const toggleEditMode = () => {
    setIsEditing((prev) => !prev);
  };

  // Error veya loading durumları için conditional render
  if (error) {
    return <div>{error}</div>;
  }

  if (!user) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div className="my-account">
      {/* Sol Bölüm: Hesap Bilgileri ve Favori Araçlar */}
      <div className="account-left-side">
        {/* Hesap Bilgileri */}
        <div className="account-user-info">
          <h2>Hesap Bilgileriniz</h2>
          <div className="account-user-details">
            <img
              className="account-user-profile-photo"
              src={profile}
              alt="Profil"
            />
            <div className="account-user-text">
              {isEditing ? (
                <>
                  <p>
                    <strong>Kullanıcı Adı:</strong>
                    <input
                      className="edit-user-input"
                      type="text"
                      name="userName"
                      value={formData.userName}
                      readOnly
                    />
                  </p>

                  <p>
                    <strong>Ad:</strong>
                    <input
                      className="edit-user-input"
                      type="text"
                      name="firstName"
                      value={formData.firstName}
                      onChange={handleInputChange}
                    />
                  </p>
                  <p>
                    <strong>Soyad:</strong>
                    <input
                      className="edit-user-input"
                      type="text"
                      name="lastName"
                      value={formData.lastName}
                      onChange={handleInputChange}
                    />
                  </p>
                  <button className="account-btn-save" onClick={handleSave}>
                    Kaydet
                  </button>
                </>
              ) : (
                <>
                  <p>
                    <strong>Kullanıcı Adı:</strong> {user.userName}
                  </p>
                  <p>
                    <strong>Ad Soyad:</strong> {user.firstName} {user.lastName}
                  </p>
                  <p>
                    <strong>E-posta:</strong> {user.email}
                  </p>
                  <button className="account-btn-edit" onClick={toggleEditMode}>
                    Hesap Bilgilerini Düzenle
                  </button>
                </>
              )}
            </div>
          </div>
        </div>

        {/* Favori Araçlarım */}
        <section id="favorite-cars" className="favorite-cars">
          <h3 className="favorite-cars-h3">Favori Araçlarım</h3>
          <div className="account-favorite-cars-list">
            <button className="btn-account-prev" onClick={handlePrev}>
              {"<"}
            </button>{" "}
            {/* Sol ok */}
            {favoriteCars && favoriteCars.length > 0 ? (
              favoriteCars
                .slice(currentIndex, currentIndex + visibleCars)
                .map((car) => (
                  <div key={car.carId} className="account-car-card">
                    <img
                      src={`http://localhost:5000/${car.imageUrl}`}
                      alt={car.model}
                      className="account-car-card-image"
                    />
                    <div className="account-car-details">
                      <p>
                        {car.brand} {car.model}
                      </p>
                      <p className="account-car-card-price">
                        <strong>Fiyat:</strong> {car.price}$
                      </p>
                      <button
                        className="btn-favorite-incele"
                        onClick={() => handleInceleClick(car.carId)}
                      >
                        İncele
                      </button>
                    </div>
                  </div>
                ))
            ) : (
              <p>Favori araç bulunamadı.</p>
            )}
            <button className="btn-account-next" onClick={handleNext}>
              {">"}
            </button>{" "}
            {/* Sağ ok */}
          </div>
        </section>
      </div>

      {/* Sağ Bölüm: Menü Kartları ve Mesajlar */}
      <div className="account-right-side">
        {/* Menü Kartları */}
        <div className="account-menu-cards">
          <div className="account-card card-setting-1">
            <a href="/accountSettings" className="account-card-link">
              <h3 className="account-card-h3">Hesap Ayarlarım</h3>
            </a>
          </div>
          <div className="account-card card-setting-2">
            <a href="/favourite" className="account-card-link">
              <h3 className="account-card-h3">Favori Araçlarım</h3>

              {/* Favori araç sayısı göstergesi */}
              {favoriteCars.length > 0 && (
                <span className="favorite-badge">
                  {favoriteCarsNumber.length}
                </span>
              )}
            </a>
          </div>
          <div className="account-card card-setting-6">
            <a href="/agreements" className="account-card-link">
              <h3 className="account-card-h3">
                Anlaşmalar
                {acceptedOffersCount > 0 && (
                  <span className="notification-badge-offer">
                    {acceptedOffersCount}
                  </span>
                )}
              </h3>
            </a>
          </div>
          <div className="account-card card-setting-3">
            <a href="/message" className="account-card-link">
              <h3 className="account-card-h3">Mesajlarım</h3>
              {messages.length > 0 && (
                <span className="message-count-notification">{messages.length}</span>
              )}
            </a>
          </div>

          <div className="account-card card-setting-4">
            <a href="/notifications" className="account-card-link">
              {unreadCount > 0 && (
                <span className="user-panel-notification-badge">
                  {unreadCount}
                </span>
              )}
              <h3 className="account-card-h3">Bildirimler</h3>
            </a>
          </div>
          <div className="account-card card-setting-5">
            <a href="/help" className="account-card-link">
              <h3 className="account-card-h3">Yardım ve Destek</h3>
            </a>
          </div>
        </div>

        <div className="account-messages">
          {messages.length > 0 ? (
            <>
              <h4 className="account-message-h3">
                Gönderen: {messages[currentMessageIndex]?.sender}
              </h4>
              <p>
                <strong className="account-message-h4">Mesaj: </strong>
                {messages[currentMessageIndex]?.message}
              </p>
              <p className="account-message-message">
                <strong className="account-message-h4">Alıcı: </strong>
                {messages[currentMessageIndex]?.receiver}
              </p>

              {/* Mesaj değiştirme butonu */}
              <div className="message-switch-container">
                <button
                  onClick={handleNextMessage}
                  className="message-switch-icon"
                >
                  <i className="fas fa-arrow-right"></i>
                  {" > "}
                </button>
              </div>

              {/* Alıntılanan mesajlar */}
              {messages[currentMessageIndex]?.anonimMessages &&
                messages[currentMessageIndex].anonimMessages.length > 0 && (
                  <div className="quoted-message">
                    <h5 className="account-message-h4">Alıntılanan Mesaj:</h5>
                    <div className="quoted-message-card">
                      <p>
                        <strong className="account-message-h4">
                          {
                            messages[currentMessageIndex].anonimMessages[0]
                              .fullName
                          }
                          :{" "}
                        </strong>
                        {
                          messages[currentMessageIndex].anonimMessages[0]
                            .message
                        }{" "}
                        {messages[
                          currentMessageIndex
                        ].anonimMessages[0].anonimMessageCars.map(
                          (car, index) => (
                            <button
                              className="quoted-message-card-button"
                              onClick={() => handleInceleClick(car.carId)}
                            >
                              {car.brand} {car.model} ({car.year})
                            </button>
                          )
                        )}
                      </p>
                    </div>
                  </div>
                )}
            </>
          ) : (
            <p>Mesaj bulunamadı.</p>
          )}
        </div>
      </div>
    </div>
  );
}

export default MyAccount;
