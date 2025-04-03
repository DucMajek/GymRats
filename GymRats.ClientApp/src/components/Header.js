import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/img/GymRats_Logo.png'


function Header() {
  return (
    <header className="dashboard-header" style={{
      width: '100%',
      height: '64px',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      padding: '0 20px',
      boxSizing: 'border-box'
    }}>
      <a href='/' className="logo" style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100%',
        width: '100%',
        maxWidth: '1448px'
      }}>
        <img
          src={gymRatsLogo}
          alt="gymRatsLogo"
          style={{
            maxWidth: '90%',
            height: 'auto',
            maxHeight: '50px',
            objectFit: 'contain'
          }}
          className="gymRatsLogo"
        />
      </a>
    </header>
  );
}

export default Header;
