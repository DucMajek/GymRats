import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/img/GymRats_Logo.png'


function Header() {
  return (
    <header className="dashboard-header" style={{ width:'1448px', height: '64px'}}>
        <Link to="/" className="logo"><img src={gymRatsLogo} alt="gymRatsLogo" className="gymRatsLogo"/></Link>
    </header>
  );
}

export default Header;
