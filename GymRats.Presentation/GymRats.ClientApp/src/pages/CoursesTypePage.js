import React, { useState, useEffect } from 'react';
import { useAuth } from '../components/AuthContext';
import { useNavigate } from 'react-router-dom';
import { Alert, Button, Modal } from 'react-bootstrap';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import CoursePageCard, { useCourses, useBuyCourse, useAddTrainerCourse } from '../components/CoursePageCard';
import axios from 'axios';

import '../assets/styles/CoursesPage.css';

function CourseTypePage() {
  const { email, token, isLoggedIn } = useAuth();
  const navigate = useNavigate();

  const { courses, loading, error: loadError } = useCourses(token);
  const { handleBuy, buyingId, error: buyError, success: buySuccess } = useBuyCourse(email, navigate);

  // Pagination state
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 3;
  const totalPages = Math.ceil(courses.length / itemsPerPage);
  const indexOfLast = currentPage * itemsPerPage;
  const indexOfFirst = indexOfLast - itemsPerPage;
  const currentCourses = courses.slice(indexOfFirst, indexOfLast);

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

  // Po błędzie kupowania przekieruj po 3 sekundach
  useEffect(() => {
    if (buyError) {
      const timer = setTimeout(() => {
        navigate('/courses');
      }, 1500);
      return () => clearTimeout(timer);
    }
  }, [buyError, navigate]);

  // ADMIN: modal state for adding course
  const [showAddModal, setShowAddModal] = useState(false);
  const [addForm, setAddForm] = useState({ courseName: '', duration: '', description: '', coachId: '' });
  const [coaches, setCoaches] = useState([]);
  const { addCourse, loading: addLoading, error: addError, success: addSuccess } = useAddTrainerCourse(token);

  // Fetch coaches for select
  useEffect(() => {
    if (showAddModal) {
      axios.get('https://localhost:44380/user/trainersList')
        .then(res => setCoaches(res.data))
        .catch(() => setCoaches([]));
    }
  }, [showAddModal]);

  function isAdmin() {
    const token = localStorage.getItem('token');
    if (!token) return false;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.roleId === "3";
    } catch {
      return false;
    }
  }

  const handleAddFormChange = e => {
    const { name, value } = e.target;
    setAddForm(f => ({ ...f, [name]: value }));
  };

  const handleAddSubmit = async e => {
    e.preventDefault();
    await addCourse(addForm);
    if (!addError) {
      setTimeout(() => {
        setShowAddModal(false);
        setAddForm({ courseName: '', duration: '', description: '', coachId: '' });
        window.location.reload();
      }, 800);
    }
  };

  if (!isLoggedIn) {
    return <Alert variant="danger" className="m-4">Musisz być zalogowany, aby przeglądać kursy.</Alert>;
  }

  if (loading) return <div>Ładowanie kursów...</div>;
  if (loadError) return <Alert variant="danger" className="m-4">{loadError}</Alert>;

  return (
    <div className="main">
      <Header />
      <div className="d-flex">
        <Sidebar />
        <div className="courses-page">
          <div className="courses-header">
            <h2>Kursy na trenera</h2>
            {isAdmin() && (
              <Button variant="primary" className="add-course-btn" onClick={() => setShowAddModal(true)}>
                Dodaj kurs
              </Button>
            )}
          </div>
          <Modal show={showAddModal} onHide={() => setShowAddModal(false)} centered>
            <Modal.Header closeButton>
              <Modal.Title>Dodaj nowy kurs trenera</Modal.Title>
            </Modal.Header>
            <form onSubmit={handleAddSubmit}>
              <Modal.Body>
                <div className="mb-3">
                  <label>Nazwa kursu:</label>
                  <input type="text" name="courseName" className="form-control" value={addForm.courseName} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Czas trwania (godziny):</label>
                  <input type="text" name="duration" className="form-control" value={addForm.duration} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Opis:</label>
                  <textarea name="description" className="form-control" value={addForm.description} onChange={handleAddFormChange} required />
                </div>
                <div className="mb-3">
                  <label>Trener:</label>
                  <select name="coachId" className="form-control" value={addForm.coachId} onChange={handleAddFormChange} required>
                    <option value="">Wybierz...</option>
                    {coaches.map(coach => (
                      <option key={coach.idCoach} value={coach.idCoach}>
                        {coach.idCoachNavigation?.name} {coach.idCoachNavigation?.surname}
                      </option>
                    ))}
                  </select>
                </div>
                {addError && <div className="text-danger">{addError}</div>}
                {addSuccess && <div className="text-success">Kurs dodany!</div>}
              </Modal.Body>
              <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowAddModal(false)}>Anuluj</Button>
                <Button variant="primary" type="submit" disabled={addLoading}>{addLoading ? "Dodawanie..." : "Dodaj"}</Button>
              </Modal.Footer>
            </form>
          </Modal>
          {buyError && <Alert variant="danger" className="mb-4">{buyError}</Alert>}
          {buySuccess && <Alert variant="success" className="mb-4">Kurs zakupiony! Zaraz przekieruję Cię do podglądu.</Alert>}
          <div className="courses-container typepage-courses">
            {currentCourses.map(course => (
              <CoursePageCard
                key={course.idCourse}
                course={course}
                handleBuy={handleBuy}
                buyingId={buyingId}
                error={buyError}
                success={buySuccess}
                hideBuyButton={!!buyError}
              />
            ))}
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
          {/* DESKTOP: paginacja fixed na dole */}
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
        </div>
      </div>
    </div>
  );
}
export default CourseTypePage;