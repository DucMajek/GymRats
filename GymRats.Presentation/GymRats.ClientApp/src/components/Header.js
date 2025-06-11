// src/components/Header.js
import React from 'react';
import { Link } from 'react-router-dom';
import gymRatsLogo from '../assets/img/GymRats_Logo.png';
import '../assets/styles/Header.css'; // Import your CSS file for styling

function Header() {
  return (
    <header className="dashboard-header">
      <Link to="/" className="logo">
        <img
          src={gymRatsLogo}
          alt="GymRats Logo"
          className="gymRatsLogo"
        />
      </Link>
    </header>
  );
}

export default Header;
