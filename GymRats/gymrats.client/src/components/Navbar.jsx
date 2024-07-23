import React from 'react';
import '../assets/navbarStyles.css'
import gymRatsLogo from '../assets/gymRatsLogo.png'
import profileIcon from '../assets/profileIcon.png'
import background from '../assets/background.png'
function Navbar() {
  return (
      <nav className="nav">
          <a href="/" className="site-title">GYM RATS</a>
          <div className="img-logo">
                <img src={gymRatsLogo} alt="gymRatsLogo" />
          </div>
          <div className="nav-right">
                <a href="/subscriptionOffers" className="subscriptionText">Kup Karnet</a>
                <a href="/profile" className="profile">{/*<img src={profileIcon} alt="profileIcon" />*/}Profil</a>
          </div>
      </nav>
  );
}

export default Navbar;