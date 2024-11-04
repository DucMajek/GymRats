import React from "react";


export const DarkModeToggle = () => {
 const setDarkMode = () => {
    document.querySelector("body").setAttribute('data-theme', 'dark')
 }

 const setLightMode = () => {
    document.querySelector("body").setAttribute('data-theme', 'light')
 }
    
    const toggleTheme = e => {
        if(e.target.checked) setDarkMode();
        else setLightMode();
    }
    return(
        <div className='dark_mode'>
            <input
            className='dark_mode_input'
            type='checkbox'
            id='darkmode-toggle'
            onChange={toggleTheme}
            />
        </div>

    );
};