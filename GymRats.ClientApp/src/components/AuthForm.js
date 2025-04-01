import React from 'react';
import email_icon from '../assets/img/email.png';
import password_icon from '../assets/img/password.png';
import perosn_icon from '../assets/img/person.png';
function refreshPage() {
    window.location.href = "http://localhost:3000/login";

}
const AuthForm = ({ action, name, setName, surname, setSurname, email, setEmail, password, setPassword, confirmPassword, setConfirmPassword, onSubmit }) => {
    return (
        <form onSubmit={onSubmit}>
            {action === "Stwórz konto" && (
                <>
                    <div class="mb-3" >
                        <label for="exampleInputName" class="form-label" style={{ color: 'white' }}>Imie</label>
                        <input
                            type='text'
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            class="form-control" id="exampleInputName" aria-describedby="nameHelp"
                            required
                        />
                    </div>
                    <div class="mb-3" >
                        <label for="exampleInputSurname" class="form-label" style={{ color: 'white' }}>Nazwisko</label>
                        <input
                            type='text'
                            value={surname}
                            onChange={(e) => setSurname(e.target.value)}
                            class="form-control" id="exampleInputSurname" aria-describedby="surNameHelp"
                            required
                        />
                    </div>
                </>
            )}
            <div class="mb-3" >
                <label for="exampleInputEmail1" class="form-label" style={{ color: 'white' }}>Email</label>
                <input
                    type='email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp"
                    required
                />
            </div>
            <div class="mb-3">
                <label for="inputPassword5" class="form-label" style={{ color: 'white' }}>Hasło</label>
                <input
                    type='password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    class="form-control" id="exampleInputPassword1"
                    required
                />
            </div>
            {action === "Stwórz konto" && (
                <div class="mb-3">
                    <label for="inputPassword5" class="form-label" style={{ color: 'white' }}>Potwierdź hasło</label>
                    <input
                        type='password'
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        class="form-control" id="exampleInputPassword1"
                        required
                    />
                </div>

            )}
            {action === "Stwórz konto" ? <button class="btn" type='submit' /*onClick={refreshPage}*/ >{action}</button> : <button class="btn" type='submit' >{action}</button>}
        </form>
    );
};

export default AuthForm;