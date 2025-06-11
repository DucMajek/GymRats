-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2025-04-27 15:50:05.756

-- tables
-- Table: Coach
CREATE TABLE Coach (
    id_coach int  NOT NULL,
    specialization varchar(max)  NOT NULL,
    CONSTRAINT Coach_pk PRIMARY KEY  (id_coach)
);

-- Table: Food_ebook
CREATE TABLE Food_ebook (
    id_ebook int  NOT NULL IDENTITY(1, 1),
    calories varchar(max)  NOT NULL,
    diet_type varchar(max)  NOT NULL,
    ebook_file varbinary(max)  NOT NULL,
    CONSTRAINT Food_ebook_pk PRIMARY KEY  (id_ebook)
);

-- Table: Group_classes
CREATE TABLE Group_classes (
    id_group int  NOT NULL IDENTITY(1, 1),
    class_type varchar(max)  NOT NULL,
    start_date datetime  NULL,
    group_size int  NOT NULL,
    id_coach int  NOT NULL,
    CONSTRAINT Group_classes_pk PRIMARY KEY  (id_group)
);

-- Table: Participation_in_classes
CREATE TABLE Participation_in_classes (
    id_participation int  NOT NULL IDENTITY(1, 1),
    id_group int  NOT NULL,
    id_user int  NOT NULL,
    CONSTRAINT Participation_in_classes_pk PRIMARY KEY  (id_participation)
);

-- Table: Pass_status
CREATE TABLE Pass_status (
    id_status int  NOT NULL,
    status_type varchar(max)  NOT NULL,
    CONSTRAINT Pass_status_pk PRIMARY KEY  (id_status)
);

-- Table: Person
CREATE TABLE Person (
    id_person int  NOT NULL IDENTITY(1, 1),
    name varchar(max)  NOT NULL,
    surname varchar(max)  NOT NULL,
    birthday date  NULL,
    phone_number varchar(max)  NOT NULL,
    gender varchar(max)  NOT NULL,
    address varchar(max)  NULL,
    flat_number varchar(max)  NULL,
    zip_code varchar(max)  NULL,
    place varchar(max)  NULL,
    CONSTRAINT Person_pk PRIMARY KEY  (id_person)
);

-- Table: Personal_training
CREATE TABLE Personal_training (
    id_personal_training int  NOT NULL IDENTITY(1, 1),
    reservation_date_time datetime  NULL,
    id_coach int  NOT NULL,
    id_user int  NOT NULL,
    CONSTRAINT Personal_training_pk PRIMARY KEY  (id_personal_training)
);

-- Table: Purchased_course
CREATE TABLE Purchased_course (
    id_purchased_course int  NOT NULL IDENTITY(1, 1),
    id_course int  NOT NULL,
    id_user int  NOT NULL,
    CONSTRAINT Purchased_course_pk PRIMARY KEY  (id_purchased_course)
);

-- Table: Role
CREATE TABLE Role (
    id_role int  NOT NULL,
    role_name varchar(50)  NOT NULL,
    CONSTRAINT Role_pk PRIMARY KEY  (id_role)
);

-- Table: Trainer_Course
CREATE TABLE Trainer_Course (
    id_course int  NOT NULL IDENTITY(1, 1),
    course_name varchar(max)  NOT NULL,
    duration varchar(max)  NOT NULL,
    description varchar(max)  NOT NULL,
    id_coach int  NOT NULL,
    CONSTRAINT Trainer_Course_pk PRIMARY KEY  (id_course)
);

-- Table: Training_plan
CREATE TABLE Training_plan (
    id_training_plan int  NOT NULL IDENTITY(1, 1),
    training_plan_name varchar(max)  NOT NULL,
    training_plan_file varbinary(max)  NOT NULL,
    CONSTRAINT Training_plan_pk PRIMARY KEY  (id_training_plan)
);

-- Table: Type_pass
CREATE TABLE Type_pass (
    id_type_pass int  NOT NULL IDENTITY(1, 1),
    gym_pass_name varchar(max)  NOT NULL,
    price int  NOT NULL,
    duration_pass int  NOT NULL,
    description varchar(max)  NOT NULL,
    CONSTRAINT Type_pass_pk PRIMARY KEY  (id_type_pass)
);

-- Table: User
CREATE TABLE "User" (
    id_user int  NOT NULL,
    email varchar(255)  NOT NULL,
    password varchar(255)  NOT NULL,
    id_role int  NOT NULL,
    CONSTRAINT User_pk PRIMARY KEY  (id_user)
);

-- Table: User_ebook
CREATE TABLE User_ebook (
    id_user_ebook int  NOT NULL IDENTITY(1, 1),
    id_user int  NOT NULL,
    id_ebook int  NOT NULL,
    CONSTRAINT User_ebook_pk PRIMARY KEY  (id_user_ebook)
);

