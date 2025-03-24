import React, { useState, useEffect } from 'react';
import axios from 'axios';
import "./Css/Blog.css";

function Blog() {
  const [news, setNews] = useState([]);  
  const [loading, setLoading] = useState(true); 
  const [error, setError] = useState(null); 
  const [currentPage, setCurrentPage] = useState(1); 
  const [articlesPerPage] = useState(3); 
  const [visiblePages, setVisiblePages] = useState([]); 

  useEffect(() => {
    axios.get('http://localhost:5000/api/blog/latest-news') 
      .then(response => {
        setNews(response.data.articles); 
        setLoading(false); // Yükleme işlemi tamamlandı
      })
      .catch(error => {
        setError('Haberler yüklenirken bir hata oluştu');
        setLoading(false);
      }); 
  }, []); 

  const indexOfLastArticle = currentPage * articlesPerPage;
  const indexOfFirstArticle = indexOfLastArticle - articlesPerPage;
  const currentArticles = news.slice(indexOfFirstArticle, indexOfLastArticle);

  const pageCount = Math.ceil(news.length / articlesPerPage); 
  const pageGroup = Math.ceil(currentPage / 10); 
  const startPage = (pageGroup - 1) * 10 + 1; 
  const endPage = Math.min(pageGroup * 10, pageCount); 
  
  useEffect(() => {
    setVisiblePages(Array.from({ length: endPage - startPage + 1 }, (_, index) => startPage + index));
  }, [currentPage, pageGroup, pageCount]);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  if (loading) {
    return <p>Haberler yükleniyor...</p>;
  }

  if (error) {
    return <p>{error}</p>;
  }

   return (
    <div className="blog-container">
      {/* Üst Başlık */}
      <header className="blog-header">
        <h1 className="blog-title">Otomotiv Dünyasından Haberler</h1>
      </header>
      
      <div className="blog-content">
        {/* Sol Panel - Kategoriler */}
        <aside className="blog-sidebar">
          <h2 className="blog-sidebar-title">Kategoriler</h2>
          <ul className="blog-categories">
            <li>Yeni Nesil Araçlar</li>
            <li>Elektrikli Araçlar</li>
            <li>Otonom Sürüş</li>
            <li>Geleceğin Teknolojileri</li>
            <li>Tartışmalar</li>
          </ul>
        </aside>
        
        {/* Ana İçerik - Blog Yazıları */}
        <main className="blog-main">
          {currentArticles.map((article, index) => (
            <article key={index} className="blog-post">
              <h2 className="blog-post-title">{article.title}</h2>
              <p className="blog-post-text">{article.description}</p>
              <button className="blog-read-more">
                <a href={article.url} target="_blank" rel="noopener noreferrer">Devamını Oku</a>
              </button>
            </article>
          ))}
          
          {/* Sayfa Numaralandırması */}
          <div className="pagination">
            {/* Geri gitme butonu */}
            {startPage > 1 && (
              <button onClick={() => setCurrentPage(startPage - 1)}>&laquo; Önceki</button>
            )}

            {visiblePages.map((pageNumber) => (
              <button 
                key={pageNumber}
                onClick={() => paginate(pageNumber)}
                className={currentPage === pageNumber ? 'active' : ''}>
                {pageNumber}
              </button>
            ))}

            {/* İleri gitme butonu */}
            {endPage < pageCount && (
              <button onClick={() => setCurrentPage(endPage + 1)}>Sonraki &raquo;</button>
            )}
          </div>
        </main>
        
        {/* Sağ Panel - Popüler Yazılar */}
        <aside className="blog-sidebar blog-popular">
          <h2 className="blog-sidebar-title">Popüler Yazılar</h2>
          <ul className="blog-popular-list">
            <li>Elektrikli Araçların Artıları</li>
            <li>Otonom Sürüş Geleceği</li>
            <li>Yeni Tesla Modeli</li>
            <li>Hibrit Araçlar Neden Tercih Ediliyor?</li>
          </ul>
        </aside>
      </div>
    </div>
  );
}

export default Blog;