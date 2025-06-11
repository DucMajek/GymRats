import React, { useState } from 'react';
import useGroupClasses from '../components/GroupClass';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import GroupClassCard from '../components/GroupClassCard';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import axios from 'axios';

import '../assets/styles/Groupclass.css';

function getRoleFromToken() {
  const token = localStorage.getItem('token');
  if (!token) return null;
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.roleId || null;
  } catch {
    return null;
  }
}

function getEmailFromToken() {
  const token = localStorage.getItem('token');
  if (!token) return null;
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.email || null;
  } catch {
    return null;
  }
}

function GroupClassPage() {
  const { classes, loading, error, signedIn, signIn, drop } = useGroupClasses();

  const [coachFilter, setCoachFilter] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 3;

  // MODAL STATE
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({
    classType: '',
    date: '',
    hour: '',
    duration: '',
    groupSize: ''
  });
  const [adding, setAdding] = useState(false);
  const [addError, setAddError] = useState('');

  const coachNames = Array.from(
    new Set(classes.map(c => c.coachName).filter(n => n))
  );

  const displayed = coachFilter
    ? classes.filter(c => c.coachName === coachFilter)
    : classes;

  const totalPages = Math.ceil(displayed.length / pageSize);
  const paginated = displayed.slice((currentPage - 1) * pageSize, currentPage * pageSize);

  const isCoach = getRoleFromToken() === "2";
  const email = getEmailFromToken();

  const handleFormChange = e => {
    setForm(f => ({ ...f, [e.target.name]: e.target.value }));
  };

  const handleAddClass = async e => {
    e.preventDefault();
    setAdding(true);
    setAddError('');

    // Łączymy datę i godzinę do formatu datetime-local
    const start = `${form.date}T${form.hour.padStart(2, '0')}:00`;

    try {
      await axios.post(
        `https://localhost:44380/user/addNewGroupClass/${encodeURIComponent(email)}/${encodeURIComponent(form.classType)}/${encodeURIComponent(start)}/${encodeURIComponent(form.duration)}/${encodeURIComponent(form.groupSize)}`
      );
      setShowModal(false);
      setForm({ classType: '', date: '', hour: '', duration: '', groupSize: '' });
    } catch (err) {
      setAddError('Błąd podczas dodawania zajęć.');
    } finally {
      setAdding(false);
    }
  };

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
          <div className="group-toolbar">
            <div className="group-filter">
              <label htmlFor="coachFilter">Filtruj przez imie trenera:</label>
              <br />
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
            {isCoach && (
              <>
                <button
                  className="add-group-class-btn"
                  style={{
                    padding: "10px 24px",
                    background: "#ff9800",
                    color: "#fff",
                    border: "none",
                    borderRadius: "6px",
                    fontWeight: "bold",
                    cursor: "pointer"
                  }}
                  onClick={() => setShowModal(true)}
                >
                  Dodaj
                </button>
                <Modal show={showModal} onHide={() => setShowModal(false)} centered size="xl" >
                  <Modal.Header closeButton>
                    <Modal.Title>Dodaj zajęcia grupowe</Modal.Title>
                  </Modal.Header>
                  <form onSubmit={handleAddClass}>
                    <Modal.Body>
                      <div style={{ marginBottom: 12 }}>
                        <label>Typ zajęć:</label>
                        <input
                          type="text"
                          name="classType"
                          value={form.classType}
                          onChange={handleFormChange}
                          required
                          className="form-control"
                        />
                      </div>
                      <div style={{ marginBottom: 12 }}>
                        <label>Data rozpoczęcia:</label>
                        <input
                          type="date"
                          name="date"
                          value={form.date || ''}
                          onChange={handleFormChange}
                          required
                          className="form-control"
                          min={new Date().toISOString().slice(0, 10)}
                        />
                      </div>
                      <div style={{ marginBottom: 12 }}>
                        <label>Godzina rozpoczęcia:</label>
                        <select
                          name="hour"
                          value={form.hour || ''}
                          onChange={handleFormChange}
                          required
                          className="form-control"
                        >
                          <option value="">Wybierz godzinę</option>
                          {[17, 18, 19, 20].map(h => (
                            <option key={h} value={h}>{`${h}:00`}</option>
                          ))}
                        </select>
                      </div>
                      <div style={{ marginBottom: 12 }}>
                        <label>Czas trwania (minuty):</label>
                        <input
                          type="number"
                          name="duration"
                          value={form.duration}
                          onChange={handleFormChange}
                          required
                          min="1"
                          className="form-control"
                        />
                      </div>
                      <div style={{ marginBottom: 12 }}>
                        <label>Rozmiar grupy:</label>
                        <input
                          type="number"
                          name="groupSize"
                          value={form.groupSize}
                          onChange={handleFormChange}
                          required
                          min="1"
                          className="form-control"
                        />
                      </div>
                      {addError && <div style={{ color: 'red' }}>{addError}</div>}
                    </Modal.Body>
                    <Modal.Footer>
                      <Button variant="secondary" onClick={() => setShowModal(false)}>
                        Anuluj
                      </Button>
                      <Button variant="primary" type="submit" disabled={adding}>
                        {adding ? "Dodawanie..." : "Dodaj"}
                      </Button>
                    </Modal.Footer>
                  </form>
                </Modal>
              </>
            )}
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
export default GroupClassPage;