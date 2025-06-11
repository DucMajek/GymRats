import React, { useEffect } from 'react';
import '../assets/styles/Footer.css';
import GymRats_logo from '../assets/img/GymRats_Logo.png';

function Footer() {
  useEffect(() => {
    const link = document.createElement('link');
    link.href = 'https://cdn.jsdelivr.net/npm/remixicon@3.4.0/fonts/remixicon.css';
    link.rel = 'stylesheet';
    document.head.appendChild(link);

    return () => {
      document.head.removeChild(link);
    };
  }, []);

  return (
    <div>
      <footer className="section__container footer__container">
        <span className="bg__blur"></span>
        <span className="bg__blur footer__blur"></span>
      </footer>
      <div className="footer__bar">
        Copyright © 2025. All rights reserved.
      </div>
    </div>
  );
}

export default Footer;
