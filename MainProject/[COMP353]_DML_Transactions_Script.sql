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
