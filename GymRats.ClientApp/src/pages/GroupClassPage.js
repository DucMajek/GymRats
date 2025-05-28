import React, { useState } from 'react';
import useGroupClasses from '../components/GroupClass';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import GroupClassCard from '../components/GroupClassCard';

import '../assets/styles/Groupclass.css';

export default function GroupClassPage() {
  const { classes, loading, error, signedIn, signIn, drop } = useGroupClasses();

  const [coachFilter, setCoachFilter] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 4; 

  const coachNames = Array.from(
    new Set(classes.map(c => c.coachName).filter(n => n))
  );

  const displayed = coachFilter
    ? classes.filter(c => c.coachName === coachFilter)
    : classes;

 
  const totalPages = Math.ceil(displayed.length / pageSize);
  const paginated = displayed.slice((currentPage - 1) * pageSize, currentPage * pageSize);

  
  React.useEffect(() => {
    setCurrentPage(1);
  }, [coachFilter]);

  return (
    <div className="dashboard-container">
      <Header />
      <div className="dashboard-body">
        <aside className="dashboard-sidebar">
          <Sidebar />
        </aside>
        <main className="dashboard-main">
          <h2 className="group-classes-title">Zajęcia grupowe</h2>

          {/* ─── Filter UI ───────────────────────── */}
          <div className="group-filter">
            <label htmlFor="coachFilter">Filtruj przez imie trenera:</label>
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
          {error && <p className="error">Error: {error.message}</p>}

          <div className="group-classes-container">
            {paginated.map(cls => (
              <GroupClassCard
                key={cls.idGroup}
                type="Class"
                title={cls.classType}
                date={cls.startDate.replace("T", "\n")}
                groupSize={cls.groupSize}
                coachName={cls.coachName}
                signedIn={signedIn.has(cls.idGroup)}
                onSignIn={() => signIn(cls.idGroup)}
                onDrop={() => drop(cls.idGroup)}
              />
            ))}
          </div>

          {totalPages > 1 && (
            <div className="pagination" style={{ marginTop: 24, textAlign: 'center' }}>
              <button
                onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                disabled={currentPage === 1}
              >
                Poprzednia
              </button>
              <span style={{ margin: '0 12px' }}>
                Strona {currentPage} z {totalPages}
              </span>
              <button
                onClick={() => setCurrentPage(p => Math.min(totalPages, p + 1))}
                disabled={currentPage === totalPages}
              >
                Następna
              </button>
            </div>
          )}
        </main>
      </div>
    </div>
  );
}