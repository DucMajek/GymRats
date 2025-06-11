import React, { useEffect, useState, useRef } from 'react';
import DietPlan from '../components/DietPlan';
import '../assets/styles/Dashboard.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import '../assets/styles/DietPlan.css';
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

function DietsPage() {
  const [diets, setDiets] = useState({});
  const [showDietModal, setShowDietModal] = useState(false);
  const [dietForm, setDietForm] = useState({ calories: '', dietType: '', file: null });
  const [dietLoading, setDietLoading] = useState(false);
  const [dietError, setDietError] = useState('');
  const fileInputRef = useRef();
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [dietToDelete, setDietToDelete] = useState(null);

  const fetchDiets = () => {
    fetch('https://localhost:44380/getAllDiet')
      .then(res => res.json())
      .then(data => {
        const grouped = {};
        data.forEach(diet => {
          const type = diet.dietType;
          if (!grouped[type]) grouped[type] = [];
          grouped[type].push(diet);
        });
        setDiets(grouped);
      })
      .catch(err => console.error(err));
  };

  useEffect(() => {
    fetchDiets();
  }, []);

  const handleDietFormChange = e => {
    const { name, value, files } = e.target;
    setDietForm(f => ({
      ...f,
      [name]: files ? files[0] : value
    }));
  };

  const handleDietSubmit = async e => {
    e.preventDefault();
    setDietLoading(true);
    setDietError('');
    const formData = new FormData();
    formData.append('EbookFile', dietForm.file);
    try {
      await fetch(`https://localhost:44380/admin/newFoodBook/${dietForm.calories}/${dietForm.dietType}`, {
        method: 'POST',
        body: formData
      });
      setShowDietModal(false);
      setDietForm({ calories: '', dietType: '', file: null });
      if (fileInputRef.current) fileInputRef.current.value = '';
      fetchDiets();
      window.location.reload();
    } catch (err) {
      setDietError('Błąd podczas dodawania diety.');
    } finally {
      setDietLoading(false);
    }
  };

  const handleDeleteDiet = async (idEbook) => {
    setDietToDelete(idEbook);
    setShowDeleteModal(true);
  };

  const confirmDeleteDiet = async () => {
    if (!dietToDelete) return;
    try {
      await fetch(`https://localhost:44380/admin/deleteFoodEbook/${dietToDelete}/`, {
        method: 'DELETE',
      });
      fetchDiets();
    } catch (err) {
      alert('Błąd podczas usuwania diety.');
    } finally {
      setShowDeleteModal(false);
      setDietToDelete(null);
    }
  };

  const cancelDeleteDiet = () => {
    setShowDeleteModal(false);
    setDietToDelete(null);
  };

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="dietContainer">
          <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', marginBottom: 20}}>
            <h2 style={{marginBottom: 0, marginRight: 24}}>Diety</h2>
            {isAdmin() && (
              <Button
                className="admin-add-diet-btn"
                variant="primary"
                onClick={() => setShowDietModal(true)}
              >
                Dodaj dietę
              </Button>
            )}
          </div>
          <Modal show={showDietModal} onHide={() => setShowDietModal(false)} centered>
            <Modal.Header closeButton>
              <Modal.Title>Dodaj dietę</Modal.Title>
            </Modal.Header>
            <form onSubmit={handleDietSubmit}>
              <Modal.Body>
                <div className="mb-3">
                  <label>Kalorie:</label>
                  <input type="number" name="calories" className="form-control" value={dietForm.calories} onChange={handleDietFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Typ diety:</label>
                  <select name="dietType" className="form-control" value={dietForm.dietType} onChange={handleDietFormChange} required>
                    <option value="">Wybierz typ diety</option>
                    <option value="Standard">Standard</option>
                    <option value="Sportowa">Sportowa</option>
                    <option value="Vegetarian">Vegetarian</option>
                  </select>
                </div>
                <div className="mb-3">
                  <label>Plik PDF:</label>
                  <input type="file" name="file" className="form-control" accept="application/pdf" onChange={handleDietFormChange} ref={fileInputRef} required />
                </div>
                {dietError && <div className="text-danger">{dietError}</div>}
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowDietModal(false)}>Anuluj</Button>
                <Button variant="primary" type="submit" disabled={dietLoading}>{dietLoading ? "Dodawanie..." : "Dodaj"}</Button>
              </Modal.Footer>
            </form>
          </Modal>
          <Modal show={showDeleteModal} onHide={cancelDeleteDiet} centered>
            <Modal.Header closeButton>
              <Modal.Title>Potwierdzenie usunięcia</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Czy na pewno chcesz usunąć tę dietę?
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={cancelDeleteDiet}>Nie</Button>
              <Button variant="danger" onClick={confirmDeleteDiet}>Tak</Button>
            </Modal.Footer>
          </Modal>
          <div className="diet-plans" style={{marginTop: 32}}>
            {Object.entries(diets).map(([type, dietArr]) => (
              <DietPlan
                key={type}
                title={type}
                type={type}
                diets={dietArr}
                onDelete={handleDeleteDiet}
                isAdmin={isAdmin()}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default DietsPage;