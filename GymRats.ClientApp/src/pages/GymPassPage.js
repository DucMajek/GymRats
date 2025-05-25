import React from 'react';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import GymPassCard from '../components/GymPassCard';
import '../assets/styles/GymPass.css';

export default function GymPassPage() {
  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="passContainer">
          <h2>Moje karnety</h2>
          <GymPassCard />
        </div>
      </div>
    </div>
  );
}
