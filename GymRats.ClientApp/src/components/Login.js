import React, { useState } from 'react';
import '../assets/styles/Login.css';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import AuthForm from './AuthForm';
import { useAuth } from './AuthContext';
function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [action, setAction] = useState("Zaloguj");
    const { login: authLogin } = useAuth();
    const navigate = useNavigate();
    const handleSubmit = (e) => {
        e.preventDefault();
        if (action === "Zaloguj") {
            axios.post('https://localhost:44380/login', { email, password })
                .then(result => {
                    console.log(result);

                    if (result.status === 200) {
                        authLogin(email);
                        navigate('/dashboard');
                    }
                })
                .catch(err => { console.log(err); });
        } else if (action === "Stwórz konto") {
            axios.post(`https://localhost:44380/register?Email=${email}&Password=${password}&Imie=${name}&Nazwisko=${surname}`, { email, password, name, surname })
                .then(result => {
                    if (result.status === 200) {
                    }
                })
                .catch(err => { console.log(err); });
        }

        else {
            console.log("Signing up with:", email, password, confirmPassword);
        }
    };

    return (
        <div className='container'>
            <div className='header'>
                <div className='text'>{action}</div>
                <div className='underline'></div>
            </div>
            <div className='inputs'>
                <AuthForm
                    action={action}
                    name={name}
                    setName={setName}
                    surname={surname}
                    setSurname={setSurname}
                    email={email}
                    setEmail={setEmail}
                    password={password}
                    setPassword={setPassword}
                    confirmPassword={confirmPassword}
                    setConfirmPassword={setConfirmPassword}
                    onSubmit={handleSubmit}
                />
            </div>
            <div className='forgot-password'>Zapomniałeś hasła? <span>Kliknij tutaj!</span></div>
            <div className='submit-container'>
                <div className={action === "Zaloguj" ? "submit gray" : "submit"} onClick={() => { setAction("Stwórz konto"); setEmail(""); 
                    setPassword("") }}>Stwórz konto</div>
                <div className={action === "Stwórz konto" ? "submit gray" : "submit"} onClick={() =>{ setAction("Zaloguj"); 
                    setEmail(""); setPassword(""); setConfirmPassword("") }}>Logowanie</div>
            </div>
        </div>
    );
}

export default Login;