import React from 'react';
import '../assets/styles/GymPass.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
function GymPassPage() {
  return (

    <div className="main">
      <Header />
      <div className="content-wrapper" style={{gap:"0"}}>
        <Sidebar />
        <div className="passContainer">
          <h2>Moje karnety</h2>
          <div className="cardContainer">
            <h3 className="cardHeader">Karnet (typ)</h3>

            <div className="cardDates">
              <p>Start karnetu: 200</p>
              <p>Koniec umowy:</p>
              <p>Cena: </p>
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
    </div>

  );
}

export default GymPassPage;
