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

ALTER TABLE Session AUTO_INCREMENT = 1;

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
("training", "2023-10-31 18:00:00", 7);

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

INSERT INTO PersonnelLocation
VALUES (1,1,'2017-01-01',null,'General Manager'),
	(2,1,'2017-01-01',null,'Deputy Manager'),
	(3,1,'2017-01-01',null,'Treasurer'),
	(4,1,'2017-01-01',null,'Secretary'),
	(5,1,'2017-01-01',null,'Admin'),
	(6,2,'2019-01-01',null,'Admin'),
	(7,2,'2019-01-01',null,'Treasurer'),
	(8,2,'2019-06-01',null,'Coach'),
	(9,2,'2019-06-01',null,'Assistant Coach'),
	(10,3,'2017-01-01',null,'Admin'),
	(11,3,'2017-01-01',null,'Treasurer'),
	(12,3,'2017-09-01',null,'Coach'),
	(13,3,'2017-09-01',null,'Assistant Coach'),
	(14,4,'2018-01-01',null,'Admin'),
	(15,4,'2018-01-01',null,'Treasurer'),
	(16,4,'2018-01-01',null,'Coach'),
	(17,4,'2018-01-01',null,'Assistant Coach'),
	(18,5,'2018-01-01',null,'Admin'),
	(19,5,'2018-01-01',null,'Treasurer'),
	(20,5,'2018-03-01',null,'Coach'),
	(21,5,'2018-03-01',null,'Assistant Coach'),
	(22,1,'2017-06-01',null,'Coach'),
	(23,1,'2017-06-01',null,'Assistant Coach'),
	(24,1,'2021-01-01','2023-01-25','Admin'),
	(25,6,'2025-01-01',null,'Admin'),
	(26,7,'2023-01-01',null,'Admin');

ALTER TABLE ClubMember AUTO_INCREMENT = 1;

