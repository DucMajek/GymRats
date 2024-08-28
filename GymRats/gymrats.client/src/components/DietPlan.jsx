import React from 'react';
import DowloadDietButton from './DowloadDietButton';
function DietPlan({ title, id }) {
  return (
    <div className="diet-plan">
      <h3>{title}</h3>
      <div className="calories">
              <p>1500kcal <DowloadDietButton fileId={id} calories={'1500'} /></p>
              <p>1800kcal <DowloadDietButton fileId={id} calories={'1800'} /></p>
              <p>2100kcal <DowloadDietButton fileId={id} calories={'2100'} /></p>
      </div>
    </div>
  );
}

export default DietPlan;
