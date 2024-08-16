import React from 'react';

function DietPlan({ title }) {
  return (
    <div className="diet-plan">
      <h3>{title}</h3>
      <div className="calories">
        <p>1500kcal <button>Pobierz pdf</button></p>
        <p>2000kcal <button>Pobierz pdf</button></p>
        <p>2500kcal <button>Pobierz pdf</button></p>
      </div>
    </div>
  );
}

export default DietPlan;
