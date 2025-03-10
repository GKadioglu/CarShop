import React, { useState, useEffect } from "react";
import { fetchWithToken } from "./Shared/api";
import "./Css/AdminCarEdit.css";

function AdminCarEdit() {
  const [cars, setCars] = useState([]); // Tüm araçlar
  const [selectedCar, setSelectedCar] = useState(null); // Seçilen araç
  const [categories, setCategories] = useState([]); // Kategoriler
  const [models, setModels] = useState([]); // Modeller
  const [newCar, setNewCar] = useState({
    brand: "",
    model: "",
    year: "",
    price: "",
    categoryId: "", // Tekil kategori ID
    modelId: "", // Tekil model ID
  });

  // API'den araçları çekme
  useEffect(() => {
    async function fetchData() {
      try {
        const carsResponse = await fetchWithToken(
          "http://localhost:5000/api/admin/allcars"
        );
        const categoriesResponse = await fetchWithToken(
          "http://localhost:5000/api/admin/getcategories"
        );
        const modelsResponse = await fetchWithToken(
          "http://localhost:5000/api/admin/getmodels"
        );

        if (carsResponse.ok) {
          const carsData = await carsResponse.json();
          setCars(carsData.cars || []);
          setSelectedCar(carsData.cars?.[0] || null);
        }

        if (categoriesResponse.ok) {
          const categoriesData = await categoriesResponse.json();
          setCategories(categoriesData.categories || []);
        }

        if (modelsResponse.ok) {
          const modelsData = await modelsResponse.json();
          setModels(modelsData || []);
        }
      } catch (error) {
        console.error("Veriler yüklenirken hata oluştu:", error);
      }
    }

    fetchData();
  }, []);

  // Yeni araç bilgilerini güncelleme
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewCar((prevCar) => ({ ...prevCar, [name]: value }));
  };

  const handleCategoryChange = (e) => {
    const categoryId = e.target.value; // Doğrudan string alınır
    setNewCar((prevCar) => ({
      ...prevCar,
      categoryId: categoryId ? parseInt(categoryId, 10) : null, // Eğer değer yoksa null
    }));
  };

  const handleModelChange = (e) => {
    const modelId = e.target.value; // Doğrudan string alınır
    setNewCar((prevCar) => ({
      ...prevCar,
      modelId: modelId ? parseInt(modelId, 10) : null, // Eğer değer yoksa null
    }));
  };

  const handleAddCar = async () => {
    console.log("Yeni araç ekleniyor:", newCar); // Gelen veri loglanıyor

    // Zorunlu alanlar kontrolü
    if (
      !newCar.brand ||
      !newCar.model ||
      !newCar.year ||
      !newCar.price ||
      !newCar.categoryId ||
      !newCar.modelId
    ) {
      alert("Tüm alanları doldurduğunuzdan emin olun.");
      return;
    }

    // Girilen model ile seçilen modelin eşleşmesini kontrol et
    const selectedModel = models.find((brand) => brand.id === newCar.modelId);
    if (
      newCar.brand &&
      selectedModel &&
      newCar.brand.trim().toLowerCase() !==
        selectedModel.name.trim().toLowerCase()
    ) {
      alert("Seçilen Marka ile Girilen Marka aynı olmalıdır.");
      return; // Araç eklemeyi engelle
    }

    // JSON verisi oluşturun
    const carData = {
      brand: newCar.brand,
      model: newCar.model,
      price: newCar.price,
      year: newCar.year,
      categoryId: newCar.categoryId,
      modelId: newCar.modelId,
    };

    try {
      const response = await fetchWithToken(
        "http://localhost:5000/api/admin/addnewcar",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(carData), // JSON verisini gönderiyoruz
        }
      );

      if (!response.ok) {
        throw new Error("Araç ekleme işlemi başarısız oldu");
      }

      const addedCar = await response.json();
      setCars((prevCars) => [...prevCars, addedCar]);
      alert("Yeni araç başarıyla eklendi.");
    } catch (error) {
      console.error("Araç eklenirken hata oluştu:", error);
      alert("Yeni araç eklenemedi.");
    }
  };

  const handleDeleteCar = async (carId) => {
    try {
      // API çağrısı ile aracı sil
      const response = await fetchWithToken(
        `http://localhost:5000/api/admin/deletecar?id=${carId}`,
        {
          method: "DELETE",
        }
      );

      if (!response.ok) {
        throw new Error("Silme işlemi başarısız");
      }

      // Silinen aracı listeden kaldır
      setCars((prevCars) => prevCars.filter((car) => car.carId !== carId));

      // Eğer seçilen araç silindiyse, seçilen aracı null yap
      if (selectedCar?.carId === carId) {
        setSelectedCar(null);
      }

      alert("Araç başarıyla silindi.");
    } catch (error) {
      console.error("Araç silinirken hata oluştu:", error);
      alert("Araç silinemedi.");
    }
  };

  return (
    <div className="admin-car-edit-container">
      {/* Üst Yapı: Araç Listesi ve Detay Kartı */}
      <div className="admin-car-edit-upper">
        {/* Araç Listesi */}
        <div className="admin-car-edit-menu">
          <h1>KATALOGDAKİ ARAÇLAR</h1>
          {cars.map((car) => (
            <div
              key={car.carId}
              className={`admin-car-edit-menu-item ${
                selectedCar?.carId === car.carId ? "selected" : ""
              }`}
              onClick={() => setSelectedCar(car)}
            >
              {car.brand} {car.model} ({car.year})
            </div>
          ))}
        </div>

        {/* Araç Detay Kartı */}
        {selectedCar && (
          <div className="admin-car-edit-card">
            <div className="admin-card-image-wrapper">
              <img
                src={`http://localhost:5000/${selectedCar.imageUrl}`}
                className="admin-car-edit-image"
                alt="Araç görseli"
              />
              <div className="admin-car-edit-info">
                <h2 className="admin-card-edit-title">
                  {selectedCar.brand} {selectedCar.model}
                </h2>
                <p className="admin-price-edit-text">
                  Fiyat: <span className="price">{selectedCar.price} $</span>
                </p>
                {/* Silme Butonu */}
                <button
                  className="delete-car-btn"
                  onClick={() => handleDeleteCar(selectedCar.carId)}
                >
                  Sil
                </button>
              </div>
            </div>
          </div>
        )}
      </div>

      <h2 className="admin-new-car-title">Yeni Araç Ekle</h2>
      <div className="admin-new-car">
        <div className="admin-new-car-form-group">
          <label>Marka:</label>
          <input
            type="text"
            name="brand"
            value={newCar.brand}
            onChange={handleInputChange}
          />
        </div>
        <div className="admin-new-car-form-group">
          <label>Model:</label>
          <input
            type="text"
            name="model"
            value={newCar.model}
            onChange={handleInputChange}
          />
        </div>
        <div className="admin-new-car-form-group">
          <label>Yıl:</label>
          <input
            type="number"
            name="year"
            value={newCar.year}
            onChange={handleInputChange}
          />
        </div>
        <div className="admin-new-car-form-group">
          <label>Fiyat:</label>
          <input
            type="number"
            name="price"
            value={newCar.price}
            onChange={handleInputChange}
          />
        </div>
        <div className="admin-new-car-form-group">
          <label>Kategoriler:</label>
          <select
            value={newCar.categoryId || ""}
            onChange={handleCategoryChange}
          >
            <option value="" disabled>
              Seçin
            </option>
            {categories.map((category) => (
              <option key={category.categoryId} value={category.categoryId}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
        <div className="admin-new-car-form-group">
          <label>Markalar:</label>
          <select
            value={newCar.modelId || ""} // Seçilen model ID
            onChange={handleModelChange} // Model değiştiğinde state'i günceller
          >
            <option value="" disabled>
              Seçin
            </option>
            {models.map((model) => (
              <option key={model.modelId} value={model.modelId}>
                {model.name}
              </option>
            ))}
          </select>
        </div>

        <button className="add-new-car-button" onClick={handleAddCar}>
          Yeni Araç Ekle
        </button>
      </div>
    </div>
  );
}

export default AdminCarEdit;
