import React from "react";
import { useNavigate } from "react-router-dom";
import "../Components/Css/HomePageNews.css";
import carimage1 from "./Images/car-4419081.jpg";
import carimage2 from "./Images/moto-7182384.jpg"

function HomePageNews() {
  const navigate = useNavigate();
  return (
    <section className="garage-news">
      <div className="garage-news-container">
        <div className="garage-news-header" >
          <h2 className="garage-news-title">Garage News</h2>
          <p className="garage-news-subtitle">
            Araç endüstrisine ait en yeni haberler ve blog yazıları için takip ediniz...
          </p>
        </div>

        <div className="garage-news-content">
          <div className="news-article">
            <div className="news-article-image">
              <img
                src={carimage1}
                alt="Car News"
                style={{
                  maxWidth: "100%", 
                  maxHeight: "100%", 
                  objectFit: "contain", 
                  display: "block", 
                  margin: "0 auto",
                }}
              />
            </div>
            <div className="news-article-text">
              <h3 className="news-article-title">
                Car Model X Breaks Speed Records
              </h3>
              <p className="news-article-description">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras
                tincidunt ligula eu orci euismod, sed tincidunt neque facilisis.
              </p>
              <button className="news-article-button"
              onClick={() => navigate("/blog")}>Devamını Gör</button>
            </div>
          </div>

          <div className="news-article">
            <div className="news-article-image">
              {/* Görsel eklemek için burayı kullanın */}
              <img
                src={carimage2}
                alt="Car Update"
                style={{
                  maxWidth: "100%", 
                  maxHeight: "100%",
                  objectFit: "contain", 
                  display: "block", 
                  margin: "0 auto", 
                }}
              />
            </div>
            <div className="news-article-text">
              <h3 className="news-article-title">
                The Future of Electric Vehicles
              </h3>
              <p className="news-article-description">
                Curabitur ultricies magna ac feugiat maximus. Sed eget metus eu
                turpis dignissim sodales.
              </p>
              <button className="news-article-button"
              onClick={() => navigate("/blog")}>Devamını Gör</button>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

export default HomePageNews;
