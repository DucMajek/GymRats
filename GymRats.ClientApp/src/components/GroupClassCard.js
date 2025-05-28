import React, { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

export default function GroupClassCard({
  type,
  title,
  groupSize,
  date,
  coachName,
  signedIn,
  onSignIn,
  onDrop
}) {
  const [showDropModal, setShowDropModal] = useState(false);

  const handleConfirmDrop = () => {
    onDrop();
    setShowDropModal(false);
  };

  return (
    <div className="group-card">
      
      <p className="group-card-type">{type}</p>
      <h3 className="group-card-title">{title}</h3>

      <div className="group-card-middle">
        {signedIn ? (
          <div className="signed-container" >
            <div className="signed-icon">
            <span className="signin-status">Zapisany ✓</span>
            </div>
            <Button
              variant="danger"
              size="sm"
              className="mt-2"
              onClick={() => setShowDropModal(true)}
            >
              Rezygnuj
            </Button>
          </div>
        ) : (
          <button className="sign-in-button" onClick={onSignIn}>
            Zapisz się
          </button>
        )}
      </div>

      <div className="group-card-stats">
        <span className="stats-size">Rozmiar grupy: {groupSize}</span>
        <span className="stats-coach">{coachName}</span>
        <span className="stats-size">{date}</span>
      </div>

      <Modal
        show={showDropModal}
        onHide={() => setShowDropModal(false)}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>Potwierdź rezygnację</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Czy na pewno chcesz zrezygnować z tych zajęć?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowDropModal(false)}>
            Anuluj
          </Button>
          <Button variant="danger" onClick={handleConfirmDrop}>
            Potwierdź
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}