import React from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';

import '../assets/styles/LinksYoutubePage.css';

function LinksYoutubePage() {
  const navigate = useNavigate();

  const sampleLinks = [
    { id: 1, title: 'Wprowadzenie do kursu', url: 'https://www.youtube.com/watch?v=abc123' },
    { id: 2, title: 'Ćwiczenia praktyczne', url: 'https://www.youtube.com/watch?v=xyz456' },
    { id: 3, title: 'Podsumowanie', url: 'https://www.youtube.com/watch?v=example789' }
  ];

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="links-youtube-page">
        <div className="header-actions">
            <h2 className="mb-0">Linki do YouTube</h2>
            <Button variant="secondary" className="action-button" onClick={() => navigate(-1)}>
            Powrót
            </Button>
        </div>

        <ul className="youtube-links-list">
            {sampleLinks.map(link => (
            <li key={link.id}>
                <a href={link.url} target="_blank" rel="noopener noreferrer">
                {link.title}
                </a>
            </li>
            ))}
        </ul>
        </div>
      </div>
    </div>
  );
}

export default LinksYoutubePage;