INSERT INTO ClubMember (first_name, last_name, dob, email, height, weight, social_sec_num, med_card_num, phone_number, city, province, postal_code, address, progress_report, is_active, family_member_id_fk, primary_relationship, secondary_relationship)
VALUES ('Cotton', 'Joe', '2012-10-03', 'Gegagedigedagedago@yahoo.com', 157.48, 160.00, '437022223', '648492359553', '5147585160', 'Montreal', 'QC', 'H3H2P2', '6354 Boardfish Road', 'Needs to lose weight', 1, 3, 'nephew', 'nephew'),
       ('Julia', 'Himenez', '2010-03-10', 'JHarmony@googleplus.com', 160.34, 125.00, '439238588', '892748374776', '5142398475', 'Montreal', 'QC', 'H3H2P4', '12317 Hirani Lane', 'Very good at volleyball!', 1, 4, 'daughter', 'grand-daughter'),
       ('John', 'Chena', '2008-04-12', 'UCantSeeMe@WWE.com', 182.88, 200.00, '434848584', '283934393293', '5146869348', 'Montreal', 'QC', 'G0A1H0', '4572 Champ Street', 'Very muscular, hard to find, and already balding', 1, 6, 'son', 'grandson'),
       ('Mario', 'Mario', '2008-02-26', 'Wahoo@wahoo.com', 155.00, 154.00, '121334325', '903923903445', '5147823456', 'Laval', 'QC', 'H7A0A1', '1234 Mushroom Kingdom', 'Jumps incredibly high, has a mild shroom addiction', 1, 10, 'cousin', 'cousin'),
       ('Luigi', 'Mario', '2009-10-10', 'Wahoo2@wahoo.com', 180.00, 154.00, '121334236', '903923903545', '5147829072', 'Laval', 'QC', 'H7A0A1', '1234 Mushroom Kingdom', 'Jumps even higher than his brother, same addiciton', 1, 10, 'cousin', 'cousin'),
       ('Alex', 'Tremblay', '2000-05-15', 'alex.tremblay@example.com', 180.00, 165.34, '123456789', '123456654321', '5145551234', 'Montreal', 'QC', 'H2X1Y4', '123 Rue St-Denis', 'Good spiker, improving on defense.', 0, 3, 'brother', 'brother'),
       ('Marie', 'Lefebvre', '1995-08-22', 'marie.lefebvre@example.com', 165.12, 136.68, '987654321', '654321789123', '5145555678', 'Montreal', 'QC', 'H3Z2J1', '456 Av. des Pins', 'Strong setter with excellent game sense.', 0, 7, 'cousin', 'cousin'),
       ('Julien', 'Bergeron', '1998-12-10', 'julien.bergeron@example.com', 175.56, 176.37, '321549876', '789123574829', '5145559012', 'Montreal', 'QC', 'H4B3K5', '789 Blvd. René-Lévesque', 'Powerful hitter but needs work on stamina.', 0, 2, 'brother', 'brother'),
       ('Sophie', 'Ducharme', '1996-03-28', 'sophie.ducharme@example.com', 170.32, 149.91, '456789123', '234567689345', '5145553456', 'Montreal', 'QC', 'H5A4M2', '101 Rue Sainte-Catherine', 'Great libero with fast reflexes.', 0, 9, 'sister', 'daughter'),
       ('Étienne', 'Gagnon', '1999-07-04', 'etienne.gagnon@example.com', 185.16, 187.39, '789123456', '345678362782', '5145557890', 'Montreal', 'QC', 'H6C5N3', '202 Av. du Parc', 'Versatile player, excels in blocking.', 0, 5, 'cousin', 'cousin'),
       ('Lucas', 'Beaulieu', '2010-06-12', 'lucas.beaulieu@example.com', 165.45, 140.00, '112456789', '123456789012', '5145551122', 'Montreal', 'QC', 'H3B1A4', '300 Rue Sherbrooke', 'Developing setter with good agility.', 0, 1, 'cousin', 'cousin'),
       ('Émilie', 'Chartrand', '2012-09-25', 'emilie.chartrand@example.com', 150.40, 110.00, '223567890', '234567890124', '5145552233', 'Montreal', 'QC', 'H3C2B5', '123 Rue de la Montagne', 'Quick reflexes but needs power.', 0, 5, 'niece', 'granddaughter'),
       ('Antoine', 'Desrosiers', '2008-02-17', 'antoine.desrosiers@example.com', 170.12, 160.50, '334678901', '345678901235', '5145553344', 'Montreal', 'QC', 'H4A3C6', '456 Rue Sainte-Catherine', 'Strong attacker, improving technique.', 0, 3, 'nephew', 'nephew'),
       ('Camille', 'Fournier', '2011-11-03', 'camille.fournier@example.com', 155.11, 120.22, '445789012', '456789012346', '5145554455', 'Montreal', 'QC', 'H2X4D7', '789 Boulevard René-Lévesque', 'Excellent defense, needs serve work.', 0, 2, 'daughter', 'niece'),
       ('Mathis', 'Lavoie', '2006-04-29', 'mathis.lavoie@example.com', 180.10, 175.59, '556890123', '567890123456', '5145555566', 'Montreal', 'QC', 'H1Y5E8', '1010 Rue Sherbrooke', 'All-rounder, great blocking skills.', 0, 7, 'cousin', 'cousin'),
       ('Sophie', 'Pelletier', '2009-07-19', 'sophie.pelletier@example.com', 162.91, 130.83, '667901234', '678901234567', '5145556677', 'Montreal', 'QC', 'H3T6F9', '1111 Avenue du Parc', 'Developing spiker, solid stamina.', 0, 4, 'niece', 'granddaughter'),
       ('Noah', 'Gauthier', '2013-01-08', 'noah.gauthier@example.com', 145.00, 100.00, '778012345', '789012345678', '5145557788', 'Montreal', 'QC', 'H3Z7G0', '1212 Rue Saint-Denis', 'Young player with great potential.', 0, 6, 'son', 'grandson'),
       ('Isabelle', 'Morin', '2005-12-14', 'isabelle.morin@example.com', 172.00, 150.00, '889123456', '890123456789', '5145558899', 'Montreal', 'QC', 'H4G8H1', '1313 Rue de la Commune', 'Smart playmaker, solid court awareness.', 0, 9, 'cousin', 'cousin'),
       ('Gabriel', 'Côté', '2014-05-30', 'gabriel.cote@example.com', 138.00, 90.00, '990234567', '901234567890', '5145559900', 'Montreal', 'QC', 'H5H9J2', '1414 Rue Notre-Dame', 'Promising skills, needs experience.', 0, 8, 'son', 'grandson'),
       ('Chloé', 'Bouchard', '2007-08-11', 'chloe.bouchard@example.com', 160.67, 125.15, '101345678', '012345678901', '5145551011', 'Montreal', 'QC', 'H3B0K3', '1515 Rue Saint-Laurent', 'Fast reactions, improving accuracy.', 0, 10, 'daughter', 'granddaughter'),
       ('William', 'Dubois', '2011-06-23', 'william.dubois@example.com', 158.52, 135.00, '212456789', '123456789013', '5145552122', 'Montreal', 'QC', 'H2X1L4', '1616 Rue Sainte-Famille', 'Consistent serves, needs mobility.', 0, 2, 'nephew', 'nephew'),
       ('Élodie', 'Roy', '2004-03-17', 'elodie.roy@example.com', 178.00, 165.90, '323567890', '234567890123', '5145553233', 'Montreal', 'QC', 'H3C2M5', '1717 Rue Saint-Urbain', 'Great vertical jump, strong in defense.', 0, 3, 'cousin', 'cousin'),
       ('Thomas', 'Lévesque', '2015-09-09', 'thomas.levesque@example.com', 140.31, 95.63, '434678901', '345678901234', '5145554344', 'Montreal', 'QC', 'H4A3N6', '1818 Rue Saint-Antoine', 'Energetic but lacks discipline.', 0, 1, 'nephew', 'nephew'),
       ('Léa', 'Bélanger', '2010-10-20', 'lea.belanger@example.com', 168.00, 145.12, '545789012', '456789012345', '5145555455', 'Montreal', 'QC', 'H5A4P7', '1919 Rue Saint-Hubert', 'Balanced skill set, improving leadership.', 0, 5, 'niece', 'granddaughter');

