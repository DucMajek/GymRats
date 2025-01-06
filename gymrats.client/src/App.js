import React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import UserDashboard from './pages/UserDashboard';
import GymPassPage from './pages/GymPassPage';
import DietsPage from './pages/DietsPage';
import TrainingPlansPage from './pages/TrainingPlansPage';
import CoursesPage from './pages/CoursesPage';
import './App.css';
import { DarkModeToggle } from "./components/DarkModeToggle";

function App() {
  return (
    <Router>
      <div className="app">
        <DarkModeToggle/>
        <Header />
        <div className="content-wrapper">
          <Sidebar />
          <main className="main-content">
            <Routes>
              <Route path="/" element={<UserDashboard />} />
              <Route path="/gym-pass" element={<GymPassPage />} />
              <Route path="/diets" element={<DietsPage />} />
              <Route path="/training-plans" element={<TrainingPlansPage />} />
              <Route path="/courses" element={<CoursesPage />} />
            </Routes>
          </main>
        </div>
      </div>
    </Router>
  );
}

export default App;