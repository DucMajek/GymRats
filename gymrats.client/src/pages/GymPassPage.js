import React from 'react';
import '../assets/styles/GymPassPage.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
function GymPassPage() {
  return (

    <div className="main">
      <Header />
      <div className="content-wrapper">
        <Sidebar />
        {/*<h2>Posiadane Karnety</h2>*/}
        {/* Główna karta */}
        <div className="cardContainer">
          <h3 className="cardHeader">Karnet na miesiąc</h3>

          <div className="cardDates">
            <p>Data karnetu : 200</p>
            <p>Data końca karnetu</p>
          </div>

          <div className="benefitsSection">
            <span>benefity karty</span>
            <hr className="divider" />
          </div>

          <div className="benefitsPlaceholder"></div>

          <div className="bottomSection">
            <div className="paymentForm">
              <p><strong>Forma płatności :</strong> karta kredytowa</p>
              <button className="changeButton">ZMIEŃ</button>
            </div>
            <div className="promoCodes">
              <p><strong>Kody promocyjne</strong></p>
              <button className="addButton">DODAJ</button>
            </div>
          </div>
        </div>
      </div>
    </div>

  );
}

export default GymPassPage;
