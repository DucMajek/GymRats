import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuth } from '../components/AuthContext';
import { Button, Modal } from 'react-bootstrap';
import '../assets/styles/GymPass.css';

const API_URL = process.env.REACT_APP_API_URL || 'https://localhost:44380';

export default function GymPassCard() {
  const { isLoggedIn, email, token } = useAuth();
  const [passData, setPassData] = useState(null);
  const [loading, setLoading]   = useState(true);
  const [error, setError]       = useState('');
  const [noPass, setNoPass]     = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [showCancelModal, setShowCancelModal] = useState(false);
  const [cancelLoading, setCancelLoading] = useState(false);
  const [cancelError, setCancelError] = useState('');

  useEffect(() => {
    if (!isLoggedIn) {
      setError('Musisz być zalogowany, aby zobaczyć karnet.');
      setLoading(false);
      return;
    }

    const fetchPass = async () => {
      try {
        const resp = await axios.get(
          `${API_URL}/user/membership/${encodeURIComponent(email)}`,
          { headers: { Authorization: `Bearer ${token}` } }
        );
        setPassData(resp.data);
      } catch (err) {
        if (err.response?.status === 404) {
          setNoPass(true);
        } else {
          setError(
            err.response?.data ||
            err.message ||
            'Błąd podczas pobierania karnetu.'
          );
        }
      } finally {
        setLoading(false);
      }
    };

    fetchPass();
  }, [isLoggedIn, email, token]);

  const handleCancel = async () => {
    setCancelLoading(true);
    setCancelError('');
    try {
      // Wywołanie endpointu z zalogowanym emailem
      await axios.delete(
        `${API_URL}/user/passCancellation/${encodeURIComponent(email)}`,
        { headers: { Authorization: `Bearer ${token}` } }
      );
      setNoPass(true);
      setShowCancelModal(false);
    } catch (err) {
      setCancelError(
        err.response?.data ||
        err.message ||
        'Błąd podczas rezygnacji z karnetu.'
      );
    } finally {
      setCancelLoading(false);
    }
  };

  if (loading) return <div>Ładowanie karnetu…</div>;
  if (error)   return <div className="error">Błąd: {error}</div>;

  if (noPass) {
    return (
      <>
        <Button variant="primary" onClick={() => setShowModal(true)} className='buyPassButton'>
          Kup karnet
        </Button>
        <Modal show={showModal} onHide={() => setShowModal(false)} centered>
          <Modal.Header closeButton>
            <Modal.Title>Brak karnetu</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            Czy chcesz zakupić u nas karnet? Posiadamy różne rodzaje karnetów, kliknij Kup karnet, aby się przekonać.
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => setShowModal(false)}>
              Anuluj
            </Button>
            <Button variant="primary" onClick={() => { window.location.href = '/purchase'; }}>
              Kup karnet
            </Button>
          </Modal.Footer>
        </Modal>
      </>
    );
  }

  const {
    dateStart,
    dateEnd,
    idStatusNavigation: { statusType },
    idTypePassNavigation: { gymPassName, price, description }
  } = passData;

  const benefits = description
    .split(/\r?\n/)
    .map(line => line.trim())
    .filter(Boolean);

  return (
    <div className="cardContainer">
      <h3 className="cardHeader">Karnet ({gymPassName})</h3>

      <div className="cardDates">
        <p>Start: {new Date(dateStart).toLocaleDateString()}</p>
        <p>Koniec: {new Date(dateEnd).toLocaleDateString()}</p>
        <p>Status: {' '}
          <span className={
            statusType.toLowerCase() === 'active'
              ? 'statusActive'
              : 'statusInactive'
          }>
            {statusType}
          </span>
        </p>
        <p>Cena: {price} PLN</p>
      </div>

      <div className="benefitsSection">
        <span>Benefity karty</span>
        <hr className="divider" />
      </div>

      <ul className="benefitsList">
        {benefits.map((b, i) => (
          <li key={i}>{b}</li>
        ))}
      </ul>

      <Button
        variant="danger"
        className="cancelPassButton"
        onClick={() => setShowCancelModal(true)}
      >
        Rezygnuj z karnetu
      </Button>

      <Modal show={showCancelModal} onHide={() => setShowCancelModal(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title>Potwierdź rezygnację</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Czy na pewno chcesz zrezygnować z karnetu?
          {cancelError && <p className="error">Błąd: {cancelError}</p>}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowCancelModal(false)}>
            Anuluj
          </Button>
          <Button
            variant="danger"
            onClick={handleCancel}
            disabled={cancelLoading}
          >
            {cancelLoading ? 'Rezygnacja...' : 'Potwierdź'}
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}