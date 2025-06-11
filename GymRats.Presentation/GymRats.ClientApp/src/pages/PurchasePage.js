import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuth } from '../components/AuthContext';
import { Button, Alert, Modal } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import '../assets/styles/PurchasePage.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';

const API_URL = process.env.REACT_APP_API_URL || 'https://localhost:44380';

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

function PurchasePage() {
  const { isLoggedIn, email, token } = useAuth();
  const navigate = useNavigate();

  const [passes, setPasses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [fetchError, setFetchError] = useState('');
  const [buyingId, setBuyingId]   = useState(null);
  const [error, setError]         = useState('');
  const [success, setSuccess]     = useState(false);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [addForm, setAddForm] = useState({ gymPassName: '', price: '', durationPass: '', description: '' });
  const [addLoading, setAddLoading] = useState(false);
  const [addError, setAddError] = useState('');
  const [updateForm, setUpdateForm] = useState({ gymPassId: '', newPassPrice: '' });
  const [updateLoading, setUpdateLoading] = useState(false);
  const [updateError, setUpdateError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 3;

  const fetchPasses = async () => {
    setLoading(true);
    setFetchError('');
    try {
      const res = await axios.get(`${API_URL}/categories`);
      setPasses(res.data || []);
    } catch (err) {
      setFetchError(err.response?.data || err.message || 'Błąd podczas pobierania karnetów.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPasses();
  }, []);

  if (!isLoggedIn) {
    return (
      <Alert variant="warning" className="m-4">
        Musisz być zalogowany, aby kupić karnet.
      </Alert>
    );
  }

  const handleBuy = async (passId) => {
    setError('');
    setBuyingId(passId);

    try {
      await axios.post(
        `${API_URL}/buyGymPass/${passId}/${encodeURIComponent(email)}`,
        null,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      setSuccess(true);
 
      setTimeout(() => navigate('/gym-pass'), 1500);
    } catch (err) {
      setError(err.response?.data || err.message || 'Coś poszło nie tak przy zakupie.');
    } finally {
      setBuyingId(null);
    }
  };

  const handleAddFormChange = e => {
    const { name, value } = e.target;
    setAddForm(f => ({ ...f, [name]: value }));
  };

  const handleUpdateFormChange = e => {
    const { name, value } = e.target;
    setUpdateForm(f => ({ ...f, [name]: value }));
  };

  const handleAddSubmit = async e => {
    e.preventDefault();
    setAddLoading(true);
    setAddError('');
    try {
      await axios.post(
        `${API_URL}/admin/NewGymPass/${encodeURIComponent(addForm.gymPassName)}/${addForm.price}/${addForm.durationPass}/${encodeURIComponent(addForm.description)}`
      );
      setShowAddModal(false);
      setAddForm({ gymPassName: '', price: '', durationPass: '', description: '' });
      setTimeout(() => {
        window.location.reload();
      }, 500);
    } catch (err) {
      setAddError(err.response?.data || err.message || 'Błąd podczas dodawania karnetu.');
    } finally {
      setAddLoading(false);
    }
  };

  const handleUpdateSubmit = async e => {
    e.preventDefault();
    setUpdateLoading(true);
    setUpdateError('');
    try {
      await axios.put(
        `${API_URL}/admin/updateGymPassPrice/${updateForm.gymPassId}/${updateForm.newPassPrice}`
      );
      setShowUpdateModal(false);
      setUpdateForm({ gymPassId: '', newPassPrice: '' });
      setTimeout(() => {
        window.location.reload();
      }, 500);
    } catch (err) {
      setUpdateError(err.response?.data || err.message || 'Błąd podczas aktualizacji ceny.');
    } finally {
      setUpdateLoading(false);
    }
  };


  const totalPages = Math.ceil(passes.length / pageSize);
  const paginatedPasses = passes.slice((currentPage - 1) * pageSize, currentPage * pageSize);

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="dashboard-main">
          <h2 className="group-classes-title">Zakup karnetu</h2>

          {isAdmin() && (
            <div style={{ display: 'flex', gap: 16, marginBottom: 24 }}>
              <Button variant="primary" onClick={() => setShowAddModal(true)}>
                Dodaj karnet
              </Button>
              <Button variant="warning" onClick={() => setShowUpdateModal(true)}>
                Zmień cenę karnetu
              </Button>
            </div>
          )}

          <Modal show={showAddModal} onHide={() => setShowAddModal(false)} centered>
            <Modal.Header closeButton>
              <Modal.Title>Dodaj nowy karnet</Modal.Title>
            </Modal.Header>
            <form onSubmit={handleAddSubmit}>
              <Modal.Body>
                <div className="mb-3">
                  <label>Nazwa karnetu:</label>
                  <input type="text" name="gymPassName" className="form-control" value={addForm.gymPassName} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Cena (PLN):</label>
                  <input type="number" name="price" className="form-control" value={addForm.price} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Czas trwania (dni):</label>
                  <input type="number" name="durationPass" className="form-control" value={addForm.durationPass} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Opis:</label>
                  <textarea name="description" className="form-control" value={addForm.description} onChange={handleAddFormChange} required />
                </div>
                {addError && <div className="text-danger">{addError}</div>}
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowAddModal(false)}>Anuluj</Button>
                <Button variant="primary" type="submit" disabled={addLoading}>{addLoading ? "Dodawanie..." : "Dodaj"}</Button>
              </Modal.Footer>
            </form>
          </Modal>

          <Modal show={showUpdateModal} onHide={() => setShowUpdateModal(false)} centered>
            <Modal.Header closeButton>
              <Modal.Title>Zmień cenę karnetu</Modal.Title>
            </Modal.Header>
            <form onSubmit={handleUpdateSubmit}>
              <Modal.Body>
                <div className="mb-3">
                  <label>Wybierz karnet:</label>
                  <select name="gymPassId" className="form-control" value={updateForm.gymPassId} onChange={handleUpdateFormChange} required>
                    <option value="">Wybierz...</option>
                    {passes.map(p => (
                      <option key={p.idTypePass} value={p.idTypePass}>{p.gymPassName} ({p.price} PLN)</option>
                    ))}
                  </select>
                </div>
                <div className="mb-3">
                  <label>Nowa cena (PLN):</label>
                  <input type="number" name="newPassPrice" className="form-control" value={updateForm.newPassPrice} onChange={handleUpdateFormChange} required />
                </div>
                {updateError && <div className="text-danger">{updateError}</div>}
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowUpdateModal(false)}>Anuluj</Button>
                <Button variant="primary" type="submit" disabled={updateLoading}>{updateLoading ? "Aktualizuję..." : "Zmień"}</Button>
              </Modal.Footer>
            </form>
          </Modal>

          {fetchError && <Alert variant="danger">{fetchError}</Alert>}
          {error && <Alert variant="danger">{error}</Alert>}
          {success && (
            <Alert variant="success">
              Karnet zakupiony! Zaraz przekieruję Cię do podglądu.
            </Alert>
          )}

          {/* Ukryj opcje zakupowe po sukcesie */}
          {!success && (
            <div className="group-classes-container">
              {loading ? (
                <div>Ładowanie karnetów...</div>
              ) : (
                passes.length === 0 ? (
                  <div>Brak dostępnych karnetów.</div>
                ) : (
                  paginatedPasses.map(p => (
                    <div className="group-card" key={p.idTypePass}>
                      <div className="group-card-type">{p.gymPassName}</div>
                      <h3 className="group-card-title">{p.price} PLN</h3>

                      <div className="group-card-description">
                        {(p.description || '').split('\n').map((line, i) => (
                          <p key={i}>{line}</p>
                        ))}
                      </div>

                      <div className="group-card-middle">
                        <Button
                          className="sign-in-button"
                          disabled={buyingId === p.idTypePass}
                          onClick={() => handleBuy(p.idTypePass)}
                        >
                          {buyingId === p.idTypePass ? 'Kupowanie…' : 'Kup karnet'}
                        </Button>
                      </div>

                      <div className="group-card-stats">
                        <span>Czas trwania:</span>
                        <span>{p.durationPass} dni</span>
                      </div>
                    </div>
                  ))
                )
              )}
            </div>
          )}

          {/* PAGINACJA */}
          {!success && totalPages > 1 && (
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
        </div>
      </div>
    </div>
  );
}
export default PurchasePage;