import React, { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';

export default function ExpandableDescription({ description, wordLimit = 60 }) {
  const words = description ? description.split(/\s+/) : [];
  const shouldTruncate = words.length > wordLimit;
  const [showModal, setShowModal] = useState(false);

  if (!shouldTruncate) return <span>{description}</span>;

  return (
    <>
      <span>
        {words.slice(0, wordLimit).join(' ') + '...'}
        <button
          style={{
            background: 'none',
            border: 'none',
            color: 'orange',
            cursor: 'pointer',
            marginLeft: 8,
            fontWeight: 'bold',
            padding: 0
          }}
          onClick={() => setShowModal(true)}
        >
          Zobacz więcej opisu
        </button>
      </span>
      <Modal show={showModal} onHide={() => setShowModal(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title>Pełny opis kursu</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {description}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Zamknij
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
} 