// Button.js
import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
const Button = ({ size, text, variant, className }) => {
  const sizeClass = size === 'sm' ? 'btn-sm' : '';
  return (
    <button 
      className={`btn ${variant ? `btn-${variant}` : ''} ${sizeClass} ${className}`}
    >
      {text}
    </button>
  );
};

export default Button;