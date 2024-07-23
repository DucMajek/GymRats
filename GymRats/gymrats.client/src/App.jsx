/*import { useEffect, useState } from 'react';*/
import './App.css';
import Navbar from './components/Navbar'
import background from './assets/background.png'
function App() {
    return (
        <div>
            <img src={background} alt="Background" className="backgorund-image" />
            <Navbar />
        </div>
    );
    
}

export default App;