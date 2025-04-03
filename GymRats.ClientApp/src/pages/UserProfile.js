import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import '../assets/styles/UserProfile.css'
import React from 'react';
import Button from '../components/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import {useState} from 'react';
function UserProfile() {
  const [changePasswordButton, setChangePasswordButton] = useState(false)
  const [editDataButton, setEditDataButton] = useState(false)
  
  const PersonalData = () => (
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
      <div>
        <p className="mb-1">Michał Majek</p>
        <p className="mb-1">michamajek@gmail.com</p>
        <p className="mb-1">+48886723988</p>
        <p className="mb-1">Dokument: 01320710239</p>
      </div>
      <hr className="text-white-50" />
    </section>
  );

  const ContactAddress = () => (
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
      <div>
        <p className="mb-1">Michał Majek</p>
        <p className="mb-1">Jana Kazimierza 31A m71</p>
        <p className="mb-1">01-248 Warszawa</p>
      </div>
      <hr className="text-white-50" />
    </section>
  );


  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="flex-grow-1 p-4 mt-3 ms-3">
          <div className="container-md">
            <div className="p-md-4">
              <PersonalData />
              <ContactAddress />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default UserProfile;