DELIMITER $$

DROP PROCEDURE IF EXISTS insert_into_club_member_location $$

CREATE PROCEDURE insert_into_club_member_location()

BEGIN
	DECLARE maxCMN INT DEFAULT 0;
    DECLARE increment INT DEFAULT 1;
    
	SET maxCMN = (SELECT MAX(cmn) FROM ClubMember);
    
    WHILE (increment <= maxCMN) DO
		INSERT INTO ClubMemberLocation
        SELECT
			location_id_fk,
			CM.cmn AS cmn_fk,
			start_date,
			end_date
		FROM FamilyMemberLocation FML
		JOIN FamilyMember FM ON FML.family_member_id_fk = FM.id
		JOIN ClubMember CM ON FM.id = CM.family_member_id_fk AND CM.cmn = increment
		WHERE FML.end_date IS NULL;
                
		SET increment = increment + 1;
    END WHILE;
END $$

CALL insert_into_club_member_location();

INSERT INTO TeamFormation (name,captain_id_fk,location_id_fk)
VALUES ('Altean Army',1,1),
	('Archanean League',1,1),
	('The Deliverance',2,1),
	('Pilgrimage Misfits',3,1),
	('Lycian League',4,1),
	('Golden Deer',5,1),
	('Banner of the Crest of Flames',6,1),
	('Kronos Island Giants',5,2),
	('Area Island Dragons',1,2),
	('Chaos Island Crusaders',2,2),
	('Rhea Island Nomads',5,2),
	('Ouranos Island Champions',5,2),
	('Production Destruction',9,5),
	('Security Lockdown',9,5),
	('Queens of Spin',4,5),
	('Big Bad Wolves',4,5),
	('My Team',10,3),
	('Enemy Team',3,3),
	('Spetsnaz',7,4),
	('SEALs',10,7);

INSERT INTO TeamSession (team_formation_id_fk, session_id_fk, score)
VALUES (1, 1, 85),
	(2, 1, 100),
	(3, 2, 70),
	(4, 2, 100),
	(5, 3, 60),
	(6, 3, 100),
	(7, 4, 90),
	(8, 4, 100),
	(9, 5, 75),
	(10, 5, 100),
	(11, 6, 80),
	(12, 6, 100),
	(13, 7, 95),
	(14, 7, 100),
	(15, 8, 85),
	(16, 8, 100),
	(17, 9, 78),
	(18, 9, 100),
	(19, 10, 92),
	(20, 10, 100),
	(1, 11, 80),
	(2, 11, 100),
	(3, 12, 70),
	(4, 12, 100),
	(5, 13, 75),
	(6, 13, 100),
	(7, 14, 90),
	(8, 14, 100),
	(9, 15, 85),
	(10, 15, 100),
	(11, 16, 60),
	(12, 16, 100),
	(13, 17, 95),
	(14, 17, 100),
	(19, 20, 85),
	(20, 20, 100);

