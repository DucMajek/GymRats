import React, { useState } from 'react';
import { Modal, Form, Alert, Spinner, Button } from 'react-bootstrap';
import axios from 'axios';
import { useAuth } from '../components/AuthContext';
import '../assets/styles/ChangeModalPassword.css';

export default function ChangePasswordModal({ show, onHide }) {
  const { email, token } = useAuth();
  const [oldPwd, setOldPwd]         = useState('');
  const [newPwd, setNewPwd]         = useState('');
  const [confirmPwd, setConfirmPwd] = useState('');
  const [error, setError]           = useState('');
  const [loading, setLoading]       = useState(false);
  const [success, setSuccess]       = useState(false);

  const handleSubmit = async e => {
    e.preventDefault();
    setError('');

    if (newPwd !== confirmPwd) {
      setError('Hasła nie są takie same');
      return;
    }

    setLoading(true);
    try {
      await axios.put(
        `https://localhost:44380/user/changePassword/${email}/${oldPwd}/${newPwd}`,
        null,
        {
          withCredentials: true,
          headers: { Authorization: `Bearer ${token}` },
        }
      );
      setSuccess(true);
      setTimeout(() => {
        setSuccess(false);
        onHide();
      }, 1000);
    } catch (err) {
      // Map the service’s exception text to a friendly message
      const respText = err.response?.data?.toString() || '';
      if (respText.includes('Password not match')) {
        setError('Źle wpisane stare hasło');
      } else if (err.response?.status === 404) {
        setError('Nie znaleziono użytkownika');
      } else {
        setError(respText || err.message);
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      centered
      dialogClassName="change-pass-modal"
    >
      <Form onSubmit={handleSubmit}>
        <Modal.Header closeButton>
          <Modal.Title>Zmiana hasła</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          {error   && <Alert variant="danger">{error}</Alert>}
          {success && <Alert variant="success">Hasło zmienione!</Alert>}

          <Form.Group className="mb-3" controlId="oldPassword">
            <Form.Label>Stare hasło</Form.Label>
            <Form.Control
              type="password"
              value={oldPwd}
              onChange={e => setOldPwd(e.target.value)}
              required
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="newPassword">
            <Form.Label>Nowe hasło</Form.Label>
            <Form.Control
              type="password"
              value={newPwd}
              onChange={e => setNewPwd(e.target.value)}
              required
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="confirmPassword">
            <Form.Label>Potwierdź nowe hasło</Form.Label>
            <Form.Control
              type="password"
              value={confirmPwd}
              onChange={e => setConfirmPwd(e.target.value)}
              required
            />
          </Form.Group>
        </Modal.Body>

        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={onHide}
            disabled={loading}
          >
            Anuluj
          </Button>
          <Button
            variant="primary"
            type="submit"
            disabled={loading}
          >
            {loading
              ? <Spinner as="span" animation="border" size="sm" role="status" aria-hidden="true" />
              : 'Zmień hasło'
            }
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}
