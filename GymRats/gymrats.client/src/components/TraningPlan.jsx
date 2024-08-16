function TraningPlan({ title }) {
  return (
      <div className="traning-plan">
          <h3>{title}</h3>
          <div className="schedule">
              <p>Plan treningowy <button>Pobierz excel</button></p>
          </div>
      </div>
  );
}

export default TraningPlan;