-- Table: User_pass
CREATE TABLE User_pass (
    id_pass int  NOT NULL IDENTITY(1, 1),
    date_start date  NULL,
    date_end date  NULL,
    id_type_pass int  NOT NULL,
    id_user int  NOT NULL,
    id_status int  NOT NULL,
    CONSTRAINT User_pass_pk PRIMARY KEY  (id_pass)
);

-- Table: User_training_plan
CREATE TABLE User_training_plan (
    id_user_training_plan int  NOT NULL IDENTITY(1, 1),
    id_training_plan int  NOT NULL,
    id_user int  NOT NULL,
    CONSTRAINT User_training_plan_pk PRIMARY KEY  (id_user_training_plan)
);

-- foreign keys
-- Reference: Karnet_Typ_Karnetu (table: User_pass)
ALTER TABLE User_pass ADD CONSTRAINT Karnet_Typ_Karnetu
    FOREIGN KEY (id_type_pass)
    REFERENCES Type_pass (id_type_pass);

-- Reference: Karnet_Uzytkownik (table: User_pass)
ALTER TABLE User_pass ADD CONSTRAINT Karnet_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Kurs_Trenera_Trener (table: Trainer_Course)
ALTER TABLE Trainer_Course ADD CONSTRAINT Kurs_Trenera_Trener
    FOREIGN KEY (id_coach)
    REFERENCES Coach (id_coach);

-- Reference: Plan_treningowy_Uzytkownik_Plan_treningowy (table: User_training_plan)
ALTER TABLE User_training_plan ADD CONSTRAINT Plan_treningowy_Uzytkownik_Plan_treningowy
    FOREIGN KEY (id_training_plan)
    REFERENCES Training_plan (id_training_plan);

-- Reference: Plan_treningowy_Uzytkownik_Uzytkownik (table: User_training_plan)
ALTER TABLE User_training_plan ADD CONSTRAINT Plan_treningowy_Uzytkownik_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Trener_Osoba (table: Coach)
ALTER TABLE Coach ADD CONSTRAINT Trener_Osoba
    FOREIGN KEY (id_coach)
    REFERENCES Person (id_person);

-- Reference: Trening_Personalny_Trener (table: Personal_training)
ALTER TABLE Personal_training ADD CONSTRAINT Trening_Personalny_Trener
    FOREIGN KEY (id_coach)
    REFERENCES Coach (id_coach);

-- Reference: Trening_Personalny_Uzytkownik (table: Personal_training)
ALTER TABLE Personal_training ADD CONSTRAINT Trening_Personalny_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Udzial_w_zajeciach_Uzytkownik (table: Participation_in_classes)
ALTER TABLE Participation_in_classes ADD CONSTRAINT Udzial_w_zajeciach_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Udzial_w_zajeciach_Zajecia_Grupowe (table: Participation_in_classes)
ALTER TABLE Participation_in_classes ADD CONSTRAINT Udzial_w_zajeciach_Zajecia_Grupowe
    FOREIGN KEY (id_group)
    REFERENCES Group_classes (id_group);

-- Reference: User_pass_Pass_status (table: User_pass)
ALTER TABLE User_pass ADD CONSTRAINT User_pass_Pass_status
    FOREIGN KEY (id_status)
    REFERENCES Pass_status (id_status);

-- Reference: Uzytkownik_Jadlospis_Jadlospis (table: User_ebook)
ALTER TABLE User_ebook ADD CONSTRAINT Uzytkownik_Jadlospis_Jadlospis
    FOREIGN KEY (id_ebook)
    REFERENCES Food_ebook (id_ebook);

-- Reference: Uzytkownik_Jadlospis_Uzytkownik (table: User_ebook)
ALTER TABLE User_ebook ADD CONSTRAINT Uzytkownik_Jadlospis_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Uzytkownik_Kurs_Trenera_Kurs_Trenera (table: Purchased_course)
ALTER TABLE Purchased_course ADD CONSTRAINT Uzytkownik_Kurs_Trenera_Kurs_Trenera
    FOREIGN KEY (id_course)
    REFERENCES Trainer_Course (id_course);

-- Reference: Uzytkownik_Kurs_Trenera_Uzytkownik (table: Purchased_course)
ALTER TABLE Purchased_course ADD CONSTRAINT Uzytkownik_Kurs_Trenera_Uzytkownik
    FOREIGN KEY (id_user)
    REFERENCES "User" (id_user);

-- Reference: Uzytkownik_Osoba (table: User)
ALTER TABLE "User" ADD CONSTRAINT Uzytkownik_Osoba
    FOREIGN KEY (id_user)
    REFERENCES Person (id_person);

-- Reference: Uzytkownik_Rola (table: User)
ALTER TABLE "User" ADD CONSTRAINT Uzytkownik_Rola
    FOREIGN KEY (id_role)
    REFERENCES Role (id_role);

-- Reference: Zajecia_Grupowe_Trener (table: Group_classes)
ALTER TABLE Group_classes ADD CONSTRAINT Zajecia_Grupowe_Trener
    FOREIGN KEY (id_coach)
    REFERENCES Coach (id_coach);

-- End of file.

