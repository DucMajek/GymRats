import React from 'react';
import { BMICalculator } from '../index.js';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';

const BmiCalculatorPage = () => (
  
  <div className="main">
    
    <Header />
    <div className="d-flex">
      <Sidebar />
      

      <div className="bmi-calculator-container">
        <BMICalculator />
      </div>
      
    </div>
  </div>
);

export default BmiCalculatorPage; 