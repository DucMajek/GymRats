import React, { useEffect, useState } from "react";
import axios from "axios";
import "../assets/styles/HomePage.css";
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';
import PopupButton from "./PopupButton";

function TraningCourse() {
    const [courses, setCourses] = useState([]);
    const [coaches, setCoaches] = useState({});
    const [showPopup, setPopup] = useState(false);
    const [selectedCourse, setSelectedCourse] = useState(null);

    useEffect(() => {
        const getCoursesData = async () => {
            try {
                const courseResponse = await axios.get(
                    "https://localhost:44380/Courses",
                    { responseType: "json" }
                );
                setCourses(courseResponse.data);

                const coachData = {};
                for (const course of courseResponse.data) {
                    if (!coachData[course.trenerIdTrener]) {
                        try {
                            const coachResponse = await axios.get(
                                `https://localhost:44380/coaches/${course.trenerIdTrener}`
                            );
                            coachData[course.trenerIdTrener] = coachResponse.data;
                        } catch (error) {
                            console.error(`Failed to fetch coach for course ${course.idKursu}:`, error);
                        }
                    }
                }
            
                setCoaches(coachData);
            } catch (error) {
                console.error("Failed to fetch courses data:", error);
            }
        };

        getCoursesData();
    }, []);

    const handleShowPopup = (course) => {
        setSelectedCourse(course);
        setPopup(true);
    };

    return (
        <div id="courses">
            <h2 className="section__header">KURSY NA TRENERA</h2>
            <p className="section__subheader">
                Nasza oferta kursów trenerskich jest zróżnicowana, aby sprostać różnym potrzebom i 
                celom zawodowym. Oferujemy szeroki wybór programów edukacyjnych, które są dopasowane do osób na różnych etapach kariery.
            </p>
            <div id="coursesCarousel" className="carousel slide mx-auto" style={{ maxWidth: '600px' }}>
                <div className="carousel-inner">
                    {courses.map((course, index) => (
                        <div className={`carousel-item ${index === 0 ? 'active' : ''}`} key={course.idKursu}>
                            <div className="course__card" style={{ width: 'auto', height: 'auto' }}>
                                <center><h3>{course.nazwa}</h3></center>
                                <br />
                                <br />
                                <button
                                    className="btn price__btn"
                                    onClick={() => handleShowPopup(course)}
                                >
                                    Dowiedz się więcej
                                </button>
                            </div>
                        </div>
                    ))}
                </div>

                <button className="carousel-control-prev border-0 bg-transparent" type="button" data-bs-target="#coursesCarousel" data-bs-slide="prev">
                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Previous</span>
                </button>
                <button className="carousel-control-next border-0 bg-transparent" type="button" data-bs-target="#coursesCarousel" data-bs-slide="next">
                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Next</span>
                </button>
            </div>
            {showPopup && (
                <PopupButton
                    onClose={() => setPopup(false)}
                    course={selectedCourse}
                    coach={selectedCourse ? coaches[selectedCourse.trenerIdTrener] : null}
                />
            )}
        </div>
    );
}

export default TraningCourse;