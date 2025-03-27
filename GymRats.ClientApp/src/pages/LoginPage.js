import Login from "../components/Login";
import GymRats_logo from '../assets/img/GymRats_Logo.png';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import '../assets/styles/HomePage.css';
function LoginPage() {

    return (
        <div>
            <nav>
                <div className="nav__logo">
                    <a href="/"><img src={GymRats_logo} alt="gymRatsLogo_resized" className="img-logo" /></a>
                </div>
                <ul className="nav__links">
                    <li className="link"><a href="/">Strona Główna</a></li>
                    <li className="link">
                        <Link to="/login">Konto</Link>
                    </li>
                </ul>
            </nav>
            <Login />
        </div>
    );
}

export default LoginPage;