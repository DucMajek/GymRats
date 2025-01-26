import React, {useState} from 'react'
import '../assets/styles/login.css'
function Login(){
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = (e) => {
      e.preventDefault();
  };
    return(
      <div className='loginPanel'>
        <center><h2>Login</h2></center>
                <div className="header__content">
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
                    <button className= 'btn' type='submit'>Login</button>
                </form>
                </div>
          </div>
    
    );
};

export default Login;
