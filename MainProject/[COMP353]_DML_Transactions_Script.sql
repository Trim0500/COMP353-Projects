ALTER TABLE Location AUTO_INCREMENT = 1;

INSERT INTO Location (type,name,postal_code,province,address,city,website_url,capacity)
VALUES ('Head','MYVC HQ','G6H5M7','QC','4803 Broadcast Drive','Montreal','someboguswebsite.com',50),
		('Branch','MYVC Branch 1','G6H5M8','QC','4804 Broadcast Drive','Montreal','someotherboguswebsite.com',25),
        ('Branch','MYVC Branch 2','G6H5M9','QC','4805 Broadcast Drive','Montreal','anotherboguswebsite.com',20),
        ('Branch','MYVC Branch 3','G6H5M7','QC','4806 Broadcast Drive','Montreal','thisboguswebsite.com',30),
        ('Branch','MYVC Branch 4','G6H5M6','QC','4807 Broadcast Drive','Montreal','thatboguswebsite.com',10),
        ('Branch','Trimaloo Branch','J2X5S8','QC','32 rue Rene','Saint-Jean-sur-Richelieu','agent.nexone.ca',15),
        ('Branch','Merski Branch','G6H5M5','QC','1515 Sainte-Catherine St W','Montreal','aboguswebsite.com',35);

INSERT INTO LocationPhone
VALUES (1,'4385265985'),
		(2,'5146866801'),
        (3,'3432628383'),
        (4,'5147094066'),
        (5,'3333334444'),
        (6,'4388858146'),
        (7,'3320170519');

INSERT INTO FamilyMember (first_name, last_name, dob, email, social_sec_num, med_card_num, city, province, phone_number, postal_code)
VALUES 
("Paul", "Denton", "2010-01-01", "pdenton@unatco.com", "123454321", "ABCD12345678", "Montreal", "QC", "5145551234", "H3H1M1"),
("Char", "Aznable", "2013-01-01", "char@zeonmail.fr", "234565432", "BCDE23456789", "Montreal", "QC", "5145552645", "H3H2P2"),
("Matthew", "Erskine", "2013-01-01", "matt@mattmail.ca", "345676543", "ADCD12345678", "Montreal", "QC", "5145351234", "H3H4M1"),
("Chris", "Trinh", "2013-01-01", "chris@chrismail.com", "235565432", "ACDC21256789", "Montreal", "QC", "5145571234", "H3H1P1"),
("Nanako", "Dojima", "2009-01-01", "ndoji@ocdsb.ca", "234576432", "BCDE23454789", "Ottawa", "ON", "5145551232", "H3D1M1"),
("Iroquois", "Pliskin", "2012-01-01", "snake@snakemail.com", "734585432", "ABCD12345778", "Montreal", "QC", "8145551234", "H3H4M1"),
("Napoleon", "Bonaparte", "2013-01-01", "napoleon@revolutionmail.fr", "234795432", "BCDE23456689", "Montreal", "QC", "5141251234", "H3H6M1"),
("Maximilien", "Robespierre", "2009-01-01", "max@revolutionmail.fr", "234575432", "ABCD12345378", "Montreal", "QC", "5145901234", "H3H4M1"),
("Jean-Paul", "Marat", "2008-01-01", "jp@revolutionmail.com", "234535437", "BCDE23456189", "Montreal", "QC", "5140051234", "H3H7M1"),
("Elton", "John", "2011-01-01", "elton@johnmail.com", "234515732", "ABCD12345078", "Montreal", "QC", "5145550234", "Z3H1M1");

INSERT INTO SecondaryFamilyMember (primary_family_member_id_fk, first_name, last_name, phone_number, relationship_to_primary)
VALUES
(1, "JC", "Denton", "5145551234", "brother"),
(2, "Sayla", "Mass", "5145552645", "sister"),
(3, "Matthew", "Ersmine", "5145351234", "evil clone"),
(4, "Zaid", "Minhas", "5145571234", "mother"),
(5, "Ryotaro", "Dojima", "5145551232", "father"),
(6, "Hideo", "Kojima", "8145551234", "father"),
(7, "Maria", "Ramolino", "5141251234", "mother"),
(8, "Jacqueline", "Carrault", "5145901234", "mother"),
(9, "Louise", "Cabrol", "5140051234", "mother"),
(10, "Stanley", "Dwight", "5145550234", "father");

