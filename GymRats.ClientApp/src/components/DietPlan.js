import React from 'react';
import DownloadButton from './DownloadButton';
import '../assets/styles/DietPlan.css';

function DietPlan({ title, type, diets = [], onDelete, isAdmin }) {
  return (
    <div className="diet-plan">
      <h3>{title}</h3>
      <div className="calories">
        {diets.map(diet => (
          <p key={diet.idEbook}>
            {diet.calories} kcal&nbsp;
            <DownloadButton useAlternativeApi={false} type={type} calories={diet.calories.toString()} />
            {isAdmin && (
              <button style={{marginLeft: 8, background: 'red'}} onClick={() => onDelete(diet.idEbook)}>Usuń</button>
            )}
          </p>
        ))}
      </div>
    </div>
  );
}

export default DietPlan;
