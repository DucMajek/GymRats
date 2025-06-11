import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.js'
import './index.css'
import 'bootstrap/dist/css/bootstrap.min.css';
export { default as BMICalculator } from './components/BMICalculator'; 



createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
  
)
