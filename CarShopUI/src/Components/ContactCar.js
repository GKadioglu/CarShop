import React, { useEffect, useState } from "react";
import "../Components/Css/ContactCar.css";
import { useParams } from "react-router-dom";
import { MdFullscreen, MdCloseFullscreen } from "react-icons/md";
import { useAuth } from "./Shared/AuthContext";
import { fetchWithToken } from "./Shared/api";

const ContactCar = () => {
  const { carName } = useParams();
  const { userName, token } = useAuth(); // AuthContext'ten doğru state'leri alıyoruz
  const [car, setCar] = useState(null);
  const [form, setForm] = useState({
    name: "",
    email: "",
    phone: "",
    message: "",
  });
  const [formStatus, setFormStatus] = useState("");
  const [isFullScreen, setIsFullScreen] = useState(false);

  // Kullanıcı bilgilerini al
  useEffect(() => {
    if (userName && token) {
      fetchWithToken(
        `http://localhost:5000/api/users/getUser/${userName}`,
        token // fetchWithToken çağrısına token parametresi eklenmeli
      )
        .then((response) => {
          if (!response.ok) throw new Error("Kullanıcı bilgisi alınamadı");
          return response.json();
        })
        .then((userData) => {
          setForm((prevForm) => ({
            ...prevForm,
            name: `${userData.firstName} ${userData.lastName}`|| "",
            email: userData.email || "",
            phone: userData.phone || "",
          }));
        })
        .catch((error) => console.error(error.message));
    }
  }, [userName, token]);

  // Araç bilgilerini al
  useEffect(() => {
    fetch(`http://localhost:5000/api/car/contact/${encodeURIComponent(carName)}`)
      .then((response) => {
        if (!response.ok) throw new Error("Araç bulunamadı");
        return response.json();
      })
      .then((data) => {
        console.log(data); // Yanıtı kontrol et
        setCar(data.data); // Eğer yanıt "data" içinde ise burayı değiştir
      })
      .catch((error) => console.error(error.message));
  }, [carName]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!form.name || !form.email || !form.message) {
      setFormStatus("Lütfen gerekli alanları doldurunuz.");
      return;
    }

    fetch("http://localhost:5000/api/controller/anonimMessage", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        fullName: form.name,
        email: form.email,
        phone: form.phone,
        message: form.message,
        carId: car?.carId,
      }),
    })
      .then((response) => {
        if (response.ok) {
          setFormStatus("Mesajınız başarıyla gönderildi!");
          setForm({ name: "", email: "", phone: "", message: "" });
        } else {
          setFormStatus("Mesaj gönderimi başarısız oldu.");
        }
      })
      .catch((error) => setFormStatus("Hata oluştu: " + error.message));
  };

  return (
    <div
      className={`contact-page-container ${
        isFullScreen ? "fullscreen-background" : ""
      }`}
    >
      {car ? (
        <div className="contact-page-content">
          <div className="car-info-card">
            <div
              className={`car-contact-image-container ${
                isFullScreen ? "fullscreen" : ""
              }`}
            >
              <img
                src={`http://localhost:5000/${car.imageUrl}`}
                alt={`${car.brand} ${car.model}`}
                className="car-contact-image"
              />
              {!isFullScreen && (
                <button
                  className="zoom-icon"
                  onClick={() => setIsFullScreen(true)}
                >
                  <MdFullscreen />
                </button>
              )}
              {isFullScreen && (
                <button
                  className="close-fullscreen"
                  onClick={() => setIsFullScreen(false)}
                >
                  <MdCloseFullscreen />
                </button>
              )}
            </div>
            <div className="car-details">
              <h1>{`${car.brand} ${car.model}`}</h1>
              <h2>{`(${car.year})`}</h2>
              <p className="car-price">Fiyat: {car.price} $</p>
              <ul className="car-models">
                {car.models.map((model) => (
                  <li key={model.modelId}>Origin - {model.origin}</li>
                ))}
              </ul>
              <ul className="car-categories">
                {car.categories.map((category) => (
                  <li key={category.categoryId}>Type - {category.name}</li>
                ))}
              </ul>
            </div>
          </div>

          <form className="contact-form" onSubmit={handleSubmit}>
            <h3>İletişim Formu</h3>
            <div className="form-group">
              <label htmlFor="name">Adınız:</label>
              <input
                type="text"
                id="name"
                name="name"
                value={form.name}
                onChange={handleChange}
                placeholder="Adınızı giriniz"
              />
            </div>
            <div className="form-group">
              <label htmlFor="email">E-posta:</label>
              <input
                type="email"
                id="email"
                name="email"
                value={form.email}             
                onChange={handleChange}
                placeholder="E-posta adresinizi giriniz"
              />
            </div>
            <div className="form-group">
              <label htmlFor="phone">Telefon:</label>
              <input
                type="text"
                id="phone"
                name="phone"
                value={form.phone}
                onChange={handleChange}
                placeholder="Telefon numaranızı giriniz (opsiyonel)"
              />
            </div>
            <div className="form-group">
              <label htmlFor="message">Mesajınız:</label>
              <textarea
                id="message"
                name="message"
                value={form.message}
                onChange={handleChange}
                placeholder="Satıcıya iletmek istediğiniz mesaj"
              />
            </div>
            <button type="submit" className="submit-btn">
              Mesaj Gönder
            </button>
            {formStatus && <p className="form-status">{formStatus}</p>}
          </form>
        </div>
      ) : (
        <p>Yükleniyor...</p>
      )}
    </div>
  );
};

export default ContactCar;
