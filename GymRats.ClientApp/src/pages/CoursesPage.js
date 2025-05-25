import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Modal, Alert } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../components/AuthContext';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Footer from '../components/Footer';
import '../assets/styles/CoursesPage.css';


const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL || 'https://localhost:44380',
  headers: { 'Content-Type': 'application/json' }
});

function CoursesPage() {
  const { email, token, isLoggedIn } = useAuth();
  const navigate = useNavigate();

  const [showModal, setShowModal] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    if (token) api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    else delete api.defaults.headers.common['Authorization'];
  }, [token]);

  useEffect(() => {
    if (isLoggedIn) fetchUserCourses();
  }, [isLoggedIn]);

  const fetchUserCourses = async () => {
    setLoading(true);
    setError('');
    try {
      const response = await api.post(`/user/courses/${encodeURIComponent(email)}`);
      setCourses(response.data.map(pc => pc.idCourseNavigation));
    } catch (err) {
      if (err.response?.status === 409) setCourses([]);
      else setError('Błąd przy pobieraniu Twoich kursów.');
    } finally {
      setLoading(false);
    }
  };

  const handleOpenModal = () => setShowModal(true);
  const handleCloseModal = () => setShowModal(false);
  const handleNavigate = () => {
    setShowModal(false);
    navigate('/Courses-type');
  };

  if (!isLoggedIn) {
    return (
      <div className="main">
        <Header />
        <div className="d-flex">
          <Sidebar />
          <div className="courses-page p-4">
            <Alert variant="warning">Musisz być zalogowany, aby zobaczyć kursy.</Alert>
          </div>
        </div>
        <Footer />
      </div>
    );
  }

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="courses-page p-4">
          
          <div className="header-actions">
            <h2 className="mb-0">Kursy dla trenera</h2>
            <Button variant="primary" className="action-button" onClick={handleOpenModal}>
              Kup kurs
            </Button>
          </div>

          {error && <Alert variant="danger" className="mt-3">{error}</Alert>}
          {loading ? (
            <div className="mt-3">Ładowanie Twoich kursów...</div>
          ) : (
            <div className="courses-container mt-4 d-flex flex-wrap gap-3">
              {courses.length > 0 ? (
                courses.map(course => (
                  <div key={course.idCourse} className="course-card">
                    <h3>{course.courseName}</h3>
                    <p className="course-label">Opis:</p>
                    <p>{course.description}</p>
                    <p className="course-duration"><strong>Czas trwania:</strong> {course.duration} godz.</p>
                    {course.price && <p className="course-price">Cena: {course.price} zł</p>}
                    <div className="course-benefits">
                      <p><strong>Link do YouTube:</strong></p>
                      <ul>
                        {/* YouTube links can be listed here */}
                      </ul>
                    </div>
                  </div>
                ))
              ) : (
                <p className="mt-3">Nie masz aktywnych kursów. Kup teraz!</p>
              )}
            </div>
          )}

          <Modal show={showModal} onHide={handleCloseModal} centered>
            <Modal.Header closeButton>
              <Modal.Title>Przejdź do kursów</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Kliknij poniżej, aby zobaczyć wszystkie dostępne kursy i kupić wybrany.
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={handleCloseModal}>Zamknij</Button>
              <Button variant="primary" onClick={handleNavigate}>Przejdź do kursów</Button>
            </Modal.Footer>
          </Modal>
        </div>
      </div>

    </div>
  );
}

export default CoursesPage;