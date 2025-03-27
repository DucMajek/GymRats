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
        <div className="footer__col">
          <div className="footer__logo"><img src={GymRats_logo} alt="logo" /></div>
          <p>
            Zrób pierwszy krok w stronę zdrowszej i silniejszej wersji siebie dzięki naszym atrakcyjnym planom cenowym.
            Ćwicz, osiągaj cele i pokonuj wyzwania razem z nami!
          </p>
          <div className="footer__socials">
            <a href="#"><i className="ri-youtube-fill"></i></a>
            <a href="#"><i className="ri-instagram-line"></i></a>
            <a href="#"><i className="ri-tiktok-fill"></i></a>
          </div>
        </div>

        <div className="footer__col">
          <h4>Contact</h4>
          <a href="#">Contact Us</a>
          <a href="#">Privacy Policy</a>
          <a href="#">Terms & Conditions</a>
          <a href="#">BMI Calculator</a>
        </div>
      </footer>
      <div className="footer__bar">
        Copyright © 2025. All rights reserved.
      </div>
    </div>
  );
}

export default Footer;
