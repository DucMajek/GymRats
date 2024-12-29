import React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import HomePage from './pages/HomePage';
import GymPassPage from './pages/GymPassPage';
import DietsPage from './pages/DietsPage';
import TrainingPlansPage from './pages/TrainingPlansPage';
import CoursesPage from './pages/CoursesPage';
//import './App.css';
import { DarkModeToggle } from "./components/DarkModeToggle";

function App() {
  return (
    <Router>
      <div className="app">
        <HomePage />
        
      </div>
    </Router>
  );
}

export default App;