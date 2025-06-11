import React, { useState } from 'react';
import '../assets/styles/AuthForm.css';

const today = new Date().toISOString().split('T')[0];

function AuthForm({
  action,
  name,
  setName,
  surname,
  setSurname,
  email,
  setEmail,
  password,
  setPassword,
  confirmPassword,
  setConfirmPassword,
  birthday,
  setBirthday,
  phoneNumber,
  setPhoneNumber,
  gender,
  setGender,
  address,
  setAddress,
  flatNumber,
  setFlatNumber,
  zipCode,
  setZipCode,
  place,
  setPlace,
  onSubmit
}) {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();

    setError('');
    setSuccess('');

    if (action === "Stwórz konto" && password !== confirmPassword) {
      setError("Hasła się nie zgadzają.");
      return;
    }

    // Wywołaj funkcję z rodzica
    const result = onSubmit(e);

    if (result !== false) {
      setSuccess("Konto zostało pomyślnie utworzone!");

      // Wyczyść wszystkie pola
      setName('');
      setSurname('');
      setEmail('');
      setPassword('');
      setConfirmPassword('');
      setBirthday('');
      setPhoneNumber('');
      setGender('');
      setAddress('');
      setFlatNumber('');
      setZipCode('');
      setPlace('');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      {error && <div className="alert alert-danger">{error}</div>}
      {success && <div className="alert alert-success">{success}</div>}
      {action === "Stwórz konto" && (
        <>
          <div className="row">
            <div className="col-md-6 mb-3">
              <label htmlFor="exampleInputName" className="form-label text-white">Imię</label>
              <input
                type="text"
                value={name}
                onChange={e => setName(e.target.value)}
                className="form-control"
                id="exampleInputName"
                required
              />
            </div>
            <div className="col-md-6 mb-3">
              <label htmlFor="exampleInputSurname" className="form-label text-white">Nazwisko</label>
              <input
                type="text"
                value={surname}
                onChange={e => setSurname(e.target.value)}
                className="form-control"
                id="exampleInputSurname"
                required
              />
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-3">
              <label htmlFor="inputBirthday" className="form-label text-white">Data urodzenia</label>
              <input
                type="date"
                value={birthday}
                onChange={e => setBirthday(e.target.value)}
                className="form-control"
                id="inputBirthday"
                required
                max={today}
              />
            </div>
            <div className="col-md-6 mb-3">
              <label htmlFor="inputPhone" className="form-label text-white">Telefon</label>
              <input
                type="tel"
                value={phoneNumber}
                onChange={e => setPhoneNumber(e.target.value)}
                className="form-control"
                id="inputPhone"
                required
              />
            </div>
          </div>

          <div className="mb-3">
            <label htmlFor="selectGender" className="form-label text-white">Płeć</label>
            <select
              value={gender}
              onChange={e => setGender(e.target.value)}
              className="form-select"
              id="selectGender"
              required
            >
              <option value="">Wybierz...</option>
              <option value="M">Mężczyzna</option>
              <option value="F">Kobieta</option>
            </select>
          </div>

          <div className="row">
            <div className="col-md-8 mb-3">
              <label htmlFor="inputAddress" className="form-label text-white">Adres</label>
              <input
                type="text"
                value={address}
                onChange={e => setAddress(e.target.value)}
                className="form-control"
                id="inputAddress"
                required
              />
            </div>
            <div className="col-md-4 mb-3">
              <label htmlFor="inputFlat" className="form-label text-white">Numer mieszkania</label>
              <input
                type="text"
                value={flatNumber}
                onChange={e => setFlatNumber(e.target.value)}
                className="form-control"
                id="inputFlat"
              />
            </div>
          </div>

          <div className="row">
            <div className="col-md-4 mb-3">
              <label htmlFor="inputZip" className="form-label text-white">Kod pocztowy</label>
              <input
                type="text"
                value={zipCode}
                onChange={e => setZipCode(e.target.value)}
                className="form-control"
                id="inputZip"
                required
              />
            </div>
            <div className="col-md-8 mb-3">
              <label htmlFor="inputPlace" className="form-label text-white">Miejscowość</label>
              <input
                type="text"
                value={place}
                onChange={e => setPlace(e.target.value)}
                className="form-control"
                id="inputPlace"
                required
              />
            </div>
          </div>
        </>
      )}

      <div className="mb-3">
        <label htmlFor="exampleInputEmail1" className="form-label text-white">Email</label>
        <input
          type="email"
          value={email}
          onChange={e => setEmail(e.target.value)}
          className="form-control"
          id="exampleInputEmail1"
          required
        />
      </div>

      <div className="mb-3">
        <label htmlFor="exampleInputPassword1" className="form-label text-white">Hasło</label>
        <input
          type="password"
          value={password}
          onChange={e => setPassword(e.target.value)}
          className="form-control"
          id="exampleInputPassword1"
          required
        />
      </div>

      {action === "Stwórz konto" && (
        <div className="mb-3">
          <label htmlFor="exampleInputPassword2" className="form-label text-white">Potwierdź hasło</label>
          <input
            type="password"
            value={confirmPassword}
            onChange={e => setConfirmPassword(e.target.value)}
            className="form-control"
            id="exampleInputPassword2"
            required
          />
        </div>
      )}

      <button type="submit" className="btn btn-primary w-100">
        {action}
      </button>
    </form>
  );
}

export default AuthForm;