-- Outside hitters (IDs 6-10)
INSERT INTO TeamMember (team_formation_id_fk, cmn_fk, role, assignment_date_time)
VALUES
(6, 6, "outside hitter", "2023-01-10 18:00:00"),
(7, 7, "outside hitter", "2023-01-10 18:00:00"),
(8, 8, "outside hitter", "2023-01-10 18:00:00"),
(9, 9, "outside hitter", "2023-01-10 18:00:00"),
(10, 10, "outside hitter", "2023-01-10 18:00:00");

-- All rounders (team members who have filled every role)
INSERT INTO TeamMember (team_formation_id_fk, cmn_fk, role, assignment_date_time)
VALUES
(1, 1, "outside hitter", "2023-01-10 18:00:00"),
(2, 1, "opposite", "2023-01-11 18:00:00"),
(3, 1, "setter", "2023-01-12 18:00:00"),
(4, 1, "middle blocker", "2023-01-13 18:00:00"),
(5, 1, "libero", "2023-01-14 18:00:00"),
(6, 1, "defensive specialist", "2023-01-15 18:00:00"),
(7, 1, "serving specialist", "2023-01-16 18:00:00"),

(2, 2, "outside hitter", "2023-01-10 18:00:00"),
(3, 2, "opposite", "2023-01-11 18:00:00"),
(4, 2, "setter", "2023-01-12 18:00:00"),
(5, 2, "middle blocker", "2023-01-13 18:00:00"),
(6, 2, "libero", "2023-01-14 18:00:00"),
(7, 2, "defensive specialist", "2023-01-15 18:00:00"),
(8, 2, "serving specialist", "2023-01-16 18:00:00"),

(3, 3, "outside hitter", "2023-01-10 18:00:00"),
(4, 3, "opposite", "2023-01-11 18:00:00"),
(5, 3, "setter", "2023-01-12 18:00:00"),
(6, 3, "middle blocker", "2023-01-13 18:00:00"),
(7, 3, "libero", "2023-01-14 18:00:00"),
(8, 3, "defensive specialist", "2023-01-15 18:00:00"),
(9, 3, "serving specialist", "2023-01-16 18:00:00"),

(4, 4, "outside hitter", "2023-01-10 18:00:00"),
(5, 4, "opposite", "2023-01-11 18:00:00"),
(6, 4, "setter", "2023-01-12 18:00:00"),
(7, 4, "middle blocker", "2023-01-13 18:00:00"),
(8, 4, "libero", "2023-01-14 18:00:00"),
(9, 4, "defensive specialist", "2023-01-15 18:00:00"),
(10, 4, "serving specialist", "2023-01-16 18:00:00"),

(5, 5, "outside hitter", "2023-01-10 18:00:00"),
(6, 5, "opposite", "2023-01-11 18:00:00"),
(7, 5, "setter", "2023-01-12 18:00:00"),
(8, 5, "middle blocker", "2023-01-13 18:00:00"),
(9, 5, "libero", "2023-01-14 18:00:00"),
(10, 5, "defensive specialist", "2023-01-15 18:00:00"),
(1, 5, "serving specialist", "2023-01-16 18:00:00");

-- Filling teams up to minimum required team members (3)
INSERT INTO TeamMember (team_formation_id_fk, cmn_fk, role, assignment_date_time)
VALUES
(1, 4, "libero", "2023-01-10 18:00:00"),
(2, 4, "libero", "2023-01-11 18:00:00");

INSERT INTO TeamMember (team_formation_id_fk, cmn_fk, role, assignment_date_time)
VALUES
(11, 1, "serving specialist", "2023-02-10 18:00:00"),
(11, 2, "defensive specialist", "2023-02-11 18:00:00"),
(11, 3, "middle blocker", "2023-02-11 18:00:00"),

(12, 2, "serving specialist", "2023-02-12 18:00:00"),
(12, 3, "defensive specialist", "2023-02-12 18:00:00"),
(12, 4, "middle blocker", "2023-02-12 18:00:00"),

