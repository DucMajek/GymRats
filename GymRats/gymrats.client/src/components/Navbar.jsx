import React from 'react';
import '../assets/navbarStyles.css'
import gymRatsLogo from '../assets/gymRatsLogo.png'
import profileIcon from '../assets/profileIcon.png'
function Navbar() {
  return (
      <nav className="nav">
          <a href="/" className="site-title">GYM RATS</a>
          <ul>
              <li>
                  <div className="img-logo">
                      <img src={gymRatsLogo} alt="gymRatsLogo" />
                  </div>
              </li>
              <li>
                  <a href="/subscriptionOffers" className="">Kup Karnet</a>
              </li>
              <li>
                  <div className="img-profile">
                        
                      <a href="/profile" className="profile"><img src={profileIcon} alt="profileIcon" />Profil</a>
                  </div>
              </li>
          </ul>
      </nav>
  );
}

export default Navbar;