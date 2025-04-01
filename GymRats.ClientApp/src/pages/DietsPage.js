import React from 'react';
import DietPlan from '../components/DietPlan';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import '../assets/styles/DietPlan.css';
import Footer from '../components/Footer';
function DietsPage() {
  return (
    <div className="main">
      <Header />
      <div className="content-wrapper" style={{gap:"0"}}>
        <Sidebar />
        <div className="dietContainer">
          <h2>Diety</h2>
          <div className="diet-plans">
            <DietPlan title="Sportowa" type={"Sportowa"} />
            <DietPlan title="Standard" type={"Standard"} />
            <DietPlan title="Wegetariańska" type={"Wegetarianska"} />
          </div>
          
        </div>
      </div>
    </div>
  );
}

export default DietsPage;
