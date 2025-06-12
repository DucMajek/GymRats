import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Modal, Alert, Pagination } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../components/AuthContext';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import Footer from '../components/Footer';
import ExpandableDescription from '../components/ExpandableDescription';
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

  const [showYoutubeModal, setShowYoutubeModal] = useState(false);

const handleShowYoutubeModal = () => {
  setShowYoutubeModal(true);
};

const handleCloseYoutubeModal = () => {
  setShowYoutubeModal(false);
};

const handleGoToYoutubeLinks = () => {
  setShowYoutubeModal(false);
  navigate('/linksYoutube');
};


  // Pagination state
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 3;

  // Show pagination on mobile only at bottom
  const [showPaginationMobile, setShowPaginationMobile] = useState(false);
  useEffect(() => {
    const handleScroll = () => {
      const isMobile = window.innerWidth <= 768;
      if (!isMobile) {
        setShowPaginationMobile(false);
        return;
      }
      const scrollY = window.scrollY || window.pageYOffset;
      const windowHeight = window.innerHeight;
      const docHeight = document.documentElement.scrollHeight;
      // If user is at the bottom (with 10px tolerance)
      if (windowHeight + scrollY >= docHeight - 10) {
        setShowPaginationMobile(true);
      } else {
        setShowPaginationMobile(false);
      }
    };
    window.addEventListener('scroll', handleScroll);
    window.addEventListener('resize', handleScroll);
    handleScroll();
    return () => {
      window.removeEventListener('scroll', handleScroll);
      window.removeEventListener('resize', handleScroll);
    };
  }, []);

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
      setCurrentPage(1); // reset to first page
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

  const totalPages = Math.ceil(courses.length / itemsPerPage);
  const indexOfLast = currentPage * itemsPerPage;
  const indexOfFirst = indexOfLast - itemsPerPage;
  const currentCourses = courses.slice(indexOfFirst, indexOfLast);

  const handlePageChange = (page) => {
    setCurrentPage(page);
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
            <>
              <div className="courses-container mt-4 d-flex flex-wrap gap-3">
                {currentCourses.length > 0 ? (
                  currentCourses.map(course => (
                    <div key={course.idCourse} className="course-card">
                      <h3>{course.courseName}</h3>
                      <p className="course-label">Opis:</p>
                      <p><ExpandableDescription description={course.description} /></p>
                      <p className="course-duration"><strong>Czas trwania:</strong> {course.duration} godz.</p>
                      {course.price && <p className="course-price">Cena: {course.price} zł</p>}
                      <div className="course-benefits centered-benefits">
                        <p><strong>Link do YouTube:</strong></p>
                        <ul>
                          <Button variant="outline-primary" onClick={() => handleShowYoutubeModal()}>
                            Zobacz linki do YouTube
                          </Button>
                        </ul>
                      </div>
                    </div>
                  ))
                ) : (
                  <p className="mt-3">Nie masz aktywnych kursów. Kup teraz!</p>
                )}
              </div>
              {/* MOBILE: paginacja pod kartami */}
              {window.innerWidth <= 768 && totalPages > 1 && showPaginationMobile && (
                <div className="pagination-mobile">
                  <button
                    onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                    disabled={currentPage === 1}
                  >
                    Poprzednia
                  </button>
                  <span style={{ margin: '0 12px' }}>
                    Strona {currentPage} z {totalPages}
                  </span>
                  <button
                    onClick={() => setCurrentPage(p => Math.min(totalPages, p + 1))}
                    disabled={currentPage === totalPages}
                  >
                    Następna
                  </button>
                </div>
              )}
              
              
              {window.innerWidth > 768 && totalPages > 1 && (
                <div className="pagination">
                  <button
                    onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                    disabled={currentPage === 1}
                  >
                    Poprzednia
                  </button>
                  <span style={{ margin: '0 12px' }}>
                    Strona {currentPage} z {totalPages}
                  </span>
                  <button
                    onClick={() => setCurrentPage(p => Math.min(totalPages, p + 1))}
                    disabled={currentPage === totalPages}
                  >
                    Następna
                  </button>
                </div>
              )}
            </>
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
          <Modal show={showYoutubeModal} onHide={handleCloseYoutubeModal} centered>
            <Modal.Header closeButton>
              <Modal.Title>Linki do YouTube</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Kliknij przycisk poniżej, aby przejść do listy linków związanych z kursem na YouTube.
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={handleCloseYoutubeModal}>
                Zamknij
              </Button>
              <Button variant="primary" onClick={handleGoToYoutubeLinks}>
                Przejdź do linków
              </Button>
            </Modal.Footer>
          </Modal>

        </div>
      </div>

    </div>
  );
}

export default CoursesPage;
