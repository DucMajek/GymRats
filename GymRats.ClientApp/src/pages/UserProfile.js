import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import '../assets/styles/UserProfile.css'
import React from 'react';
import Button from '../components/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import axios from 'axios';
function UserProfile() {
  const [personalData, setPersonalData] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [email, setEmail] = useState(null)

  useEffect(() => {
    const getUserPersonalData = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('Brak tokena');
        setIsLoading(false);
        return;
      }

      try {
        const decodedPayload = JSON.parse(atob(token.split('.')[1]));

        setEmail(decodedPayload.email);

        const response = await axios.get(
          `https://localhost:44380/personal-data/${decodedPayload.email}`,
          { withCredentials: true }
        );
        setPersonalData(response.data);
      } catch (err) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    };

    getUserPersonalData();
  }, []);

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="flex-grow-1 p-4 mt-3 ms-3">
          <div className="container-md">
            <div className="p-md-4">
              <section className="mb-4">
                <div className="d-flex justify-content-between align-items-center mb-3">
                  <h2 className="fw-bold text-white fs-1">Moje dane</h2>
                  <div className="d-flex gap-2 ms-5">
                    <Button
                      size="sm"
                      text="zmień hasło"
                      variant="outline-light"
                      className="pe-2"
                    />
                    <Button
                      size="sm"
                      text="edytuj"
                      variant="primary"
                    />
                  </div>
                </div>
                <hr className="text-white-50" />
                {personalData && (
                  <div>
                    <p className="mb-1">{personalData.imie} {personalData.surname}</p>
                    <p className="mb-1">{email}</p>
                    <p className="mb-1">{personalData.phoneNumber}</p>
                    <p className="mb-1">{personalData.birthday}</p>
                  </div>
                )}
                <hr className="text-white-50" />
              </section>
              <section className="mb-4">
                <div className="d-flex justify-content-between align-items-center mb-3">
                  <h5 className="fw-bold text-white">Adres kontaktowy</h5>
                  <Button
                    size="sm"
                    text="edytuj"
                    variant="primary"
                  />
                </div>
                <hr className="text-white-50" />
                {personalData && (
                  <div>
                    <p className="mb-1">{personalData.imie} {personalData.surname}</p>
                    <p className="mb-1">{personalData.adres} {personalData.flatNumber}</p>
                    <p className="mb-1">{personalData.zipCode}</p>
                    <p className="mb-1">{personalData.place}</p>
                  </div>
                )}
                
                <hr className="text-white-50" />
              </section>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default UserProfile;