INSERT INTO FamilyMemberLocation (family_member_id_fk, location_id_fk, start_date, end_date)
VALUES
(1, 1, "2020-01-01", null),
(1, 2, "2019-01-01", null),
(1, 3, "2020-01-01", "2025-01-01"),
(2, 2, "2021-01-01", null),
(2, 3, "2022-01-01", null),
(2, 1, "2021-01-01", null),
(3, 3, "2017-01-01", null),
(3, 4, "2024-01-01", null),
(3, 1, "2024-01-01", null),
(4, 4, "2018-01-01", null),
(4, 5, "2024-01-01", null),
(4, 1, "2019-01-01", null),
(5, 1, "2017-01-01", null),
(5, 2, "2020-01-01", null),
(5, 3, "2020-01-01", null),
(6, 1, "2022-01-01", null),
(6, 2, "2022-01-01", "2025-01-01"),
(7, 4, "2023-01-01", null),
(7, 7, "2023-01-01", "2025-01-01"),
(8, 7, "2023-01-01", null),
(8, 1, "2019-01-01", "2025-01-01"),
(9, 5, "2018-01-01", null),
(9, 4, "2019-01-01", null),
(10, 3, "2023-01-01", null),
(10, 7, "2023-01-01", null);

INSERT INTO Session (event_type, event_date_time, location_id_fk)
VALUES
("training", "2024-12-10 18:00:00", 1),
("training", "2025-01-01 18:00:00", 1),
("training", "2025-02-10 18:00:00", 1),
("game", "2025-03-09 18:00:00", 1),
("game", "2025-03-10 18:00:00", 1),
("training", "2024-11-24 18:00:00", 2),
("training", "2025-02-11 18:00:00", 2),
("training", "2025-02-19 18:00:00", 2),
("game", "2025-02-08 18:00:00", 2),
("game", "2025-03-01 18:00:00", 2),
("training", "2025-03-10 18:00:00", 3),
("game", "2025-01-10 18:00:00", 3),
("game", "2025-01-11 18:00:00", 3),
("game", "2025-03-02 18:00:00", 4),
("game", "2025-03-03 18:00:00", 4),
("training", "2025-02-01 18:00:00", 5),
("training", "2025-02-02 18:00:00", 5),
("game", "2025-03-13 18:00:00", 5),
("game", "2025-03-14 18:00:00", 5),
("training", "2023-10-31 18:00:00", 6),
("training", "2022-10-31 18:00:00", 7);

ALTER TABLE Personnel AUTO_INCREMENT = 1;

