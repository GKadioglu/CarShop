import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom"; 
import "./Css/Category.css";

const Category = () => {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate(); 

  useEffect(() => {
    fetch("http://localhost:5000/api/category/categories")
      .then((response) => {
        if (!response.ok) {
          throw new Error("API hatası!");
        }
        return response.json();
      })
      .then((data) => {
        setCategories(data.categories); 
        setLoading(false);
      })
      .catch((error) => {
        console.error("Veri çekme hatası:", error);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  const handleCategoryClick = (categoryName) => {
    navigate(`/category/${categoryName}`); 
  };

  return (
    <div className="category-container">
      <h1 className="category-title">Type</h1>
      <div className="category-list">
        {categories.map((category) => (
          <div className="category-card" key={category.id}>
            <div className="image-wrapper">
              <img
                src={`http://localhost:5000/${category.imageUrl}`}
                alt={category.name}
                className="category-image"
              />
            </div>
            <button
              onClick={() => handleCategoryClick(category.name)} 
              className="category-name"
            >
              {category.name}
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Category;