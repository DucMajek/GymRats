import React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import HomePage from './pages/HomePage';
import GymPassPage from './pages/GymPassPage';
import DietsPage from './pages/DietsPage';
import TrainingPlansPage from './pages/TrainingPlansPage';
import CoursesPage from './pages/CoursesPage';
import './App.css';
import { DarkModeToggle } from "./components/DarkModeToggle";
import Login from "./components/Login";
import LoginPage from "./pages/LoginPage";
import { AuthProvider } from './components/AuthContext';
import UserDashboard from './pages/UserDashboard';
import UserProfile from './pages/UserProfile';

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="app">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route exact="true" path="/login" element={<LoginPage />} />
            <Route path="/login" element={<Login />} />
            <Route path="/dashboard" element={<UserDashboard />} />
            <Route path="/user-profile" element={<UserProfile />} />
            <Route path="/gym-pass" element={<GymPassPage />} />
            <Route path="/diets" element={<DietsPage />} />
            <Route path="/training-plans" element={<TrainingPlansPage />} />
            <Route path="/courses" element={<CoursesPage />} />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;