(13, 3, "serving specialist", "2023-02-13 18:00:00"),
(13, 4, "defensive specialist", "2023-02-13 18:00:00"),
(13, 5, "middle blocker", "2023-02-13 18:00:00"),

(14, 4, "serving specialist", "2023-03-10 18:00:00"),
(14, 5, "defensive specialist", "2023-03-11 18:00:00"),
(14, 1, "middle blocker", "2023-03-11 18:00:00"),

(15, 5, "serving specialist", "2023-04-10 18:00:00"),
(15, 1, "defensive specialist", "2023-04-11 18:00:00"),
(15, 2, "middle blocker", "2023-04-11 18:00:00"),

(16, 1, "serving specialist", "2023-04-10 18:00:00"),
(16, 3, "defensive specialist", "2023-04-11 18:00:00"),
(16, 5, "middle blocker", "2023-04-11 18:00:00"),

(17, 2, "serving specialist", "2023-04-20 18:00:00"),
(17, 4, "defensive specialist", "2023-04-20 18:00:00"),
(17, 1, "middle blocker", "2023-04-20 18:00:00"),

(18, 3, "serving specialist", "2023-05-13 18:00:00"),
(18, 5, "defensive specialist", "2023-05-13 18:00:00"),
(18, 2, "middle blocker", "2023-05-13 18:00:00"),

(19, 4, "serving specialist", "2023-07-10 18:00:00"),
(19, 1, "defensive specialist", "2023-07-11 18:00:00"),
(19, 3, "middle blocker", "2023-07-12 18:00:00"),

(20, 2, "serving specialist", "2023-11-10 18:00:00"),
(20, 4, "defensive specialist", "2023-11-11 18:00:00"),
(20, 5, "middle blocker", "2023-11-11 18:00:00");

-- Filling Payment up to minimum required 
INSERT INTO Payment (amount, paymentDate, effectiveDate, method, cmn_fk) VALUES
    (100.00, '2021-01-01', '2022-01-01', 'Debit', 1),
    (100.00, '2021-01-01', '2022-01-01', 'Cash', 2),
    (100.00, '2021-01-01', '2022-01-01', 'Cash', 3),
    (100.00, '2021-01-01', '2022-01-01', 'Debit', 4),
    (100.00, '2021-01-01', '2022-01-01', 'Debit', 5),
    (100.00, '2022-01-01', '2023-01-01', 'Debit', 1),
    (100.00, '2022-01-01', '2023-01-01', 'Cash', 2),
    (100.00, '2022-01-01', '2023-01-01', 'Cash', 3),
    (100.00, '2022-01-01', '2023-01-01', 'Debit', 4),
    (100.00, '2022-01-01', '2023-01-01', 'Debit', 5),
    (50.00, '2022-01-10', '2023-01-01', 'Debit', 7),
    (45.00, '2022-01-22', '2023-01-01', 'Cash', 8),
    (60.00, '2022-02-15', '2023-01-01', 'Cash', 6),
    (10.00, '2022-03-25', '2023-01-01', 'Debit', 9),
    (75.00, '2022-04-02', '2023-01-01', 'Debit', 10),
    (40.00, '2022-04-17', '2023-01-01', 'Credit', 9),
    (50.00, '2022-05-03', '2023-01-01', 'Credit', 9),
    (50.00, '2022-05-29', '2023-01-01', 'Debit', 7),
    (45.00, '2022-06-30', '2023-01-01', 'Cash', 8),
    (20.00, '2022-07-09', '2023-01-01', 'Credit', 6),
    (20.00, '2022-08-14', '2023-01-01', 'Cash', 8),
    (50.00, '2022-08-21', '2023-01-01', 'Debit', 10),
    (20.00, '2022-09-07', '2023-01-01', 'Credit', 6),
    (20.00, '2022-10-13', '2023-01-01', 'Credit', 6),
    (10.00, '2022-11-02', '2023-01-01', 'Debit', 7),
    (100.00, '2023-01-01', '2024-01-01', 'Debit', 1),
    (100.00, '2023-01-01', '2024-01-01', 'Cash', 2),
    (100.00, '2023-01-01', '2024-01-01', 'Cash', 3),
    (100.00, '2023-01-01', '2024-01-01', 'Debit', 4),
    (100.00, '2023-01-01', '2024-01-01', 'Debit', 5);
