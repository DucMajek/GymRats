import React, { useEffect, useState, useRef } from 'react';
import TraningPlan from '../components/TraningPlan';
import '../assets/styles/TrainingPlan.css';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import { Modal, Button } from 'react-bootstrap';

function isAdmin() {
  const token = localStorage.getItem('token');
  if (!token) return false;
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.roleId === "3";
  } catch {
    return false;
  }
}

function chunkArray(array, size) {
  const result = [];
  for (let i = 0; i < array.length; i += size) {
    result.push(array.slice(i, i + size));
  }
  return result;
}

function TrainingPlanPage() {
  const [plans, setPlans] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showPlanModal, setShowPlanModal] = useState(false);
  const [planForm, setPlanForm] = useState({ trainingPlanName: '', file: null });
  const [planLoading, setPlanLoading] = useState(false);
  const [planError, setPlanError] = useState('');
  const planFileInputRef = useRef();
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [planToDelete, setPlanToDelete] = useState(null);

  const fetchPlans = () => {
    fetch('https://localhost:44380/getAlltrainingPlan')
      .then(res => res.json())
      .then(data => {
        setPlans(data);
        setLoading(false);
      })
      .catch(err => {
        setError('Błąd podczas pobierania planów treningowych.');
        setLoading(false);
      });
  };

  useEffect(() => {
    fetchPlans();
  }, []);

  const handlePlanFormChange = e => {
    const { name, value, files } = e.target;
    setPlanForm(f => ({
      ...f,
      [name]: files ? files[0] : value
    }));
  };

  const handlePlanSubmit = async e => {
    e.preventDefault();
    setPlanLoading(true);
    setPlanError('');
    const formData = new FormData();
    formData.append('trainingPlanFile', planForm.file);
    try {
      await fetch(`https://localhost:44380/admin/newTrainigPlan/${planForm.trainingPlanName}`, {
        method: 'POST',
        body: formData
      });
      setShowPlanModal(false);
      setPlanForm({ trainingPlanName: '', file: null });
      if (planFileInputRef.current) planFileInputRef.current.value = '';
      fetchPlans();
      window.location.reload();
    } catch (err) {
      setPlanError('Błąd podczas dodawania planu.');
    } finally {
      setPlanLoading(false);
    }
  };

  const handleDeletePlan = (trainingPlanId) => {
    setPlanToDelete(trainingPlanId);
    setShowDeleteModal(true);
  };

  const confirmDeletePlan = async () => {
    if (!planToDelete) return;
    try {
      await fetch(`https://localhost:44380/admin/deleteTrainingPlan/${planToDelete}/`, {
        method: 'DELETE',
      });
      fetchPlans();
    } catch (err) {
      alert('Błąd podczas usuwania planu.');
    } finally {
      setShowDeleteModal(false);
      setPlanToDelete(null);
    }
  };

  const cancelDeletePlan = () => {
    setShowDeleteModal(false);
    setPlanToDelete(null);
  };

  const columns = chunkArray(plans, 4);

  return (
    <div className="main">
      <Header />
      <div className="d-flex training-plans-flex">
        <Sidebar />
        <div className="training-plans-outer">
          <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', marginBottom: 20}}>
            <h2 className="training-plans-title" style={{marginBottom: 0, marginRight: 24}}>Plany treningowe</h2>
            {isAdmin() && (
              <Button
                className="admin-add-plan-btn"
                variant="primary"
                onClick={() => setShowPlanModal(true)}
              >
                Dodaj plan treningowy
              </Button>
            )}
          </div>
          <Modal show={showPlanModal} onHide={() => setShowPlanModal(false)} centered>
            <Modal.Header closeButton>
              <Modal.Title>Dodaj plan treningowy</Modal.Title>
            </Modal.Header>
            <form onSubmit={handlePlanSubmit}>
              <Modal.Body>
                <div className="mb-3">
                  <label>Nazwa planu:</label>
                  <input type="text" name="trainingPlanName" className="form-control" value={planForm.trainingPlanName} onChange={handlePlanFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Plik PDF:</label>
                  <input type="file" name="file" className="form-control" accept="application/pdf" onChange={handlePlanFormChange} ref={planFileInputRef} required />
                </div>
                {planError && <div className="text-danger">{planError}</div>}
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowPlanModal(false)}>Anuluj</Button>
                <Button variant="primary" type="submit" disabled={planLoading}>{planLoading ? "Dodawanie..." : "Dodaj"}</Button>
              </Modal.Footer>
            </form>
          </Modal>
          <Modal show={showDeleteModal} onHide={cancelDeletePlan} centered>
            <Modal.Header closeButton>
              <Modal.Title>Potwierdzenie usunięcia</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Czy na pewno chcesz usunąć ten plan treningowy?
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={cancelDeletePlan}>Nie</Button>
              <Button variant="danger" onClick={confirmDeletePlan}>Tak</Button>
            </Modal.Footer>
          </Modal>
          <div className="training-plans-cards-wrapper" style={{flex: 1}}>
            {loading && <p className="training-plans-message">Ładowanie...</p>}
            {error && <p className="training-plans-message training-plans-error">{error}</p>}
            {!loading && !error && plans.length === 0 && <p className="training-plans-message">Brak dostępnych planów treningowych.</p>}
            <div className="training-plans-columns" style={{marginTop: 32}}>
              {columns.map((col, colIdx) => (
                <div key={colIdx} className="training-plans-column">
                  {col.map(plan => (
                    <TraningPlan
                      key={plan.idTrainingPlan}
                      title={plan.trainingPlanName}
                      id={plan.idTrainingPlan}
                      onDelete={handleDeletePlan}
                      isAdmin={isAdmin()}
                    />
                  ))}
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default TrainingPlanPage;
