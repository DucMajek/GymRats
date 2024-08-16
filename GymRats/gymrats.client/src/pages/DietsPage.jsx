import React from 'react';
import DietPlan from '../components/DietPlan';

function DietsPage() {
  return (
    <div>
      <h2>Diety</h2>
      <div className="diet-plans">
        <DietPlan title="Sportowa" />
        <DietPlan title="Standard" />
        <DietPlan title="Wegetariańska" />
      </div>
    </div>
  );
}

export default DietsPage;
