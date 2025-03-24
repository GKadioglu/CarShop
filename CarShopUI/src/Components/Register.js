import React, { useState } from "react";
import "./Css/Register.css";
import { Canvas } from "@react-three/fiber";
import { OrbitControls, useGLTF } from "@react-three/drei";

function Car() {
  const { scene, loading } = useGLTF("/models/bmw3.glb");

  if (loading) {
    return <div>Loading...</div>;
  }

  return <primitive object={scene} scale={8.0} position={[0, 0, 0]} />;
}

function Register() {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    userName: "",
    email: "",
    password: "",
    rePassword: "",
  });

  const [message, setMessage] = useState(null);
  const [isRegistered, setIsRegistered] = useState(false);
  const [isLoading, setIsLoading] = useState(false); 
  const [passwordError, setPasswordError] = useState(""); 

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData({ ...formData, [id]: value });

    if (id === "password") {
      const passwordRegex =
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
      if (!passwordRegex.test(value)) {
        setPasswordError(
          "Şifre en az 8 karakter, küçük harf, büyük harf, rakam ve özel karakter içermelidir."
        );
      } else {
        setPasswordError(""); 
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (formData.password !== formData.rePassword) {
      setMessage({
        text: "Şifreler eşleşmiyor.",
        type: "error",
      });
      return;
    }

    if (passwordError) {
      setMessage({
        text: passwordError,
        type: "error",
      });
      return;
    }

    setIsLoading(true); 
    setMessage({
      text: "Yükleniyor...",
      type: "loading", 
    });

    try {
      const response = await fetch(
        "http://localhost:5000/api/account/register",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(formData),
        }
      );

      const result = await response.json();

      setTimeout(() => {
        setIsLoading(false); 
        if (response.ok) {
          setMessage({
            text: "Kayıt başarılı! Lütfen emailinizi kontrol edin.",
            type: "success",  
          });
          setIsRegistered(true); 
          setFormData({
            firstName: "",
            lastName: "",
            userName: "",
            email: "",
            password: "",
            rePassword: "",
          });
        } else {
          setMessage({
            text: result.errors ? result.errors.join(", ") : result.message,
            type: "error",
          });
        }
      }, 1000); 
    } catch (error) {
      setIsLoading(false); 
      setMessage({
        text: "Bir hata oluştu. Lütfen tekrar deneyiniz.",
        type: "error",  
      });
    }
  };

  return (
    <div className="register-container">
      {/* Sol taraf: Form */}
      <div className="form-register-section">
        <h2 className="form-register-title">Kayıt Ol</h2>
        
        {/* Mesajı göstermek için burada */}
        {message && (
          <p className={`message ${message.type} ${isLoading ? 'loading' : ''}`}>
            {message.text}
          </p>
        )}

        {/* Kayıt başarılıysa formu gizle */}
        {!isRegistered && (
          <form onSubmit={handleSubmit}>
            <div className="form-register-group">
              <label htmlFor="firstName">Ad</label>
              <input
                type="text"
                id="firstName"
                value={formData.firstName}
                onChange={handleChange}
                placeholder="Ad"
                required
              />
            </div>
            <div className="form-register-group">
              <label htmlFor="lastName">Soyad</label>
              <input
                type="text"
                id="lastName"
                value={formData.lastName}
                onChange={handleChange}
                placeholder="Soyad"
                required
              />
            </div>
            <div className="form-register-group">
              <label htmlFor="userName">Kullanıcı Adı</label>
              <input
                type="text"
                id="userName"
                value={formData.userName}
                onChange={handleChange}
                placeholder="Kullanıcı Adı"
                required
              />
            </div>
            <div className="form-register-group">
              <label htmlFor="email">Email</label>
              <input
                type="email"
                id="email"
                value={formData.email}
                onChange={handleChange}
                placeholder="Email Adresi"
                required
              />
            </div>
            <div className="form-register-group">
              <label htmlFor="password">Şifre</label>
              <input
                type="password"
                id="password"
                value={formData.password}
                onChange={handleChange}
                placeholder="Şifre"
                required
              />
            </div>
            <div className="form-register-group">
              <label htmlFor="rePassword">Şifre Tekrar</label>
              <input
                type="password"
                id="rePassword"
                value={formData.rePassword}
                onChange={handleChange}
                placeholder="Şifre Tekrar"
                required
              />
            </div>
            <button type="submit" className="submit-register-button">
              Kaydol
            </button>
          </form>
        )}
      </div>

      {/* Sağ taraf: Dönen araba */}
      <div className="car-register-section">
        <Canvas camera={{ position: [40, 10, 20], fov: 50 }}>
          <OrbitControls
            autoRotate
            autoRotateSpeed={1.5}
            enableZoom={false}
            enablePan={false}
            enableRotate={false}
          />
          <ambientLight intensity={1.2} />
          <directionalLight position={[15, 15, 15]} intensity={2} />
          <pointLight position={[15, 15, 15]} intensity={3} />
          <spotLight position={[0, 5, 0]} angle={0.15} intensity={3} />
          <hemisphereLight
            skyColor={0x00aaff}
            groundColor={0x101010}
            intensity={1.5}
          />
          <Car />
        </Canvas>
      </div>
    </div>
  );
}

export default Register;