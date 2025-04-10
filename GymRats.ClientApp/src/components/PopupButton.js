import React, { useRef } from 'react';
import { X } from 'lucide-react';

function PopupButton({ onClose, course, coach }) {
    const popupRef = useRef();
    
    const closePopup = (e) => {
        if(popupRef.current === e.target) {
            onClose();
        }
    }

    return (
        <div 
            ref={popupRef} 
            onClick={closePopup} 
            className='fixed inset-0  bg-opacity-70 backdrop-blur-sm flex justify-center items-center'
            style={{ zIndex: 1000 }}
        >
            <div className='relative bg-indigo-600 rounded-xl p-8 max-w-md w-full mx-4' style={{zIndex: 1001}}>
                <button 
                    onClick={onClose} 
                    className='absolute top-4 right-4 text-white hover:text-gray-200'
                >
                    <X size={24} />
                </button>
                
                <center>
                <div className="text-white space-y-4">
                    <h1 className='text-2xl font-bold'>{course?.nazwa || "Brak danych"}</h1>
                    <p><strong>Czas trwania:</strong> {course?.czasTrwania || "Brak danych"}</p>
                    {coach && (
                        <p><strong>Trener:</strong> {coach.imie} {coach.nazwisko}</p>
                    )}
                    <p>{course?.opis || "Brak danych"}</p>
                </div>
                </center>
            </div>
        </div>
    );
}

export default PopupButton;