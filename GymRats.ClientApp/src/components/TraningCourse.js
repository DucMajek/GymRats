import React, { useEffect, useState } from "react";
import axios from "axios";
import "../assets/styles/HomePage.css";
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';

function TraningCourse() {
    const [courses, setCourses] = useState([]);
    const [coaches, setCoaches] = useState({});

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
                    if (!coachData[course.trenerIdTrenera]) {
                        try {
                            const coachResponse = await axios.get(
                                `https://localhost:44380/coaches/${course.trenerIdTrenera}`
                            );
                            coachData[course.trenerIdTrenera] = coachResponse.data;
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

    return (
        <div id="courses">
            <h2 className="section__header">KURSY NA TRENERA</h2>
            <p className="section__subheader">
                Nasza oferta kursów trenerskich jest zróżnicowana, aby sprostać różnym potrzebom i
                celom zawodowym. Oferujemy szeroki wybór programów edukacyjnych, które są dopasowane do osób na różnych etapach kariery.
            </p>
            
            <div id="coursesCarousel" className="carousel slide mx-auto price__grid" style={{ maxWidth: '500px'}}>
                <div className="carousel-inner">
                    {courses.map((course, index) => (
                        <div className={`carousel-item ${index === 0 ? 'active' : ''}`} key={course.idKursu}>
                            <div className="course__card" style={{ Width: '700px', height: '380px'}}>
                                <center><h3>{course.nazwa}</h3></center>
                                <center><h4>Czas trwania: {course.czasTrwania}</h4></center>
                                {coaches[course.trenerIdTrenera] ? (
                                    <center><h4>{coaches[course.trenerIdTrenera].imie} {coaches[course.trenerIdTrenera].nazwisko}</h4></center>
                                ) : (
                                    <center><h4>Ładowanie...</h4></center>
                                )}
                                <button className="btn price__btn">Dowiedz się więcej</button>
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
        </div>
    );
}

export default TraningCourse;