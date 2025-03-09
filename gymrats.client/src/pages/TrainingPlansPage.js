import React from 'react';
import TraningPlan from '../components/TraningPlan';
import '../assets/styles/TrainingPlan.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
function TrainingPlanPage() {
  return (
    <div className="main">
      <Header />
      <div className="content-wrapper" style={{gap:"0"}}>
        <Sidebar />
        <div className="training-plans-container">
          {/* lewa kolumna */}
          <div className="left-side">
            <h2 style={{fontSize: "2.5em"}}>Plany treningowe</h2>
            <TraningPlan title="Koszykówka" id={1} />
            <TraningPlan title="Sporty walki" id={2} />
            <TraningPlan title="Piłka nożna" id={3} />
          </div>

          {/* prawa kolumna */}
          <div className="right-side">
            <h2 style={{fontSize: "2.5em"}}>Indywidualny plan treningowy</h2>
            {/* przycisk */}
            <div className="circle-plus">+</div>
            <p>
              opis
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default TrainingPlanPage;
