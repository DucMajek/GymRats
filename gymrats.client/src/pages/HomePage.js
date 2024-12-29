import React, { useEffect } from 'react';
import '../assets/HomePage.css'; 
import GymRats_logo from '../assets/GymRats_Logo.png';
import headerLogo from '../assets/header.png';
import member from '../assets/member.jpg';
import class1 from '../assets/class-1.png';
import class2 from '../assets/class-2.png';
import join from '../assets/join.png';
import GymPassCategory from '../components/GymPass'

function IntroductionPage() {
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
        </ul>
      </nav>

      <i className="ri-home-line"></i>

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
            <span><i className="ri-file-paper-line"></i></span>
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

      <section className="section__container class__container">
        <div className="class__image">
          <span className="bg__blur"></span>
          <img src={class1} alt="class" className="class__img-1" />
          <img src={class2} alt="class" className="class__img-2" />
        </div>
        <div className="class__content">
          <h2 className="section__header">THE CLASS YOU WILL GET HERE</h2>
          <p>
            Led by our team of expert and motivational instructors, "The Class You
            Will Get Here" is a high-energy, results-driven session that combines
            a perfect blend of cardio, strength training, and functional
            exercises. Each class is carefully curated to keep you engaged and
            challenged, ensuring you never hit a plateau in your fitness
            endeavors.
          </p>
          <button className="btn">Book A Class</button>
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
                <h4>Personal Trainer</h4>
                <p>Unlock your potential with our expert Personal Trainers.</p>
              </div>
            </div>
            <div className="join__card">
              <span><i className="ri-vidicon-fill"></i></span>
              <div className="join__card__content">
                <h4>Practice Sessions</h4>
                <p>Elevate your fitness with practice sessions.</p>
              </div>
            </div>
            <div className="join__card">
              <span><i className="ri-building-line"></i></span>
              <div className="join__card__content">
                <h4>Good Management</h4>
                <p>Supportive management, for your fitness success.</p>
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
          <span><i className="ri-double-quotes-r"></i></span>
          <div className="review__content">
            <h4>Our Personnel</h4>
            <p>
              What truly sets this gym apart is their expert team of trainers. The
              trainers are knowledgeable, approachable, and genuinely invested in
              helping members achieve their fitness goals. They take the time to
              understand individual needs and create personalized workout plans,
              ensuring maximum results and safety.
            </p>
            <div className="review__rating">
              <span><i className="ri-star-fill"></i></span>
              <span><i className="ri-star-fill"></i></span>
              <span><i className="ri-star-fill"></i></span>
              <span><i className="ri-star-fill"></i></span>
              <span><i className="ri-star-half-fill"></i></span>
            </div>
            <div className="review__footer">
            <div class="review__member">
              <img src={member} alt="member" />
              <div class="review__member__details">
                <h4>Michał Majek</h4>
                <p>PJATK Student</p>
              </div>
              <img src="assets/member.jpg" alt="member" />
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
            Take the first step towards a healthier, stronger you with our
            unbeatable pricing plans. Let's sweat, achieve, and conquer together!
          </p>
          <div className="footer__socials">
            <a href="#"><i className="ri-facebook-fill"></i></a>
            <a href="#"><i className="ri-instagram-line"></i></a>
            <a href="#"><i className="ri-twitter-fill"></i></a>
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
        Copyright © 2023 Web Design Mastery. All rights reserved.
      </div>
    </div>
  );
}

export default IntroductionPage;
