import React from 'react';
import '../assets/navbarStyles.css'
import gymRatsLogo from '../assets/gymRatsLogo.png'
function Navbar() {
  return (
      <nav className="nav">
          <a href="/" className="site-title">GYM RATS</a>
          <div className="img-logo">
                <img src={gymRatsLogo} alt="gymRatsLogo" />
          </div>
          <div className="nav-right">
                <a href="/subscriptionOffers" className="subscriptionText">Kup Karnet</a>
                <a href="/profile" className="profile">Profil</a>
          </div>
      </nav>
  );
}

export default Navbar;