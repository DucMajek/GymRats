import React from "react";
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Footer from '../components/Footer';
import '../assets/styles/Dashboard.css';
import '../assets/styles/PersonalData.css';
function UserDashboard() {
  return (
    <div className="main">
      <Header />
      <div className="content-wrapper">
        <Sidebar />
        <main className="main-content">
          <div className='blog' >
            <h2>Blog</h2>
          </div>
        </main>
      </div>
    </div>
  );
}

export default UserDashboard;