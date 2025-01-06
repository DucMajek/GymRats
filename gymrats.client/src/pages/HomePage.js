import React, { useEffect } from 'react';
import '../assets/styles/HomePage.css'; 
import GymRats_logo from '../assets/img/GymRats_Logo.png';
import headerLogo from '../assets/img/header.png';
import member from '../assets/img/member.jpg';
import member2 from '../assets/img/member2.jpg';
import join from '../assets/img/join.png';
import GymPassCategory from '../components/GymPass'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
function HomePage() {
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
      <nav>
      <div className="nav__logo">
          <a href="#"><img src={GymRats_logo} alt="gymRatsLogo_resized" className="img-logo" /></a>
        </div>
        <ul className="nav__links">
          <li className="link"><a href="#">Strona Główna</a></li>
          <li className="link"><a href="#">Karnety</a></li>
          <li className="link"><a href="#">Kursy</a></li>
          <li className="link"><a href="#">O nas</a></li>
          <li className="link">
            <Link to="/login">Konto</Link>
          </li>
        </ul>
      </nav>

      <header className="section__container header__container">
        <div className="header__content">
          <span className="bg__blur"></span>
          <span className="bg__blur header__blur"></span>
          <h4>NAJLEPSZA SIŁOWNIA W MIEŚCIE</h4>
          <h1><span>ZMIEŃ</span> SWOJE ŻYCIE</h1>
          <p>
          Odkryj swój potencjał i rozpocznij swoją podróż ku większej sile, 
          lepszej kondycji i większej pewności siebie. Dołącz do nas już dziś i doświadcz niesamowitej transformacji, 
          na którą zasługuje Twoje ciało!
          </p>
          <button className="btn">Rozpocznij</button>
        </div>
        <div className="header__image">
          <img src={headerLogo} alt="header" />
        </div>
      </header>

      <section className="section__container explore__container">
        <div className="explore__header">
          <h2 className="section__header">ODKRYJ NASZE OFERTY</h2>
          <div className="explore__nav">
          </div>
        </div>
        <div className="explore__grid">
          <div className="explore__card">
            <span><i className="ri-ticket-2-fill"></i></span>
            <h4>Karnety</h4>
            <p>
              Odkryj siłę fizyczną, psychiczną i emocjonalną z naszymi karnetami, które pomogą Ci osiągnąć cel.
            </p>
            <a href="#">Join Now <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-clipboard-fill"></i></span>
            <h4>Plany Treningowe</h4>
            <p>
              Osiągnij swoje cele dzięki indywidualnie dopasowanemu planowi treningowemu.
            </p>
            <a href="#">Join Now <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-user-star-fill"></i></span>
            <h4>Kursy Trenera</h4>
            <p>
              Zdobądź wiedzę i umiejętności potrzebne, by zostać profesjonalnym trenerem.
            </p>
            <a href="#">Join Now <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-restaurant-fill"></i></span>
            <h4>Darmowe Jadłospisy</h4>
            <p>
             Darmowe jadłospisy, które pomogą Ci zdrowo zwiększyć/zredukować wage.
            </p>
            <a href="#">Join Now <i className="ri-arrow-right-line"></i></a>
          </div>
        </div>
      </section>

      <section className="section__container join__container">
        <h2 className="section__header">DLACZEGO MY?</h2>
        <p className="section__subheader">
        Nasza różnorodna społeczność tworzy przyjazną i wspierającą atmosferę, 
        w której możesz nawiązać nowe znajomości i utrzymać motywację.
        </p>
        <div className="join__image">
          <img src={join} alt="Join" />
          <div className="join__grid">
            <div className="join__card">
              <span><i className="ri-user-star-fill"></i></span>
              <div className="join__card__content">
                <h4>Kursy Trenera</h4>
                <p>Odkryj swój potencjał – Kursy Trenerskie z najlepszymi ekspertami.</p>
              </div>
            </div>
            <div className="join__card">
              <span><i className="ri-clipboard-fill"></i></span>
              <div className="join__card__content">
                <h4>Plany Treningowe</h4>
                <p>Osiągnij swoje cele – Plany Treningowe z najlepszymi ekspertami.</p>
              </div>
            </div>
            <div className="join__card">
              <span><i className="ri-restaurant-fill"></i></span>
              <div className="join__card__content">
                <h4>Przykładowe rozpis diet</h4>
                <p>Profesjonalne wsparcie w osiąganiu sukcesów fitnessowych.</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="section__container price__container">
        <h2 className="section__header">Karnety</h2>
        <p className="section__subheader">
          Nasz plan cenowy oferuje różne poziomy członkostwa, które są dopasowane do różnych preferencji i celów fitnessowych.
        </p>
        <GymPassCategory/>
      </section>

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

export default HomePage;
