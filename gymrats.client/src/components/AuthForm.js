import React from 'react';
import email_icon from '../assets/img/email.png';
import password_icon from '../assets/img/password.png';
import perosn_icon from '../assets/img/person.png';
function refreshPage(){
    window.location.href = "http://localhost:3000/login";
    
} 
const AuthForm = ({ action, name, setName, surname, setSurname, email, setEmail, password, setPassword, confirmPassword, setConfirmPassword, onSubmit }) => {
    return (

        <form onSubmit={onSubmit}>
            {action === "Sign Up" && (
                <>
                    <div className='input'>
                        <img src={perosn_icon} alt='' />
                        <input
                            type='text'
                            placeholder='Enter name'
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            onKeyDown={(e) => { e.key === 'Enter' && e.preventDefault() }}
                            required
                        />
                    </div>
                    <div className='input'>
                        <img src={perosn_icon} alt='' />
                        <input
                            type='test'
                            placeholder='Enter surname'
                            value={surname}
                            onChange={(e) => setSurname(e.target.value)}
                            onKeyDown={(e) => { e.key === 'Enter' && e.preventDefault() }}
                            required
                        />
                    </div>
                </>
            )}
            <div className='input'>
                <img src={email_icon} alt='' />
                <input
                    type='email'
                    placeholder='Enter email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    onKeyDown={(e) => { e.key === 'Enter' && e.preventDefault() }}
                    required
                />
            </div>
            <div className='input'>
                <img src={password_icon} alt='' />
                <input
                    type='password'
                    placeholder='Enter password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    onKeyDown={(e) => { e.key === 'Enter' && e.preventDefault() }}
                    required
                />
            </div>
            {action === "Sign Up" && (
                <div className='input'>
                    <img src={password_icon} alt='' />
                    <input
                        type='password'
                        placeholder='Confirm password'
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        onKeyDown={(e) => { e.key === 'Enter' && e.preventDefault() }}
                        required
                    />
                </div>
                
            )}
            {action === "Sign Up" ? <button className='btn' type='submit' onClick={refreshPage} >{action}</button> : <button className='btn' type='submit' >{action}</button>}
        </form>
    );
};

export default AuthForm;