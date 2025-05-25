import React from 'react';
import { useAuth } from '../components/AuthContext';
import { useNavigate } from 'react-router-dom';
import { Alert } from 'react-bootstrap';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import CoursePageCard, { useCourses, useBuyCourse } from '../components/CoursePageCard';

import '../assets/styles/CoursesPage.css';

export default function CourseTypePage() {
  const { email, token, isLoggedIn } = useAuth();
  const navigate = useNavigate();

  const { courses, loading, error: loadError } = useCourses(token);
  const { handleBuy, buyingId, error: buyError, success: buySuccess } = useBuyCourse(email, navigate);

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
          <h2>Kursy na trenera</h2>
          {buyError && <Alert variant="danger" className="mb-4">{buyError}</Alert>}
          {buySuccess && <Alert variant="success" className="mb-4">Kurs zakupiony! Zaraz przekieruję Cię do podglądu.</Alert>}
          <div className="courses-container">
            {courses.map(course => (
              <CoursePageCard
                key={course.idCourse}
                course={course}
                handleBuy={handleBuy}
                buyingId={buyingId}
                error={buyError}
                success={buySuccess}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
