import React, { useState, useEffect } from 'react';
import axios from 'axios';
import "./Css/Blog.css";

function Blog() {
  const [news, setNews] = useState([]);  // Haberler verisini tutacağız
  const [loading, setLoading] = useState(true); // Yükleniyor durumu
  const [error, setError] = useState(null); // Hata durumu
  const [currentPage, setCurrentPage] = useState(1); // Geçerli sayfa
  const [articlesPerPage] = useState(3); // Sayfa başına gösterilecek haber sayısı
  const [visiblePages, setVisiblePages] = useState([]); // Görünür sayfa numaraları

  // Sayfa ilk yüklendiğinde API'den haberleri çekiyoruz
  useEffect(() => {
    axios.get('http://localhost:5000/api/blog/latest-news') // API endpoint'i
      .then(response => {
        setNews(response.data.articles); // API yanıtından haberleri alıyoruz
        setLoading(false); // Yükleme işlemi tamamlandı
      })
      .catch(error => {
        setError('Haberler yüklenirken bir hata oluştu');
        setLoading(false);
      }); 
  }, []); // Boş dizi, sadece ilk renderda çalışmasını sağlar

  // Sayfa başına 3 haber göstermek için slice kullanıyoruz
  const indexOfLastArticle = currentPage * articlesPerPage;
  const indexOfFirstArticle = indexOfLastArticle - articlesPerPage;
  const currentArticles = news.slice(indexOfFirstArticle, indexOfLastArticle);

  // Sayfa numaralarını hesaplamak
  const pageCount = Math.ceil(news.length / articlesPerPage); // Toplam sayfa sayısı
  const pageGroup = Math.ceil(currentPage / 10); // Sayfa grubu
  const startPage = (pageGroup - 1) * 10 + 1; // Görünür sayfa numarasının başlangıcı
  const endPage = Math.min(pageGroup * 10, pageCount); // Görünür sayfa numarasının sonu
  
  // Görünür sayfa numaralarını güncelleme
  useEffect(() => {
    setVisiblePages(Array.from({ length: endPage - startPage + 1 }, (_, index) => startPage + index));
  }, [currentPage, pageGroup, pageCount]);

  // Sayfa numarasına tıklama
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