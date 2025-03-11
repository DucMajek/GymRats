import member from '../assets/img/member.jpg';
import member2 from '../assets/img/member2.jpg';
import React, { useEffect } from 'react';

function AboutUs() {
    useEffect(() => {
        const link = document.createElement('link');
        link.href = 'https://cdn.jsdelivr.net/npm/remixicon@3.4.0/fonts/remixicon.css';
        link.rel = 'stylesheet';
        document.head.appendChild(link);
    
        return () => {
          document.head.removeChild(link);
        };
      }, []);
    return (
        <div id='about__us'>
            <section className="review">
                <div className="section__container review__container">
                    <span><i className="ri-group-fill"></i></span>
                    <div className="review__content">
                        <h4>O nas</h4>
                        <p>
                            To, co naprawdę wyróżnia nas, to nasz zespół dwóch ekspertów. Nasi autorzy są doświadczeni, pomocni i szczerze zaangażowani w pomoc innym w osiąganiu ich celów.
                            Poświęcają czas na zrozumienie indywidualnych potrzeb i tworzą spersonalizowane treści,
                            które zapewniają maksymalne rezultaty i pełne bezpieczeństwo.
                        </p>
                        <div className="review__footer">
                            <div class="review__member">
                                <img src={member} alt="member" />
                                <div class="review__member__details">
                                    <h4>Michał Majek</h4>
                                    <p>PJATK Student</p>
                                </div>
                                <img src={member2} alt="member" />
                                <div class="review__member__details">
                                    <h4>Duc Anh Dinh</h4>
                                    <p>PJATK Student</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    )
}

export default AboutUs;