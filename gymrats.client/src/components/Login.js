import React, {useState} from 'react'
import GymRats_logo from '../assets/img/GymRats_Logo.png';
import  '../assets/styles/login.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Signup from './Signup';
const Login = () =>{
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleLogin = (e) => {
        e.preventDefault();
    };

    return(
        <div>
            <nav>
                <div className="nav__logo">
                <Link to="/"><img src={GymRats_logo} alt="gymRatsLogo_resized" className="img-logo" /></Link>
                </div>
                <ul className="nav__links">
                    <li className="link"><Link to="/">Strona Główna</Link></li>
                    <li className="link"><a href="#">Karnety</a></li>
                    <li className="link"><a href="#">Kursy</a></li>
                    <li className="link"><a href="#">O nas</a></li>
                    <li className="link">
                        <Link to="/login">Konto</Link>
                    </li>
                </ul>
            </nav>
            <div className='App'>
                <ul>
                    <li><Link to="/login">Login</Link></li>
                    <li><Link to="/signup">Signup</Link></li>
                </ul>
                <h2>Login</h2>
                <form onSubmit={handleLogin}> 
                    <input 
                        type='email' 
                        placeholder='Enter email' 
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                    <input 
                        type='password' 
                        placeholder='Enter password' 
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                    <button type='submit'>Login</button>
                </form>
            </div>
            <Signup/>
        </div>
    );
};

export default Login;
