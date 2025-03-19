import React from 'react';
import DownloadButton from './DownloadButton';
import '../assets/styles/DietPlan.css';
function DietPlan({ title, type }) {
  return (
    <div className="diet-plan">
      <h3>{title}</h3>
      <div className="calories" >
        <p>1500 kcal&nbsp; <DownloadButton  useAlternativeApi={false} type={type} calories={'1500'} /></p>
        <p>1800 kcal&nbsp; <DownloadButton useAlternativeApi={false} type={type} calories={'1800'} /></p>
        <p>2000 kcal <DownloadButton useAlternativeApi={false} type={type} calories={'2000'} /></p>
        <p>2500 kcal <DownloadButton useAlternativeApi={false} type={type} calories={'2500'} /></p>
        <p>3000 kcal <DownloadButton useAlternativeApi={false} type={type} calories={'3000'} /></p>
        <p>3500 kcal <DownloadButton useAlternativeApi={false} type={type} calories={'3500'} /></p>
      </div>
    </div>
  );
}

export default DietPlan;
