import React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import GymPassPage from './GymPassPage';
import DietsPage from './DietsPage';
import TrainingPlansPage from './TrainingPlansPage';
import CoursesPage from './CoursesPage';
import '../assets/styles/Dashboard.css';
function UserDashboard() {
  return (
    <div className="main">
    <Header />
    <div className="content-wrapper">
      <Sidebar />
      <main className="main-content">
        <Routes>
          <Route path="/gym-pass" element={<GymPassPage />} />
          <Route path="/diets" element={<DietsPage />} />
          <Route path="/training-plans" element={<TrainingPlansPage />} />
          <Route path="/courses" element={<CoursesPage />} />
        </Routes>
      </main>
    </div>
  </div>
    
  );
}

export default UserDashboard;