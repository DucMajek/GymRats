import Login from "../components/Login";
import Navbar from "../components/Navbar";
import GymRats_logo from '../assets/img/GymRats_Logo.png';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import '../assets/styles/HomePage.css';
function LoginPage() {

    return (
        <div className='stickyNavbar'>
            <nav className="navbar navbar-expand-lg">
                <div className="container-fluid">
                    <a className="navbar-brand">
                        <img src={GymRats_logo} alt="gymRatsLogo_resized" className="img-logo" />
                    </a>
                    <ul className="navbar-nav" >
                        <li className="nav-item">
                            <a className="nav-link hover-orange fontColor" style={{marginRight: "20px"}} href="/">Strona Główna</a>
                        </li>
                    </ul>
                </div>
            </nav>
            <Login />
        </div>
    );
}

export default LoginPage;