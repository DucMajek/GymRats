import React from 'react';
import { Link } from 'react-router-dom';

function Sidebar() {
  return (
    <div className="sidebar">
      <div className="user-name">Imię nazwisko</div>
      <nav className="nav-menu">
        <Link to="/karnety">Karnety</Link>
        <Link to="/diets">Diety</Link>
        <Link to="/training-plans">Plany treningowe</Link>
        <Link to="/courses">Kursy</Link>
      </nav>
    </div>
  );
}

export default Sidebar;
