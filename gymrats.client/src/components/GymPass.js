import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../assets/styles/HomePage.css'; 
function GymPass() {
    const [gymPassCategory, setGymPassCategory] = useState([]);

    useEffect(() => {
        const getGymPassData = async () => {
            try {
                const response = await axios.get('https://localhost:7200/GymPassCategory', { responseType: 'json' });
                
                setGymPassCategory(response.data);
                console.log(response.data);
            } catch (error) {
                console.error("Failed to fetch gym pass data:", error);
            }
        };
        getGymPassData();
    }, []);

    return (
        <div className="price__grid">
            {Array.isArray(gymPassCategory) && gymPassCategory.map((item) => (
                <div className="price__card" key={item.name}>
                    <div className="price__card__content">
                        <h4><center>{item.nazwa}</center></h4>
                        <h3><center>{item.cena} PLN</center></h3>
                        <p>
                        {item.opis.split('\n').map((line, index) => (
                            <React.Fragment key={index}>
                                <i className="ri-checkbox-circle-line"></i>
                                {line}
                                <br />
                            </React.Fragment>
                        ))}
                    </p>
                    </div>
                    <button className="btn price__btn">Kup Teraz</button>
                </div>
            ))}
        </div>
    );
}

export default GymPass;
