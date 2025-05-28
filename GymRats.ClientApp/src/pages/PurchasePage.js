import React, { useState } from 'react';
import axios from 'axios';
import { useAuth } from '../components/AuthContext';
import { Button, Alert } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import '../assets/styles/PurchasePage.css';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';

const API_URL = process.env.REACT_APP_API_URL || 'https://localhost:44380';

const AVAILABLE_PASSES = [
  {
    idTypePass: 1,
    gymPassName: 'Miesięczny',
    price: 120,
    durationPass: 30,
    description: `Dostęp do klubu 24/7
Dostęp do jadłospisów`
  },
  {
    idTypePass: 2,
    gymPassName: 'Półroczny',
    price: 160,
    durationPass: 240,
    description: `Dostęp do klubu 24/7
Woda na trening gratis
Dostęp do jadłospisów`
  },
  {
    idTypePass: 3,
    gymPassName: 'Roczny',
    price: 250,
    durationPass: 365,
    description: `12 miesięcy treningu w cenie 10
Dostęp do klubu 24/7
Woda na trening gratis
Dostęp do jadłospisów`
  }
];

export default function PurchasePage() {
  const { isLoggedIn, email, token } = useAuth();
  const navigate = useNavigate();

  const [buyingId, setBuyingId]   = useState(null);
  const [error, setError]         = useState('');
  const [success, setSuccess]     = useState(false);

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
      // Po 1.5s przekieruj do podglądu
      setTimeout(() => navigate('/gym-pass'), 1500);
    } catch (err) {
      setError(err.response?.data || err.message || 'Coś poszło nie tak przy zakupie.');
    } finally {
      setBuyingId(null);
    }
  };

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="dashboard-main">
          <h2 className="group-classes-title">Zakup karnetu</h2>

          {error && <Alert variant="danger">{error}</Alert>}
          {success && (
            <Alert variant="success">
              Karnet zakupiony! Zaraz przekieruję Cię do podglądu.
            </Alert>
          )}

          {/* Ukryj opcje zakupowe po sukcesie */}
          {!success && (
            <div className="group-classes-container">
              {AVAILABLE_PASSES.map(p => (
                <div className="group-card" key={p.idTypePass}>
                  <div className="group-card-type">{p.gymPassName}</div>
                  <h3 className="group-card-title">{p.price} PLN</h3>

                  <div className="group-card-description">
                    {p.description.split('\n').map((line, i) => (
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
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
