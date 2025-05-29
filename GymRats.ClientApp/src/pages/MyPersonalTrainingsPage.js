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

function getRoleFromToken() {
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload.roleId || null;
    } catch {
        return null;
    }
}

const isCoach = getRoleFromToken() === "2";

function MyPersonalTrainingsPage() {
    const [trainings, setTrainings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [isCoach, setIsCoach] = useState(false);

    useEffect(() => {
        setIsCoach(getRoleFromToken() === "2");
    }, []);
    // PAGINATION STATE
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 3;

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
                setError('Brak treningów.');
            } finally {
                setLoading(false);
            }
        };
        fetchTrainings();
    }, []);

    // PAGINATION LOGIC
    const sortedTrainings = [...trainings].sort(
        (a, b) => new Date(b.reservationDateTime) - new Date(a.reservationDateTime)
    );
    const totalPages = Math.ceil(sortedTrainings.length / itemsPerPage);
    const paginated = sortedTrainings.slice(
        (currentPage - 1) * itemsPerPage,
        currentPage * itemsPerPage
    );

    return (
        <div className="dashboard-container">
            <Header />
            <div className="dashboard-body">
                <aside className="dashboard-sidebar">
                    <Sidebar />
                </aside>
                <main className="dashboard-main">
                    <h2 className="personal-training-title">
                        {isCoach ? "Moje treningi personalne (trener)" : "Moje treningi personalne"}
                    </h2>
                    {loading && <p>Ładowanie...</p>}
                    {error && <p style={{ color: 'red' }}>{error}</p>}
                    {!loading && !error && (
                        <>
                            <ul className="training-list">
                                {paginated.length === 0 ? (
                                    <li>Brak zapisanych treningów.</li>
                                ) : (
                                    paginated.map((tr, idx) => {
                                        const dateObj = new Date(tr.reservationDateTime);
                                        const date = dateObj.toLocaleDateString();
                                        const time = dateObj.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

                                        // Dane trenera
                                        const coachPerson = tr.idCoachNavigation?.idCoachNavigation;
                                        const coachName = coachPerson ? `${coachPerson.name} ${coachPerson.surname}` : "Brak danych";
                                        const specialization = tr.idCoachNavigation?.specialization || "Brak specjalizacji";

                                        // Dane użytkownika
                                        const userPerson = tr.idUserNavigation?.idUserNavigation;
                                        const userName = userPerson ? `${userPerson.name} ${userPerson.surname}` : "Brak danych";
                                        const userEmail = tr.idUserNavigation?.email || "Brak email";

                                        return (
                                            <li key={tr.idPersonalTraining || idx}>
                                                <div className="training-specialization"><b>Trening:</b> {specialization}</div>
                                                <div className="training-date"><b>Data:</b> {date} <b>Godzina:</b> {time}</div>
                                                <div className="training-info">
                                                    <span><span className="training-label">Trener:</span> {coachName}</span>
                                                    <span><span className="training-label">Użytkownik:</span> {userName} ({userEmail})</span>
                                                </div>
                                            </li>
                                        );
                                    })
                                )}
                            </ul>
                            {totalPages > 1 && (
                                <div className="pagination" style={{ marginTop: 24, textAlign: 'center' }}>
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
                </main>
            </div>
        </div>
    );
}

export default MyPersonalTrainingsPage;