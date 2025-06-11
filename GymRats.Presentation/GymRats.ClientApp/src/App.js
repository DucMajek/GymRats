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

import Login from "./components/Login";
import LoginPage from "./pages/LoginPage";
import { AuthProvider } from './components/AuthContext';
import UserDashboard from './pages/UserDashboard';
import UserProfile from './pages/UserProfile';
import GroupClassPage from './pages/GroupClassPage';
import PurchasePage from './pages/PurchasePage';
import 'bootstrap/dist/css/bootstrap.min.css';
import CoursesTypePage from "./pages/CoursesTypePage";
import PersonalTrainingPage from "./pages/PersonalTrainingPage";
import MyPersonalTrainingsPage from "./pages/MyPersonalTrainingsPage";
import PrivateRoute from "./components/PrivateRoute";

import BmiCalculatorPage from './pages/BmiCalculatorPage';

function App() {
  return (
    <AuthProvider>
      <Router>
        <div className="app">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/Courses-type" element={<CoursesTypePage />} />
            <Route path="/Groupclass" element={<GroupClassPage />} />
            <Route exact="true" path="/login" element={<LoginPage />} />
            <Route path="/login" element={<Login />} />
            <Route path="/dashboard" element={
              <PrivateRoute>
                <UserDashboard />
              </PrivateRoute>
            } />
            <Route path="/user-profile" element={<UserProfile />} />
            <Route path="/gym-pass" element={<GymPassPage />} />
            <Route path="/purchase" element={<PurchasePage />} />
            <Route path="/diets" element={<DietsPage />} />
            <Route path="/training-plans" element={<TrainingPlansPage />} />
            <Route path="/courses" element={<CoursesPage />} />
            <Route path="/personal-training/signup" element={<PersonalTrainingPage />} />
            <Route path="/personal-training/my-training" element={<MyPersonalTrainingsPage />} />
            <Route path="/BmiCalculator" element={<BmiCalculatorPage />} />
          </Routes>
        </div>
      </Router>
    </AuthProvider>
  );
}

export default App;