import React from 'react';
import Header from '../components/Header';
import Sidebar from '../components/Sidebar';
import TrainerList from '../components/TrainerListComponent';

function PersonalTrainingPage() {
    return (
        <div className="dashboard-container">
            <Header />
            <div className="dashboard-body">
                <aside className="dashboard-sidebar">
                    <Sidebar />
                </aside>
                <main className="dashboard-main">
                    <h2 className="personal-training-title"> Lista trenerów</h2>
                    <TrainerList />
                </main>
            </div>
        </div>
    );
}

export default PersonalTrainingPage;