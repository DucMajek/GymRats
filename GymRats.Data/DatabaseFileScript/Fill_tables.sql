Use GymRats
--Typ karnetu
insert into [dbo].[Type_pass] (gym_pass_name, price, duration_pass, description) values
('Miesięczny', 120 , 30,'Dostęp do klubu 24/7
	Dostęp do jadłospisów'),
	('Półroczny', 160 , 180,'Dostęp do klubu 24/7
	Woda na trening gratis
	Dostęp do jadłospisów'),
	('Roczny',250 , 365,'12 miesięcy treningu w cenie 10
	Dostęp do klubu 24/7
	Woda na trening gratis
	Dostęp do jadłospisów')

--Status karnetu
insert into Pass_status(status_type) values
('aktywny'),
('rezygnacja'),
('zamrozony')

--Osoba
INSERT INTO Person (name, surname, birthday, phone_number, gender, address, flat_number, zip_code, place)
VALUES (
    'Jan',
    'Kowalski',
    '1985-05-15',
    '+48 123 456 789',
    'Mężczyzna',
    'ul. Słoneczna',
    '12/4',
    '00-001',
    'Warszawa'
);

INSERT INTO Person (name, surname, birthday, phone_number, gender, address, flat_number, zip_code, place)
VALUES (
    'Anna',
    'Nowak',
    '1990-08-22',
    '+48 987 654 321',
    'Kobieta',
    'ul. Kwiatowa',
    '5',
    '30-002',
    'Kraków'
);

INSERT INTO Person (name, surname, phone_number, gender)
VALUES (
    'Piotr',
    'Wiśniewski',
    '+48 555 123 456',
    'Mężczyzna',
    175
);

INSERT INTO Person (name, surname, birthday, phone_number, gender, address, flat_number, zip_code, place)
VALUES (
    'Maria',
    'Lewandowska',
    '1982-11-03',
    '+48 666 789 123',
    'Kobieta',
    'ul. Leśna',
    '8B',
    '80-003',
    'Gdańsk'
);
INSERT INTO Person (name, surname, birthday, phone_number, gender, address, flat_number, zip_code, place)
VALUES (
    'admin',
    'admin',
    '',
    '+48 000 000 000',
    'Kobieta',
    '',
    '',
    '',
    ''
);

--Rola
insert into Role (role_name) values
('user'),
('coach'),
('admin')

--Użytkownik
insert into [dbo].[User](id_user, email, password, id_role) values
  (1, 'jkowalski@example.com', '977EDD674FBB9F2E09424A8018FE1AA2:+Kg70O5mhWmcZkD/5KfSBYqk0BMJCKcY2U4GLP96Iuk=', 2),
  (2, 'anowak@example.com', '977EDD674FBB9F2E09424A8018FE1AA2:+Kg70O5mhWmcZkD/5KfSBYqk0BMJCKcY2U4GLP96Iuk=', 2),
  (3, 'pwisniewski@example.com', '977EDD674FBB9F2E09424A8018FE1AA2:+Kg70O5mhWmcZkD/5KfSBYqk0BMJCKcY2U4GLP96Iuk=', 2),
  (4, 'mlewandowska@example.com', '977EDD674FBB9F2E09424A8018FE1AA2:+Kg70O5mhWmcZkD/5KfSBYqk0BMJCKcY2U4GLP96Iuk=', 2),
  (5, 'admin@admin.com', '977EDD674FBB9F2E09424A8018FE1AA2:+Kg70O5mhWmcZkD/5KfSBYqk0BMJCKcY2U4GLP96Iuk=', 3)

--Trener
insert into Coach (id_coach,specialization) values
(1,'Przygotowanie motoryczne'),
(2,'Trening funkcjonalny'),
(3,'Budowanie masy mięśniowej'),
(4,'Podstawy treningu siłowego')

--Kurs trenera
insert into Trainer_Course (course_name, duration, description, id_coach) values
('Podstawy treningu siłowego','15 H', 'Jeżli dopiero zaczynasz swoją przygodę z siłownią, ten program to idealny wybór. Skoncentrujemy się na nauce prawidłowej techniki wykonywania ćwiczeń, pracy z wolnymi ciężarami i opanowaniu podstawowych wzorców ruchowych. Dzięki temu unikniesz kontuzji, zbudujesz silne fundamenty i szybciej zauważysz efekty swojej pracy. Program jest dopasowany do Twojego poziomu zaawansowania i tempa, z naciskiem na indywidualne podejście oraz stopniowe zwiększanie intensywności. Zyskaj pewność siebie na siłowni i trenuj świadomie już od pierwszego dnia!
',4),
('Budowanie masy mięśniowej', '15 H', 'Chcesz powiększyć swoje mięśnie, poprawić proporcje ciała i zyskać atletyczną sylwetkę Ten program został stworzony z myślą o maksymalnym rozwoju masy mięśniowej przy jednoczesnym zachowaniu zdrowego podejścia do treningu i regeneracji. Obejmuje on kompleksowe plany treningowe, ukierunkowane na hipertrofię, a także podstawy odżywiania i suplementacji wspierające wzrost mięśni. Wykorzystujemy sprawdzone metody progresji, odpowiednią objętość treningową i indywidualnie dobrane ćwiczenia, które pozwolą Ci osiągnąć widoczne rezultaty w możliwie najkrótszym czasie.
',3),
('Przygotowanie motoryczne', '15 H', 'Ten rodzaj treningu to coś więcej niż tylko praca nad wyglądem – to kompleksowe podejście do rozwoju fizycznego. Program przygotowania motorycznego jest stworzony z myślą o osobach aktywnych, sportowcach amatorach i zawodnikach, którzy chcą poprawić swoją szybkość, siłę, moc, koordynację oraz wytrzymałość. Ćwiczenia są dostosowane do konkretnej dyscypliny sportowej lub celów funkcjonalnych – wszystko po to, by Twoje ciało działało lepiej, szybciej i bardziej efektywnie. To trening, który przygotuje Cię do sportowych wyzwań i codziennych zadań, zwiększając Twoją sprawność na każdym poziomie.
', 1),
('Trening funkcjonalny', '15 H','Trening funkcjonalny to idealne rozwiązanie dla osób, które chcą poprawić swoją ogólną sprawność, uniknąć kontuzji i zadbać o zdrowe, sprawne ciało na lata. Skupiamy się na ćwiczeniach, które odwzorowują naturalne wzorce ruchowe – takie jak przysiady, wypady, rotacje czy unoszenia. Dzięki temu poprawiasz nie tylko siłę, ale także stabilność, koordynację i mobilność, co przekłada się na lepsze funkcjonowanie w życiu codziennym. Trening jest dopasowany do Twoich możliwości i celów, a jego różnorodność sprawia, że każda sesja jest ciekawa i efektywna. Świetny wybór dla każdego – niezależnie od wieku czy poziomu zaawansowania.
',2)

--Plan treningowy
INSERT INTO Training_plan (training_plan_name, training_plan_file)
VALUES ( 
    'Biegacz_Plan_treningowy', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Treningi\Biegacz_Plan_treningowy.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
    'Kolarz_Plan_treningowy', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Treningi\Kolarz_Plan_treningowy.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
    'Narciarz_Plan_treningowy', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Treningi\Narciarz_Plan_treningowy.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
    'Pływak_Plan_treningowy', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Treningi\Pływak_Plan_treningowy.pdf', SINGLE_BLOB) AS pdf_content)
);

--Jadlospisy
INSERT INTO Food_ebook(calories, diet_type, ebook_file)
VALUES 
--Sportowa
( 
	'1500',
    'Sportowa', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Sportowa\Sport_1500.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'1800',
    'Sportowa', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Sportowa\Sport_1800.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'2000',
    'Sportowa', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Sportowa\Sport_2000.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'2500',
    'Sportowa', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Sportowa\Sport_2500.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'3000',
    'Sportowa', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Sportowa\Sport_3000.pdf', SINGLE_BLOB) AS pdf_content)
),

--Standard
( 
	'1500',
    'Standard', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Standard\Standard_1500.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'1800',
    'Standard', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Standard\Standard_1800.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'2000',
    'Standard', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Standard\Standard_2000.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'2500',
    'Standard', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Standard\Standard_2500.pdf', SINGLE_BLOB) AS pdf_content)
),
( 
	'3000',
    'Standard', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\Standard\Standard_3000.pdf', SINGLE_BLOB) AS pdf_content)
),

--Wegetarianska
( 
	'1500',
    'Vegetarian', 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\\Wegetariańska\Vegetarian_1500.pdf', SINGLE_BLOB) AS pdf_content)
),																						 
( 																						 
	'1800',																					 
    'Vegetarian', 																		 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\\Wegetariańska\Vegetarian_1800.pdf', SINGLE_BLOB) AS pdf_content)
),																						 
( 																						 
	'2000',																					 
    'Vegetarian', 																		 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\\Wegetariańska\Vegetarian_2000.pdf', SINGLE_BLOB) AS pdf_content)
),																						 
( 																						 
	'2500',																					 
    'Vegetarian', 																		 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\\Wegetariańska\Vegetarian_2500.pdf', SINGLE_BLOB) AS pdf_content)
),																						 
( 																						 
	'3000',																					 
    'Vegetarian', 																		 
    (SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Users\micha\OneDrive\Pulpit\Diety\\Wegetariańska\Vegetarian_3000.pdf', SINGLE_BLOB) AS pdf_content)
)
--Zajęcia
insert into Group_classes (class_type, start_date, duration, group_size, id_coach) values
('Crossfit', '2025-06-09 17:00:00', 50, 20, 4),
('Pilates', '2025-06-11 17:00:00', 50, 20, 2),
('Boks', '2025-06-13 17:00:00', 50, 20, 1)