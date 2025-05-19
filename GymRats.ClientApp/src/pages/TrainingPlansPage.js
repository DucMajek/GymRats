import React from 'react';
import TraningPlan from '../components/TraningPlan';
import '../assets/styles/TrainingPlan.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Footer from '../components/Footer';
function TrainingPlanPage() {
  return (
    <div className="main">
      <Header />
      <div className="d-flex" >
        <Sidebar />
        <div className="training-plans-container">
          {/* lewa kolumna */}
          <div className="left-side">
            <h2>Plany treningowe</h2>
            <TraningPlan title="Dla biegacza" id={1} />
            <TraningPlan title="Dla kolarza" id={2} />
            <TraningPlan title="Dla narciarza" id={3} />
            <TraningPlan title="Dla pływaka" id={4} />
          </div>
        </div>
      </div>
    </div>
  );
}

export default TrainingPlanPage;