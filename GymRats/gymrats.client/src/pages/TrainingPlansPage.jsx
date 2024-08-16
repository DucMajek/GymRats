import React from 'react';
import TraningPlan from '../components/TraningPlan';

function TrainingPlansPage() {
  return (
    <div>
      <h2>Plany treningowe</h2>
          <div className="traning-plans">
              <TraningPlan title="Koszykówka" />
              <TraningPlan title="Sporty walki" />
              <TraningPlan title="Piłka nożna" />
          </div>
    </div>
  );
}

export default TrainingPlansPage;
