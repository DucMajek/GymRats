// File: src/pages/GroupClassPage.js
import React, { useState } from 'react';
import useGroupClasses from '../components/GroupClass';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import GroupClassCard from '../components/GroupClassCard';

import '../assets/styles/Dashboard.css';
import '../assets/styles/Groupclass.css';

export default function GroupClassPage() {
  const { classes, loading, error, signedIn, signIn } = useGroupClasses();

  // 1) Filter state
  const [coachFilter, setCoachFilter] = useState('');

  // 2) Build unique list of coaches
  const coachNames = Array.from(
    new Set(classes.map(c => c.coachName).filter(n => n))
  );

  // 3) Decide which to display
  const displayed = coachFilter
    ? classes.filter(c => c.coachName === coachFilter)
    : classes;

  return (
    <div className="dashboard-container">
      <Header />
      <div className="dashboard-body">
        <aside className="dashboard-sidebar">
          <Sidebar />
        </aside>
        <main className="dashboard-main">
          <h2 className="group-classes-title">Group Classes</h2>

          {/* ─── Filter UI ───────────────────────── */}
          <div className="group-filter">
            <label htmlFor="coachFilter">Filter by coach:</label>
            <select
              id="coachFilter"
              value={coachFilter}
              onChange={e => setCoachFilter(e.target.value)}
            >
              <option value="">All</option>
              {coachNames.map(name => (
                <option key={name} value={name}>
                  {name}
                </option>
              ))}
            </select>
            
          </div>

          {loading && <p className="loading">Loading…</p>}
          {error   && <p className="error">Error: {error.message}</p>}

          <div className="group-classes-container">
            {displayed.map(cls => (
              <GroupClassCard
                key={cls.idGroup}
                type="Class"
                title={cls.classType}
                groupSize={cls.groupSize}
                coachName={cls.coachName}
                signedIn={signedIn.has(cls.idGroup)}
                onSignIn={() => signIn(cls.idGroup)}
              />
            ))}
          </div>
        </main>
      </div>
    </div>
  );
}
