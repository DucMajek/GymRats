import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/gymRatsLogo.png'
function Header() {
  return (
    <header className="header">
          <Link to="/" className="logo">GYM RATS</Link>
          {/*<div className="img-logo">*/}
          {/*    <img src={gymRatsLogo} alt="gymRatsLogo" />*/}
          {/*</div>*/}
      <div className="header-right">
        <button className="buy-pass">kup karnet</button>
        <div className="profile-icon">Profil</div>
      </div>
    </header>
  );
}

export default Header;
