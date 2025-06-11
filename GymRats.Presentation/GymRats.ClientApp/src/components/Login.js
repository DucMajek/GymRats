import React, { useState } from 'react';
import '../assets/styles/Login.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import AuthForm from './AuthForm';
import { useAuth } from './AuthContext';
function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [birthday, setBirthday] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [gender, setGender] = useState("");
    const [address, setAddress] = useState("");
    const [flatNumber, setFlatNumber] = useState("");
    const [zipCode, setZipCode] = useState("");
    const [place, setPlace] = useState("");
    const [action, setAction] = useState("Zaloguj");
    const { login: authLogin } = useAuth();
    const navigate = useNavigate();
    const handleSubmit = (e) => {
        e.preventDefault();
        if (action === "Zaloguj") {
            axios.post('https://localhost:44380/login', { email, password })
                .then(result => {
                    if (result.status === 200) {
                        const token = result.data.token;
                        authLogin(email, token);
                        navigate('/user-profile');
                    }
                })
                .catch(err => { console.log(err); });
        } else if (action === "Stwórz konto") {
            axios.post(`https://localhost:44380/register?Email=${email}&Password=${password}&Name=${name}&Surname=${surname}&Birthday=${birthday}&PhoneNumber=${phoneNumber}&Gender=${gender}&Address=${address}&FlatNumber=${flatNumber}&ZipCode=${zipCode}&Place=${place}
                        `, { email, password, name, surname, birthday, phoneNumber, gender, address, flatNumber, zipCode, place })
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
                    birthday={birthday}
                    setBirthday={setBirthday}
                    phoneNumber={phoneNumber}
                    setPhoneNumber={setPhoneNumber}
                    gender={gender}
                    setGender={setGender}
                    address={address}
                    setAddress={setAddress}
                    flatNumber={flatNumber}
                    setFlatNumber={setFlatNumber}
                    zipCode={zipCode}
                    setZipCode={setZipCode}
                    place={place}
                    setPlace={setPlace}
                    onSubmit={handleSubmit}
                />
            </div>
            <div className='submit-container'>
                <div className={action === "Zaloguj" ? "submit gray" : "submit"} onClick={() => {
                    setAction("Stwórz konto"); setEmail("");
                    setPassword("")
                }}>Stwórz konto</div>
                <div className={action === "Stwórz konto" ? "submit gray" : "submit"} onClick={() => {
                    setAction("Zaloguj");
                    setEmail(""); setPassword(""); setConfirmPassword("")
                }}>Logowanie</div>
            </div>
        </div>
    );
}

export default Login;