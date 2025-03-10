import React from 'react'; 
import Footer from './Footer';
import Navbar from './Navbar';
import bodyImage from '../Images/warehouse-2696005_1920.jpg';

function Layout({ children }) {
  return (
    <div>
      <Navbar />
      <div style={{ backgroundImage: `url(${bodyImage})`, backgroundSize: 'cover',backgroundPosition: '70% 20%' , minHeight: '100vh' }}>
        {children} {}
      </div>
      <Footer />
    </div>
  );
}

export default Layout;