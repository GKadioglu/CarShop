import React, { useState } from "react";
import { useNavigate } from "react-router-dom"; 
import "./Css/Login.css"; 
import { useAuth } from "./Shared/AuthContext";
import { Canvas } from "@react-three/fiber";
import { OrbitControls, useGLTF } from "@react-three/drei";
import { EffectComposer, Bloom } from "@react-three/postprocessing";

function Model() {
  const { scene } = useGLTF("/models/drive3.glb");
  return <primitive object={scene} scale={8.5} />;
}

function Login() {
  const { login } = useAuth();
  const [formData, setFormData] = useState({
    userName: "",
    password: "",
    rememberMe: false,
  });
  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [successMessage, setSuccessMessage] = useState(""); 

  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (formData.userName === "" || formData.password === "") {
      setError("Lütfen tüm alanları doldurun");
      return;
    }

    setIsSubmitting(true);
    setError("");

    try {
      const response = await fetch("http://localhost:5000/api/account/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        const data = await response.json();
        setError(data?.message || "Bir hata oluştu. Lütfen tekrar deneyin.");
        setIsSubmitting(false);
      } else {
        const data = await response.json(); 
        console.log("Gelen backend verisi:", data);

        // Backend'den alınan verilerle giriş yap
        login(data.userName, data.role, data.token, data.email); 

        setSuccessMessage("Başarılı Giriş!");
        setTimeout(() => navigate("/"), 2000); 
      }
    } catch (err) {
      setError("Sunucu ile bağlantı hatası");
      setIsSubmitting(false);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <>
      <div className="login-container">
        <div className="login-form-container">
          <h1 className="login-heading">Giriş Yap</h1>
          {error && <div className="login-error-message">{error}</div>}
          {successMessage && (
            <div className="login-success-message">{successMessage}</div>
          )}
          <form className="login-form" onSubmit={handleSubmit}>
            <input
              type="text"
              name="userName"
              placeholder="Kullanıcı Adı"
              value={formData.userName}
              onChange={handleChange}
              className="login-input"
            />
            <input
              type="password"
              name="password"
              placeholder="Şifre"
              value={formData.password}
              onChange={handleChange}
              className="login-input"
            />
            <button type="submit" className="login-btn" disabled={isSubmitting}>
              {isSubmitting ? "Giriş Yapılıyor..." : "Giriş Yap"}
            </button>
          </form>
          <div className="login-additional-options">
            <a href="/forgot-password" className="login-link">
              Şifremi Unuttum
            </a>
            <a href="/register" className="login-link">
              Hesap Oluştur
            </a>
          </div>
        </div>
      </div>
      <div className="model-container">
        <Canvas
          style={{ height: "100%", width: "100%" }}
          camera={{
            position: [50, 15, 20],
            fov: 45,
            near: 0.1,
            far: 100,
          }}
        >
          <OrbitControls
            autoRotate
            autoRotateSpeed={1.5}
            enableZoom={false}
            enablePan={false}
            enableRotate={false}
          />
          <ambientLight intensity={0.5} />
          <directionalLight position={[20, 20, 20]} />
          <hemisphereLight intensity={0.35} />
          <spotLight position={[10, 10, 10]} angle={0.15} penumbra={1} />
          <Model />
          <EffectComposer>
            <Bloom intensity={0.1} />
          </EffectComposer>
        </Canvas>
      </div>
    </>
  );
}

export default Login;
