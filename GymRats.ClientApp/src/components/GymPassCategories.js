import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../assets/styles/HomePage.css';

function GymPassCategories() {
    const [gymPassCategory, setGymPassCategory] = useState([]);

    useEffect(() => {
        const getGymPassData = async () => {
            try {
                const response = await axios.get('https://localhost:44380/categories', { responseType: 'json' });

                setGymPassCategory(response.data);
            } catch (error) {
                console.error("Failed to fetch gym pass data:", error);
            }
        };
        getGymPassData();
    }, []);

    return (
        <div id="price__grid">
            <h2 className="section__header">KARNETY</h2>
            <p className="section__subheader">
                Nasz plan cenowy oferuje różne poziomy członkostwa, które są dopasowane do różnych preferencji i celów fitnessowych.
            </p>
            <div className="price__grid">
                {Array.isArray(gymPassCategory) && gymPassCategory.map((item) => (
                    <div className="price__card" key={item.id || item.name || Math.random()}>
                        <div className="price__card__content">
                            <h4><center>{item.nazwa}</center></h4>
                            <h3><center>{item.cena} PLN</center></h3>
                            <p>
                                {item.opis.split('\n').map((line, index) => (
                                    <React.Fragment key={`${item.id || item.name}-line-${index}`}>
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

        </div>
    );
}

export default GymPassCategories;
