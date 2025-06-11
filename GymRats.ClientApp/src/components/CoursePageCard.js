// src/components/CoursePageCard.js

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Alert } from 'react-bootstrap';
import ExpandableDescription from './ExpandableDescription';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL || 'https://localhost:44380',
  headers: { 'Content-Type': 'application/json' }
});

export function useCourses(token) {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (token) {
      api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      delete api.defaults.headers.common['Authorization'];
    }

    api.get('/Courses')
      .then(res => setCourses(res.data))
      .catch(() => setError('Nie można załadować kursów.'))
      .finally(() => setLoading(false));
  }, [token]);

  return { courses, loading, error };
}

export function useBuyCourse(email, navigate, redirectPath = '/courses') {
  const [buyingId, setBuyingId] = useState(null);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);

  const handleBuy = async (courseId) => {
    setError('');
    setBuyingId(courseId);
    try {
      await api.post(`/user/buyCourse/${courseId}/${encodeURIComponent(email)}`);
      setSuccess(true);
      setTimeout(() => navigate(redirectPath), 1500);
    } catch (err) {
      if (err.response?.status === 409) {
        setError('Już posiadasz ten kurs.');
        setTimeout(() => navigate(redirectPath), 1000);
      } else {
        setError('Nie udało się kupić kursu.');
      }
    } finally {
      setBuyingId(null);
    }
  };

  return { handleBuy, buyingId, error, success };
}

export function useAddTrainerCourse(token) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);

  const addCourse = async ({ courseName, duration, description, coachId }) => {
    setLoading(true);
    setError('');
    setSuccess(false);
    try {
      if (token) {
        api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      } else {
        delete api.defaults.headers.common['Authorization'];
      }

      // ─── SEND JSON BODY RATHER THAN ROUTE PARAMS ───────────────────────────────
      await api.post('admin/newTrainerCourse', {
        courseName,
        duration,
        description,
        coachId: parseInt(coachId, 10)
      });
      // ────────────────────────────────────────────────────────────────────────────

      setSuccess(true);
    } catch (err) {
      setError(err.response?.data || err.message || 'Błąd podczas dodawania kursu.');
    } finally {
      setLoading(false);
    }
  };

  return { addCourse, loading, error, success };
}

function CoursePageCard({ course, handleBuy, buyingId, hideBuyButton }) {
  return (
    <div className="course-card">
      <h3>{course.courseName}</h3>
      <p className="course-label">Opis:</p>
      <p><ExpandableDescription description={course.description} /></p>
      <p className="course-duration"><strong>Czas trwania:</strong> {course.duration} godz.</p>
      {course.price && <p className="course-price">Cena: {course.price} zł</p>}

      {!hideBuyButton && (
        <Button
          className="course-button"
          onClick={() => handleBuy(course.idCourse)}
          disabled={buyingId === course.idCourse}
        >
          {buyingId === course.idCourse ? 'Kupowanie…' : 'Wybieram'}
        </Button>
      )}

      <div className="course-benefits">
        <p><strong>Benefity:</strong></p>
        <ul>
          <li>Materiały szkoleniowe w postaci linku do YouTube</li>
          <li>Możliwość zostania trenerem</li>
          <li>Rozwój w dziedzinie {course.courseName}</li>
        </ul>
      </div>
    </div>
  );
}

export default CoursePageCard;
