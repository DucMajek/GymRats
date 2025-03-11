import React, { useEffect } from 'react';
import '../assets/styles/HomePage.css'; 
import headerLogo from '../assets/img/header.png';
import join from '../assets/img/join.png';
import GymPassCategory from '../components/GymPass';
import Footer from '../components/Footer';
import Navbar from '../components/Navbar';
import Courses from'../components/TraningCourse';
import AboutUs from '../components/AboutUs'
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
      <Navbar/>
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
            <a href="#">Dołącz teraz <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-clipboard-fill"></i></span>
            <h4>Plany Treningowe</h4>
            <p>
              Osiągnij swoje cele dzięki indywidualnie dopasowanemu planowi treningowemu.
            </p>
            <a href="#">Dołącz teraz <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-user-star-fill"></i></span>
            <h4>Kursy Trenera</h4>
            <p>
              Zdobądź wiedzę i umiejętności potrzebne, by zostać profesjonalnym trenerem.
            </p>
            <a href="#">Dołącz teraz <i className="ri-arrow-right-line"></i></a>
          </div>
          <div className="explore__card">
            <span><i className="ri-restaurant-fill"></i></span>
            <h4>Darmowe Jadłospisy</h4>
            <p>
             Darmowe jadłospisy, które pomogą Ci zdrowo zwiększyć/zredukować wage.
            </p>
            <a href="#">Dołącz teraz <i className="ri-arrow-right-line"></i></a>
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
        <GymPassCategory/>
      </section>
      <section className="section__container price__container">
        <Courses/>
      </section>
      <AboutUs/>
      <Footer/>
    </div>
  );
}

export default HomePage;
