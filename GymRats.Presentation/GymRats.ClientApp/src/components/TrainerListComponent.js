import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../assets/styles/TrainerListComponent.css';
import SignupButtonWithModal from './SignupButtonWithModal';

function TrainersListComponent() {
    const [trainers, setTrainers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axios.get('https://localhost:44380/user/trainersList')
            .then(response => {
                setTrainers(response.data);
                setLoading(false);
            })
            .catch(err => {
                setError('Błąd podczas pobierania listy trenerów');
                setLoading(false);
            });
    }, []);

    if (loading) return <div>Ładowanie...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div className="trainer-list-container">
            <div className="trainer-block-list">
                {trainers.map(trainer => (
                    <div className="trainer-block" key={trainer.idCoach}>
                        <div className="trainer-info">
                            <span className="trainer-name">
                                {trainer.idCoachNavigation?.name} {trainer.idCoachNavigation?.surname}
                            </span>
                            {trainer.specialization && (
                                <span className="trainer-specialization">
                                     Specjalizacja: {trainer.specialization}
                                </span>
                            )}
                        </div>
                        <SignupButtonWithModal trainer={trainer} />
                    </div>
                ))}
            </div>
        </div>
    );
}
export default TrainersListComponent;