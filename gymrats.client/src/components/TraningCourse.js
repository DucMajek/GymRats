import React, { useEffect, useState } from "react";
import axios from "axios";
import "../assets/styles/HomePage.css";
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';

function TraningCourse() {
    const [courses, setCourses] = useState([]);
    const [coaches, setCoaches] = useState({});
    const responsive = {
        superLargeDesktop: {
          // the naming can be any, depends on you.
          breakpoint: { max: 4000, min: 3000 },
          items: 5
        },
        desktop: {
          breakpoint: { max: 3000, min: 1024 },
          items: 3
        },
        tablet: {
          breakpoint: { max: 1024, min: 464 },
          items: 2
        },
        mobile: {
          breakpoint: { max: 464, min: 0 },
          items: 1
        }
      };


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
                                `https://localhost:44380/Coach/${course.trenerIdTrenera}`
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
        <div id="courses" >
            <h2 className="section__header">KURSY NA TRENERA</h2>
            <p className="section__subheader">Nasza oferta kursów trenerskich jest zróżnicowana, aby sprostać różnym potrzebom i 
                celom zawodowym. Oferujemy szeroki wybór programów edukacyjnych, które są dopasowane do osób na różnych etapach kariery.</p>
            <Carousel className="price__grid" responsive={responsive}>
                {courses.map((course) => (
                    <div className="course__card" key={course.idKursu}>
                        <center><h3>{course.nazwa}</h3></center>
                        <center><h4>Czas trwania: {course.czasTrwania}</h4></center>
                        {coaches[course.trenerIdTrenera] ? (
                            <center><h4>{coaches[course.trenerIdTrenera].imie} {coaches[course.trenerIdTrenera].nazwisko}</h4></center>
                        ) : (
                            <center><h4>Ładowanie...</h4></center>
                        )}
                        <button className="btn price__btn">Dowiedz się więcej</button>
                    </div>
                ))}
            </Carousel>
        </div>
    );
}

export default TraningCourse;