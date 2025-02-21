import React, { useEffect } from 'react';
import '../assets/styles/HomePage.css'; 
import GymRats_logo from '../assets/img/GymRats_Logo.png';
import member from '../assets/img/member.jpg';
import member2 from '../assets/img/member2.jpg';

function HomePageFooter() {
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
      <section className="review">
        <div className="section__container review__container">
          <span><i className="ri-group-fill"></i></span>
          <div className="review__content">
            <h4>O nas</h4>
            <p>
            To, co naprawdę wyróżnia nas, to nasz zespół dwóch ekspertów. Nasi autorzy są doświadczeni, pomocni i szczerze zaangażowani w pomoc innym w osiąganiu ich celów. 
            Poświęcają czas na zrozumienie indywidualnych potrzeb i tworzą spersonalizowane treści, 
            które zapewniają maksymalne rezultaty i pełne bezpieczeństwo.
            </p>
            <div className="review__footer">
            <div class="review__member">
              <img src={member} alt="member" />
              <div class="review__member__details">
                <h4>Michał Majek</h4>
                <p>PJATK Student</p>
              </div>
              <img src={member2} alt="member" />
              <div class="review__member__details">
                <h4>Duc Anh Dinh</h4>
                <p>PJATK Student</p>
              </div>
            </div>
            </div>
          </div>
        </div>
      </section>

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
          <h4>Company</h4>
          <a href="#">Business</a>
          <a href="#">Franchise</a>
          <a href="#">Partnership</a>
          <a href="#">Network</a>
        </div>
        <div className="footer__col">
          <h4>About Us</h4>
          <a href="#">Blogs</a>
          <a href="#">Security</a>
          <a href="#">Careers</a>
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

export default HomePageFooter;
