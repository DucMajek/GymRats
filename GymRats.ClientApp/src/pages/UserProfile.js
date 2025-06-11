import React, { useState, useEffect } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../assets/styles/UserProfile.css';
import '../assets/styles/ChangeModalPassword.css';

import { Button } from 'react-bootstrap';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import ChangePasswordModal from '../components/ChangePasswordModal';
import { useAuth } from '../components/AuthContext';

function UserProfile() {
  const { email, token } = useAuth();
  const [personalData, setPersonalData] = useState(null);
  const [isLoading, setIsLoading]       = useState(true);
  const [error, setError]               = useState(null);
  const [showModal, setShowModal]       = useState(false);

  useEffect(() => {
    if (!email) {
      setError('Brak tokena / email');
      setIsLoading(false);
      return;
    }
    (async () => {
      try {
        const resp = await axios.get(
          `https://localhost:44380/user/personal-data/${email}`,
          {
            withCredentials: true,
            headers: { Authorization: `Bearer ${token}` },
          }
        );
        setPersonalData(resp.data);
      } catch (err) {
        setError(err.response?.data || err.message);
      } finally {
        setIsLoading(false);
      }
    })();
  }, [email, token]);

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar className="sidebar" />
        <div className="content">
          <div className="container-md">
            <div className="p-md-4">
              <section className="mb-4">
                <div className="d-flex justify-content-between align-items-center mb-3">
                  <h2 className="fw-bold text-white fs-1">Moje dane</h2>
                  <Button
                    className="zmien-haslo-btn"
                    size="sm"
                    variant="outline-light"
                    onClick={() => setShowModal(true)}
                  >
                    Zmień hasło
                  </Button>
                </div>
                <hr className="text-white-50 short-hr" />

                {isLoading && <p className="text-white">Ładowanie…</p>}
                {error     && <p className="text-danger">{error}</p>}

                {personalData && (
                  <div>
                    <p className="mb-1">
                      Imię i nazwisko: {personalData.name}{' '}
                      {personalData.surname}
                    </p>
                    <p className="mb-1">Email: {email}</p>
                    <p className="mb-1">
                      Numer telefonu: {personalData.phoneNumber}
                    </p>
                    <p className="mb-1">
                      Data urodzenia: {personalData.birthday}
                    </p>
                  </div>
                )}

                <hr className="text-white-50 short-hr" />
              </section>

              <section className="mb-4">
                <h5 className="fw-bold text-white">Adres kontaktowy</h5>
                <hr className="text-white-50 short-hr" />

                {personalData && (
                  <div>
                    <p className="mb-1">
                      Adres: {personalData.address} {personalData.flatNumber}
                    </p>
                    <p className="mb-1">
                      Kod pocztowy: {personalData.zipCode}
                    </p>
                    <p className="mb-1">Miejscowość: {personalData.place}</p>
                  </div>
                )}

                <hr className="text-white-50 short-hr" />
              </section>
            </div>
          </div>
        </div>
      </div>

      <ChangePasswordModal
        show={showModal}
        onHide={() => setShowModal(false)}
      />
    </div>
  );
}
export default UserProfile;