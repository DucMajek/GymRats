import React, {useState} from 'react'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';

const Signup = () =>{
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const handleSignup = (e) => {
        e.preventDefault();
    };

    return(
        <div>
            <div className='App'>
                <h2>Signup</h2>
                <form onSubmit={handleSignup}> 
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
                    <input 
                        type='password' 
                        placeholder='Enter confirm password' 
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        required
                    />
                    <button type='submit'>Signup</button>
                </form>
            </div>
        </div>
    );
};

export default Signup;