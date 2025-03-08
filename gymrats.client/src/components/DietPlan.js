import React from 'react';
import DownloadButton from './DownloadButton';
import '../assets/styles/DietPlan.css';
function DietPlan({ title, id }) {
  return (
    <div className="diet-plan">
      <h3>{title}</h3>
      <div className="calories">
              <p>1500 kcal <DownloadButton useAlternativeApi={false} fileId={id} calories={'1500'} /></p>
              <p>1800 kcal <DownloadButton useAlternativeApi={false} fileId={id} calories={'1800'} /></p>
              <p>2100 kcal <DownloadButton useAlternativeApi={false} fileId={id} calories={'2100'} /></p>
      </div>
    </div>
  );
}

export default DietPlan;
