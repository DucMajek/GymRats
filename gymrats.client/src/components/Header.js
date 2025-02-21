import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/img/GymRats_Logo.png'
import { DarkModeToggle } from './DarkModeToggle';

function Header() {
  return (
    <header className="dashboard-header">
        <Link to="/" className="logo"><img src={gymRatsLogo} alt="gymRatsLogo" className="gymRatsLogo"/></Link>
        <div className="dashboard-header-right">
          <button className="buy-pass">Kup karnet</button>
      </div>
    </header>
  );
}

export default Header;
