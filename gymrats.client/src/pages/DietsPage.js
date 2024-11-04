import React from 'react';
import DietPlan from '../components/DietPlan';

function DietsPage() {
  return (
    <div>
      <h2>Diety</h2>
      <div className="diet-plans">
              <DietPlan title="Sportowa" id={1} />
              <DietPlan title="Standard" id={2} />
              <DietPlan title="Wegetariańska" id={3} />
      </div>
    </div>
  );
}

export default DietsPage;
