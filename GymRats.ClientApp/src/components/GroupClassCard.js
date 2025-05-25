import React from 'react';


export default function GroupClassCard({
  type,
  title,
  groupSize,
  coachName,
  signedIn,
  onSignIn
}) {
  return (
    <div className="group-card">
      <p className="group-card-type">{type}</p>
      <h3 className="group-card-title">{title}</h3>

      <div className="group-card-middle">
        {signedIn ? (
          <span className="signin-status">Zapisany ✓</span>
        ) : (
          <button className="sign-in-button" onClick={onSignIn}>
            Zapisz się
          </button>
        )}
      </div>

      <div className="group-card-stats">
        <span className="stats-size">Rozmiar grupy: {groupSize}</span>
        <span className="stats-coach">Trener: {coachName}</span>
      </div>
    </div>
  );
}