INSERT INTO Personnel (first_name,last_name,dob,social_sec_num,med_card_num,phone_number,city,province,postal_code,email,mandate)
VALUES ('Donkey','Kong','1981-07-09','503124158','680969607','5068994178','Kongo Bongo','DC','G7H5M5','konkey@dong.com','paid'),
		('Diddy','Kong','1994-11-21','503124159','680969608','5068994179','Kongo Bongo','DC','G7H5M6','kiddy@dong.com','paid'),
        ('Dixie','Kong','1995-12-04','503124157','680969609','5068994177','Kongo Bongo','DC','G7H5M7','kixie@dong.com','paid'),
        ('Kiddy','Kong','1996-11-01','503124156','680969610','5068994176','Kongo Bongo','DC','G7H5M8','kiddy@kong.com','paid'),
        ('King','Kong','2005-12-04','503124155','680969611','5068994175','Kongo Bongo','DC','G7H5M9','king@kong.com','volunteer'),
        ('Funky','Kong','1994-11-21','503124154','680969612','5068994174','Kongo Bongo','DC','G7H5N0','kunky@fong.com','volunteer'),
        ('Cranky','Kong','1994-11-21','503124153','680969613','5068994173','Kongo Bongo','DC','G7H5N1','kranky@cong.com','volunteer'),
        ('Some1','Personnel','1994-11-20','503124152','680969614','5068994171','Kongo Bongo','DC','G7H5N2','some@email.com','paid'),
        ('Jean', 'Tremblay', '1994-11-20', '503124151', '680969615', '5068994170', 'Montreal', 'QC', 'H3B1X8', 'jean.t@email.com', 'paid'),
		('Sophie', 'Lavoie', '1987-05-14', '402315678', '785412369', '6014785963', 'Quebec City', 'QC', 'G1A0A2', 'sophie.l@email.com', 'volunteer'),
		('Marc', 'Bouchard', '1990-08-25', '512478963', '632587412', '7203659874', 'Laval', 'QC', 'H7N3Y6', 'marc.b@email.com', 'paid'),
		('Isabelle', 'Gagnon', '1992-11-30', '698745123', '874125963', '9876543210', 'Gatineau', 'QC', 'J8X3X5', 'isabelle.g@email.com', 'volunteer'),
		('Luc', 'Côté', '1985-03-17', '325698741', '745698231', '6541237890', 'Longueuil', 'QC', 'J4K2V2', 'luc.c@email.com', 'paid'),
		('Marie', 'Fortin', '1993-07-22', '478563214', '369874521', '8745213698', 'Sherbrooke', 'QC', 'J1H5B9', 'marie.f@email.com', 'volunteer'),
		('Philippe', 'Pelletier', '1996-12-10', '365214789', '987452136', '3698741256', 'Saguenay', 'QC', 'G7H5N2', 'philippe.p@email.com', 'paid'),
		('Élise', 'Desjardins', '1989-06-18', '478596321', '521478963', '3214785962', 'Trois-Rivières', 'QC', 'G8T1V2', 'elise.d@email.com', 'volunteer'),
		('Maxime', 'Roy', '1995-09-05', '698745236', '874125632', '9876541236', 'Drummondville', 'QC', 'J2B6V4', 'maxime.r@email.com', 'paid'),
		('Caroline', 'Lévesque', '1984-04-29', '325698742', '745698232', '6541237891', 'Saint-Jean-sur-Richelieu', 'QC', 'J3B2W5', 'caroline.l@email.com', 'volunteer'),
		('François', 'Dubois', '1991-02-14', '478563215', '369874522', '8745213699', 'Saint-Jérôme', 'QC', 'J7Z4M2', 'francois.d@email.com', 'paid'),
		('Julie', 'Mercier', '1998-07-09', '365214790', '987452137', '3698741257', 'Granby', 'QC', 'J2G3V7', 'julie.m@email.com', 'volunteer'),
		('Olivier', 'Beaulieu', '1986-11-11', '478596322', '521478964', '3214785963', 'Chicoutimi', 'QC', 'G7H1Y4', 'olivier.b@email.com', 'paid'),
		('Camille', 'Simard', '1997-10-15', '698745237', '874125633', '9876541237', 'Blainville', 'QC', 'J7C4G6', 'camille.s@email.com', 'volunteer'),
        ('Alexandre', 'Morin', '1992-04-12', '569874123', '321478965', '4507891234', 'Terrebonne', 'QC', 'J6X4H5', 'alexandre.m@email.com', 'paid'),
		('Nathalie', 'Bergeron', '1988-09-27', '785412369', '698745239', '4384567890', 'Brossard', 'QC', 'J4W3K6', 'nathalie.b@email.com', 'volunteer'),
		('Patrick', 'Giroux', '1995-06-15', '965874123', '125478963', '4187896541', 'Victoriaville', 'QC', 'G6P4X8', 'patrick.g@email.com', 'paid'),
		('Catherine', 'Dion', '1999-01-30', '745698321', '369874128', '5149874563', 'Shawinigan', 'QC', 'G9N2K7', 'catherine.d@email.com', 'volunteer');

INSERT INTO ClubMember (first_name, last_name, dob, email, height, weight, social_sec_num, med_card_num, phone_number, city, province, postal_code, address, progress_report, is_active, family_member_id_fk, primary_relationship, secondary_relationship)
VALUES ('Cotton', 'Joe', '2012-10-03', 'Gegagedigedagedago@yahoo.com', 157.48, 160.00, '437022223', '648492359553', '5147585160', 'Montreal', 'Quebec', 'H3H2P2', '6354 Boardfish Road', 'Needs to lose weight', 1, 3, 'nephew', 'nephew'),
	   ('Julia', 'Himenez', '2010-03-10', 'JHarmony@googleplus.com', 160.34, 125.00, '439238588', '892748374776', '5142398475', 'Montreal', 'Quebec', 'H3H2P4', '12317 Hirani Lane', 'Very good at volleyball!', 1, 4, 'daughter', 'grand-daughter');

