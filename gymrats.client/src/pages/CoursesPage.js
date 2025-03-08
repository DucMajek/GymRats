import React from 'react';
import { Link } from 'react-router-dom';
import '../assets/styles/CoursesPage.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
function CoursesPage() {
  return (
    <div className="main">
      <Header />
      <div className="content-wrapper">
        <Sidebar />
        <div className="courses-page">
          <h2>Kursy na trenera</h2>

          <div className="courses-container">

            {/* Karta kursu 1 */}
            <div className="course-card">
              <h3>TRENER PERSONALNY</h3>
              <p className="course-label">Opis:</p>
              <p>
                Oferujemy 120 godzin zajęć praktycznych i teoretycznych z najlepszymi ekspertami w dziedzinie fitness,
                którzy dzielą się z Tobą swoją wiedzą i doświadczeniem.
                Dzięki nim poznasz jak wygląda zawód trenera personalnego, nauczysz się programować jednostkę treningową,
                dobierzesz metody i ćwiczenia, którymi bezpiecznie poprowadzisz klienta podczas współpracy.
              </p>
              <p className="course-price">Cena: 2500 złotych za cały kurs</p>
              <button className="course-button">Wybieram</button>

              <div className="course-benefits">
                <p><strong>Benefity:</strong></p>
                <ul>
                  <li>Certyfikat międzynarodowy w języku polskim i angielskim</li>
                  <li>Materiały szkoleniowe PDF</li>
                </ul>
              </div>


              <Link to="/course-details/1" className="learn-more">
                dowiedz się więcej
              </Link>
            </div>

            {/* Karta kursu 2 */}
            <div className="course-card">
              <h3>TRENER PRZYGOTOWANIA MOTORYCZNEGO</h3>
              <p className="course-label">Opis:</p>
              <p>
              Oferujemy 90 godzin akademickich zajęć praktycznych i teoretycznych, obejmujących ćwiczenia, wykłady oraz hospitacje. 
              Pod okiem doświadczonych specjalistów zdobędziesz wiedzę i umiejętności niezbędne do pracy w zawodzie. 
              W ostatnim dniu kursu uczestnicy przystępują do egzaminu przed komisją CKKS, który potwierdza ich kwalifikacje.
              </p>
              <p className="course-price">Cena: 1500 złotych za cały kurs</p>
              <button className="course-button">Wybieram</button>

              <div className="course-benefits">
                <p><strong>Benefity:</strong></p>
                <ul>
                  <li>Certyfikat międzynarodowy w języku polskim i angielskim</li>
                  <li>Materiały szkoleniowe PDF</li>
                </ul>
              </div>

              <Link to="/course-details/2" className="learn-more">
                dowiedz się więcej
              </Link>
            </div>

            {/* Karta kursu 3 */}
            <div className="course-card">
              <h3>TRENER FITNESS</h3>
              <p className="course-label">Opis:</p>
              <p>
                Zapewniamy intensywny kurs trenera fitness, trwający 90 godzin,
                obejmujący zarówno praktykę, jak i teorię. Nauczysz się od doświadczonych
                instruktorów, zdobędziesz niezbędną wiedzę i umiejętności.
                Kurs kończy się egzaminem przed komisją CKKS.
                Gotowy do rozpoczęcia kariery jako profesjonalny trener fitness? Dołącz do nas!
              </p>
              <p className="course-price">Cena: 2500 złotych za cały kurs</p>
              <button className="course-button">Wybieram</button>

              <div className="course-benefits">
                <p><strong>Benefity:</strong></p>
                <ul>
                  <li>Certyfikat międzynarodowy w języku polskim i angielskim</li>
                  <li>Materiały szkoleniowe PDF</li>
                </ul>
              </div>

              <Link to="/course-details/3" className="learn-more">
                dowiedz się więcej
              </Link>
            </div>

          </div>
        </div>
      </div>
    </div>
  );
}

export default CoursesPage;
