import React, { useState } from 'react';
import '../assets/styles/BMICalculator.css';

function BMICalculator() {
  const [weight, setWeight] = useState('');
  const [height, setHeight] = useState('');
  const [bmi, setBmi] = useState(null);
  const [category, setCategory] = useState('');

  const calculateBMI = (e) => {
    e.preventDefault();
    if (!weight || !height) return;
    const h = parseFloat(height) / 100;
    const w = parseFloat(weight);
    if (h > 0 && w > 0) {
      const bmiValue = w / (h * h);
      setBmi(bmiValue.toFixed(1));
      if (bmiValue < 18.5) setCategory('Niedowaga');
      else if (bmiValue < 25) setCategory('Waga prawidłowa');
      else if (bmiValue < 30) setCategory('Nadwaga');
      else setCategory('Otyłość');
    }
  };

  return (
    <div className="bmi-calculator-card">
      <h3>Kalkulator BMI</h3>
      <form onSubmit={calculateBMI} className="bmi-form">
        <div className="bmi-input-group">
          <label htmlFor="weight">Waga (kg):</label>
          <input
            id="weight"
            type="number"
            min="1"
            value={weight}
            onChange={e => setWeight(e.target.value)}
            required
          />
        </div>
        <div className="bmi-input-group">
          <label htmlFor="height">Wzrost (cm):</label>
          <input
            id="height"
            type="number"
            min="1"
            value={height}
            onChange={e => setHeight(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="bmi-btn">Oblicz BMI</button>
      </form>
      {bmi && (
        <div className="bmi-result">
          <p>Twoje BMI: <span>{bmi}</span></p>
          <p>Kategoria: <span>{category}</span></p>
        </div>
      )}
    </div>
  );
}

export default BMICalculator; 