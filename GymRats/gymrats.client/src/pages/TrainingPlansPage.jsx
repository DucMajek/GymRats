import React from 'react';
import TraningPlan from '../components/TraningPlan';

function TrainingPlansPage() {
  return (
    <div>
      <h2>Plany treningowe</h2>
          <div className="traning-plans">
              <TraningPlan title="Koszykówka" id={1} />
              <TraningPlan title="Sporty walki" id={2} />
              <TraningPlan title="Piłka nożna" id={3} />
          </div>
    </div>
  );
}

export default TrainingPlansPage;
