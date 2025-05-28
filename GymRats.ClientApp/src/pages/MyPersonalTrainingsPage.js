import React, { useEffect, useState } from 'react';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import axios from 'axios';
import '../assets/styles/MyPersonalTrainingsPage.css';
function getEmailFromToken() {
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload.email;
    } catch {
        return null;
    }
}

function MyPersonalTrainingsPage() {
    const [trainings, setTrainings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const email = getEmailFromToken();
        if (!email) {
            setError('Nie znaleziono adresu email w tokenie.');
            setLoading(false);
            return;
        }
        const fetchTrainings = async () => {
            try {
                const encodedEmail = encodeURIComponent(email);
                const res = await axios.get(`https://localhost:44380/user/personalTraning/${encodedEmail}`);
                setTrainings(res.data);
            } catch (err) {
                setError('Błąd podczas pobierania treningów.');
            } finally {
                setLoading(false);
            }
        };
        fetchTrainings();
    }, []);

    return (
        <div className="dashboard-container">
            <Header />
            <div className="dashboard-body">
                <aside className="dashboard-sidebar">
                    <Sidebar />
                </aside>
                <main className="dashboard-main">
                    <h2 className="personal-training-title">Treningi Personalne</h2>
                    {loading && <p>Ładowanie...</p>}
                    {error && <p style={{ color: 'red' }}>{error}</p>}
                    {!loading && !error && (
                        <ul className="training-list">
                            {trainings.length === 0 ? (
                                <li>Brak zapisanych treningów.</li>
                            ) : (
                                trainings.map((tr, idx) => {
                                    const dateObj = new Date(tr.reservationDateTime);
                                    const date = dateObj.toLocaleDateString();
                                    const time = dateObj.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
                                    const specialization = tr.idCoachNavigation?.specialization || 'Brak specjalizacji';

                                    return (
                                        <li key={tr.idPersonalTraining || idx}>
                                            <span className="training-specialization">{specialization}</span>
                                            <span > </span>
                                            <span className="training-date">{date} {time}</span>
                                        </li>
                                    );
                                })
                            )}
                        </ul>
                    )}
                </main>
            </div>
        </div>
    );
}

export default MyPersonalTrainingsPage;