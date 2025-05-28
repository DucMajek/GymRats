import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import axios from 'axios';

function SignupButtonWithModal({ trainer }) {
    const [showModal, setShowModal] = useState(false);
    const [date, setDate] = useState('');
    const [time, setTime] = useState('');
    const [loading, setLoading] = useState(false);

    // Funkcja do pobrania emaila z cookies (przykład dla JWT w cookie "token")
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

    const handleSignup = async () => {
        const email = getEmailFromToken();
        if (!email) {
            alert('Nie można pobrać adresu email z tokena.');
            return;
        }
        setLoading(true);
        const dateTime = `${date} ${time}:00`;
        const encodedEmail = encodeURIComponent(email);
        const encodedDateTime = encodeURIComponent(dateTime);
        const url = `https://localhost:44380/user/personalTraning/${trainer.idCoach}/${encodedEmail}/${encodedDateTime}`;
        try {
            await axios.post(url);
            alert(`Zapisano na trening z ${trainer.idCoachNavigation?.name} ${trainer.idCoachNavigation?.surname} w dniu ${date} o ${time}`);
            setShowModal(false);
            setDate('');
            setTime('');
        } catch (err) {
            alert('Wystąpił błąd podczas zapisu na trening.');
        } finally {
            setLoading(false);
        }
    };

    const todayDate = new Date().toISOString().split('T')[0];

    const hours = [];
    for (let h = 8; h <= 20; h++) {
        const hourStr = h.toString().padStart(2, '0') + ':00';
        hours.push(hourStr);
    }

    const modal = (
        <div className="modal-overlay">
            <div className="modal-content">
                <h3>Wybierz termin treningu</h3>
                <label>
                    Data:
                    <input type="date" min={todayDate} value={date} onChange={e => setDate(e.target.value)} />
                </label>
                <label>
                    Godzina:
                    <select value={time} onChange={e => setTime(e.target.value)}>
                        <option value="">Wybierz godzinę</option>
                        {hours.map(hour => (
                            <option key={hour} value={hour}>{hour}</option>
                        ))}
                    </select>
                </label>
                <div style={{ marginTop: '16px' }}>
                    <button onClick={handleSignup} disabled={!date || !time || loading}>
                        {loading ? 'Zapisywanie...' : 'Zapisz'}
                    </button>
                    <button onClick={() => setShowModal(false)} style={{ marginLeft: '8px' }} disabled={loading}>
                        Anuluj
                    </button>
                </div>
            </div>
        </div>
    );

    return (
        <>
            <button className="trainer-signup-btn" onClick={() => setShowModal(true)}>
                Zapisz się
            </button>
            {showModal && ReactDOM.createPortal(modal, document.body)}
        </>
    );
}

export default SignupButtonWithModal;