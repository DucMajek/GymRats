import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/gymRatsLogo.svg'
import { DarkModeToggle } from './DarkModeToggle';
function Header() {
  return (
    <header className="header">
        <Link to="/" className="logo">GYM RATS</Link>
        <img src={gymRatsLogo} alt="gymRatsLogo" className="img-logo"/>
      <div className="header-right">
        <DarkModeToggle/>
        <button className="buy-pass">Kup karnet</button>
        <div className="profile-icon"><p>Profil</p></div>
      </div>
    </header>
  );
}

export default Header;
