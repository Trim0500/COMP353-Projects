INSERT INTO Location(name, city, postal_code, type, address, province, capacity, website)
VALUES
    ("MYVC Centre-Ville", "Montréal", "H3G2H7","Head", "1515 Rue Ste-Catherine W", "QC", 300, "myvc.ca/location/centreville"),
	("MYVC DDO", "Montréal", "H9B1Z6", "Branch", "3237 Des Sources Blvd", "QC", 200, "myvc.ca/location/ddo"),
	("MYVC Pierrefonds", "Montréal", "H9H4V6", "Branch", "14895 Pierrefonds Boul", "QC", 250, "myvc.ca/location/pierrefonds"),
    ("MYVC Brossard", "Montréal", "J4W1M9", "Branch", "7250 Taschereau Blvd", "QC", 275, "myvc.ca/location/brossard"),
    ("MYVC Toronto", "Toronto", "M1L2L1", "Branch", "1880 Eglinton Ave E", "ON", 400, "myvc.ca/location/toronto"),
    ("MYVC Brampton", "Toronto", "L6Y1N7", "Branch", "499 Main Street South", "ON", 300, "myvc.ca/location/brampton"),
    ("MYVC Ottawa", "Ottawa", "L2H2E9", "Branch", "7555 Montrose Road", "ON", 225, "myvc.ca/location/ottawa"),
    ("MYVC Victoria", "Victoria", "V8W1E5", "Branch", "851 Broughton St", "BC", 200, "myvc.ca/location/victoria"),
    ("MYVC Vancouver", "Vancouver", "V5Z3X7", "Branch", "555 W 12th Ave", "BC", 215, "myvc.ca/location/vancouver"),
    ("MYVC Surrey", "Vancouver", "V3T2X3", "Branch", "10642 King George Blvd", "BC", 290, "myvc.ca/location/surrey");

INSERT INTO LocationPhone(location_id_fk, phone_number)
VALUES
	(1, "5141234567"),
    (1, "5146784322"),
    (2, "5147891012"),
    (2, "4389105678"),
    (3, "514112131"),
    (3, "5142238899"),
    (4, "4387651234"),
    (5, "4165557890"),
    (6, "6473216543"),
    (7, "4378762109"),
    (8, "6044329988"),
    (9, "7789012345"),
    (10, "2366785555");
    
INSERT INTO Personnel(first_name, last_name, date_of_birth,
social_sec_number, med_card_number, phone_number, address, city, province, postal_code, email_address, role,
mandate)
VALUES 
	("Ana", "Amari", "1978-05-03", 184817362, 482163928, "5143729845", "1825 Saint-Denis St", "Montréal", "QC", "H2X3K4", "aamari@gmail.com", "Captain", "Salaried"),
	("Elizabeth", "Caledonia", "1995-06-17", 284721039, 183614736, "4387781256", "6780 Sherbrooke St W", "Montréal", "QC", "H4B1P8", "ecaledonia@gmail.com", "General Manager", "Salaried"),
	("Jean-Baptiste", "Augustin", "1992-08-25", 582947182, 193753713, "5146498802", "9122 Wellington St", "Montréal", "QC", "H4G1X3", "jbaugustin@gmail.com", "Deputy Manager", "Salaried"),
	("Brigitte", "Lindholm", "1995-06-17", 119847274, 189371731, "5143929345", "3425 Park Ave", "Montréal", "QC", "H2X2H6", "blindholm@gmail.com", "Secretary", "Salaried"),
	("Jesse", "McCree", "1990-11-05", 817462648, 182373611, "4382783256", "12 Crescent St", "Montréal", "QC", "H2X1H5", "jmccree@gmail.com", "Administrator", "Salaried"),
	("Hana", "Song", "1998-01-15", 173649123, 111111111, "5145923467", "2948 St Catherine St", "Montréal", "QC", "H4W1T5", "hsong@gmail.com", "Assistant Coach", "Volunteer"),
	("Akande", "Ogundimu", "1980-12-30", 198471637, 293481828, "4385923467", "1234 Rue Sainte-Catherine Ouest", "Montréal", "QC", "H4W1A5", "aogundimu@gmail.com", "Manager", "Salaried"),
	("Genji", "Shimada", "1998-01-15", 837467122, 192784111, "5147411802", "7788 Rue Jean-Talon Est", "Montréal", "QC", "H1S1K9", "gshimada@gmail.com", "Coach", "Salaried"),
	("Hanzo", "Shimada", "1978-02-11", 184736182, 991847326, "4382256229", "5678 Boulevard Saint-Laurent", "Montréal", "QC", "H2T1S8", "hshimada@gmail.com", "Manager", "Salaried"),
	("Illari", "Ruiz", "1985-03-13", 124918463, 185381737, "4381994822", "9101 Avenue du Parc", "Montréal", "QC", "H2N1Y7", "iruiz@gmail.com", "Coach", "Volunteer"),
	("Odessa", "Stone", "1977-12-12", 18383182, 183843000, "5149293838", "2345 Rue Sherbrooke Est", "Montréal", "QC", "H1V3G8", "ostone@gmail.com", "Manager", "Salaried"),
	("Jamison", "Fawkes", "1993-03-02", 948282182, 134432191, "5141221299", "6789 Boulevard Pie-IX", "Montréal", "QC", "H2C3Y1", "jfawkes@gmail.com", "Coach", "Salaried"),
	("Teo", "Minh", "1997-04-12", 188463817, 582753811, "4165923467", "123 Yonge Street", "Toronto", "ON", "M5C1W4", "tminh@gmail.com", "Manager", "Volunteer"),
	("Kiriko", "Yamagami", "1989-04-04", 948284644, 857173111, "416991334", "456 Bloor Street West", "Toronto", "ON" , "M5S1X8", "kyamagami@gmail.com", "Coach", "Salaried"),
	("Niran", "Pruksamanee", "1979-05-12", 194858194, 591848392, "4163859192", "789 Bay Street", "Toronto", "ON", "M5G2C8", "npruksamanee@gmail.com", "Assistant Coach", "Salaried"),
	("Lucio Correia", "dos Santos", "1997-04-09", 818433919, 473817837, "4160100303", "1011 Queen Street East", "Toronto", "ON", "M4M1K3", "ldossantos@gmail.com", "Manager", "Salaried"),
	("Mauga", "Ho'okano", "1987-02-21", 736481928, 847626333, "4160019488", "3498 Dundas Street West", "Toronto", "ON", "M6P1Y2", "mhookano@gmail.com", "Coach", "Volunteer"),
	("Mei-Ling", "Zhou", "1997-02-12", 728472811, 947190483, "4168183737", "6789 Finch Avenue East", "Toronto", "ON", "M1B1T1", "mzhou@gmail.com", "Administrator", "Salaried"),
	("Angela", "Ziegler", "1986-02-08", 958929485, 195838929, "3436461828", "100 Wellington Street", "Ottawa", "ON", "K1A0A6", "aziegler@gmail.com", "Manager", "Salaried"),
	("Moira", "O'Deorain", "1969-02-06", 737481737, 814784777, "6138580393", "230 Bank Street", "Ottawa", "ON", "K1M5M1", "modeorain@gmail.com", "Assistant Coach", "Volunteer"),
	("Orisa", "Oladele", "1999-10-10", 782389541, 858585929, "3439948575", "780 Rideau Street", "Ottawa", "ON", "K2P1X4", "ooladele@gmail.com", "Captain", "Salaried"),
	("Fareeha", "Amari", "1991-08-29", 746182833, 815473722, "7780203931", "737 Government Street", "Victoria", "BC", "VLV2L2", "famari@gmail.com", "Manager", "Salaried"),
	("Ramattra", "Tekhartha", "1990-04-26", 958385418, 905837261, "7788382812", "948 Douglas Street", "Victoria", "BC", "V8Y2P7", "rtekhartha@gmail.com", "Coach", "Volunteer"),
	("Mako", "Rutledge", "1984-10-30", 828481848, 717483812, "7781113456", "544 Fort Street", "Victoria", "BC", "V8W1P9", "mrutledge@gmail.com", "Administrator", "Salaried"),
	("Siebren", "de Kuiper", "1967-11-11", 184756381, 195747184, "6041938457", "384 Granville Street", "Vancouver", "BC", "V9Y4E3", "sdekuiper@gmail.com", "Manager", "Volunteer"),
	("Vivian", "Chase", "1979-04-06", 848184818, 192548291, "6049184818", "900 Broadway Street", "Vancouver", "BC", "V7A9R4", "vchase@gmail.com", "Coach", "Salaried"),
	("Jack", "Morrison", "1965-09-03", 537628374, 184757381, "6041138858", "650 Cambie Street", "Vancouver", "BC", "V9Y2E1", "jmorrison@gmail.com", "Administrator", "Salaried"),
	("Olivia", "Colomar", "1999-12-06", 904929474, 585858111, "6049482838", "7219 Main Street", "Vancouver", "BC", "V7C1Y7", "ocolomar@gmail.com", "Manager", "Salaried"),
	("Satya", "Vaswani", "2000-10-02", 384172444, 123476533, "6040103949", "5667 Scott Road", "Vancouver", "BC", "V3W5M8", "svaswani@gmail.com", "Coach", "Volunteer"),
	("Torbjörn", "Lindholm", "1967-01-14", 163749271, 195473311, "6044329191", "19 Westminster", "Vancouver", "BC", "V3J16X", "tlindholm@gmail.com", "Assistant Coach", "Salaried");

INSERT INTO PersonnelLocationDate(personnel_id_fk, location_id_fk, start_date, end_date)
VALUES
	(1, 1, "2020-02-02", NULL),
    (2, 1, "2019-01-10", NULL),
    (3, 1, "2021-08-10", NULL),
    (4, 1, "2018-11-12", NULL),
    (5, 1, "2019-12-07", NULL),
    (6, 1, "2013-09-09", NULL),
    (7, 2, "2016-12-04", NULL),
    (8, 3, "2010-01-14", "2014-01-11"),
    (8, 3, "2014-01-12", "2016-01-11"),
    (8, 4, "2016-01-12", "2017-04-22"),
    (8, 2, "2017-04-23", NULL),
    (9, 3, "2021-03-17", NULL),
    (10, 3, "2020-04-03", NULL),
    (11, 8, "2022-05-12", "2023-05-11"),
    (11, 4, "2023-05-12", NULL),
    (12, 4, "2021-04-19", NULL),
    (13, 5, "2014-03-26", NULL),
    (14, 5, "2019-02-21", NULL),
    (15, 5, "2024-01-01", NULL),
    (16, 6, "2020-10-10", NULL),
    (17, 6, "2011-10-12", NULL),
    (18, 6, "2018-04-16", NULL),
    (19, 4, "2020-02-05", "2021-08-04"),
    (19, 7, "2021-08-05", NULL),
    (20, 7, "2022-02-12", NULL),
    (21, 7, "2021-01-04", NULL),
    (22, 8, "2019-07-14", NULL),
    (23, 8, "2025-02-12", NULL),
    (24, 8, "2025-01-05", NULL),
    (25, 9, "2021-09-12", NULL),
    (26, 9, "2020-04-17", NULL), 
    (27, 9, "2019-07-07", NULL),
    (28, 10, "2011-01-28", "2017-04-18"),
    (28, 10, "2017-04-19", NULL),
    (29, 10, "2015-02-23", NULL),
    (30, 1, "2014-05-30", NULL);
    
INSERT INTO FamilyMember(first_name, last_name, date_of_birth, social_sec_number, med_card_number, phone_number, address, city,
    province, postal_code, email_address, location_id_fk) 
    VALUES
	('Ana', 'Amari', '1975-01-05', 184817362, 482163928, '5143729845', '1825 Saint-Denis St', 'Montréal', 'QC', 'H2X3K4', 'aamari@gmail.com', 1),
	('Lena', 'Oxton', '1989-12-01', 659972022, 449799927, '5149039162', '91082 Patricia Drives Suite 790 Brittneyhaven', 'Montréal', 'QC', 'H3A1A2', 'loxton@gmail.com', 7),
	('Sloan', 'Cameron', '1982-03-11', 917953430, 126964055, '4167776094', '17284 Hahn Via West Susan', 'Toronto', 'ON', 'M5A1A1', 'scameron@gmail.org', 10),
	('Amélie', 'Lacroix', '1945-01-30', 407171497, 563357273, '6134569199', '03829 Kimberly Camp Suite 901 Port Michaelmouth', 'Ottawa', 'ON', 'K1A0B1', 'alacroix@gmail.com', 8),
	('Harold', 'Winston', '1965-01-22', 682364860, 349687078, '6135675584', '09615 Amber Village Patricktown', 'Ottawa', 'ON', 'K2P2V9', 'hwinston@gmail.com', 2),
	('Aleksandra', 'Zaryanova', '1990-06-15', 521239482, 148220473, '6047362920', '7732 Wingate Court Unit 2527 Glendaleville', 'Vancouver', 'BC', 'V5K0A1', 'azaryanova@gmail.com', 3),
	('Zenyatta', 'Tekhartha', '1985-09-20', 135746908, 324758920, '4168776354', '4168 Bryant Expressway Apt. 722 Hamiltonville', 'Toronto', 'ON', 'M5V3C6', 'ztekhartha@gmail.com', 4),
	('Jeon', 'Heejin', '1972-11-12', 458302913, 952302183, '5147722445', '7754 Greendale Ave. Apt. 875 Ricardoville', 'Montréal', 'QC', 'H2X1Y4', 'jheejin@gmail.com', 5),
	('Kim', 'Hyunjin', '2001-04-27', 369029837, 532786495, '4168225687', '2352 Mountain Rd. North Michelleview', 'Toronto', 'ON', 'M4C1E1', 'khyunjin@gmail.com', 6),
	('Cho', 'Haseul', '1995-10-11', 741963285, 269423871, '6045276923', '5501 Cloud Dr. Victoria Heights', 'Victoria', 'BC', 'V8W3G4', 'chaseul@gmail.com', 9),
	('Wong', 'Kahei', '1988-08-30', 365172704, 147828630, '2503450720', '9470 Whitefish Blvd. Rockyton', 'Victoria', 'BC', 'V9A1A4', 'wkahei@gmail.com', 10),
	('Im', 'Yeojin', '1993-02-19', 539182471, 822365719, '6132378932', '2309 Dawn Dr. Ottawa Creek', 'Ottawa', 'ON', 'K1G1X2', 'iyeojin@gmail.com', 3),
	('Jung', 'Jinsoul', '1980-01-03', 761453209, 330852117, '4166657273', '9982 Ferry Rd. South Rosedale', 'Toronto', 'ON', 'M6K3C6', 'jjinsoul@gmail.com', 7),
	('Kim', 'Jungeun', '1977-03-21', 583634092, 761293848, '2504532722', '4664 Drew Blvd. Nanaimo', 'Vancouver', 'BC', 'V6G1H6', 'kjungeun@gmail.com', 5),
	('Choi', 'Yerim', '2000-09-27', 647390180, 591874382, '4169937486', '8051 Shorter St. West Maltonsburg', 'Toronto', 'ON', 'L5G4X6', 'cyerim@gmail.com', 4),
	('Ha', 'Sooyoung', '1987-06-05', 765493128, 924752697, '6133478281', '9359 Crystal Dr. Hillardown', 'Ottawa', 'ON', 'K1A0M7', 'hsooyoung@gmail.com', 6),
	('Kim', 'Jiwoo', '1983-11-16', 429083902, 740829382, '5149448352', '8957 Fort Ave. Grangeside', 'Montréal', 'QC', 'H3Z1X7', 'kjiwoo@gmail.com', 1),
	('Park', 'Chaewon', '1998-12-04', 381270491, 186929274, '2509751014', '7133 Riverdale St.', 'Vancouver', 'BC', 'V6X1A3', 'pchaewon@gmail.com', 9),
	('Olivia', 'Hye', '1979-04-23', 617890354, 974289753, '6132746592', '5271 West Rd. Brookfield', 'Ottawa', 'ON', 'K2P1A1', 'ohye@gmail.com', 2),
	('Torbjörn', 'Lindholm', '1967-01-14', 163749271, 195473311, '6044329191', '19 Westminster', 'Vancouver', 'BC', 'V3J16X', 'tlindholm@gmail.com', 1);

INSERT INTO ClubMember(family_member_id_fk, first_name, last_name, relationship_type, date_of_birth, height, weight, social_sec_number, med_card_number,
phone_number, address, city, province, postal_code)
VALUES
	(1, 'John', 'Doe', 'Mother', '2006-02-15', 175.00, 80.50, 123456789, 987654321, '5141234567', '123 Maple St', 'Montréal', 'QC', 'H2X1Y1'),
	(2, 'Jane', 'Doe', 'Mother', '2006-05-30', 160.00, 60.00, 234567890, 876543210, '5142345678', '456 Birch Rd', 'Montréal', 'QC', 'H3A2Z2'),
	(3, 'George', 'Smith', 'Grandfather', '2011-11-20', 180.00, 85.00, 345678901, 765432109, '4163456789', '789 Oak Ave', 'Toronto', 'ON', 'M5A3R2'),
	(4, 'Helen', 'Smith', 'Grandmother', '2013-07-12', 160.00, 55.00, 456789012, 654321098, '4164567890', '101 Pine Ln', 'Toronto', 'ON', 'M4B1Y4'),
	(5, 'Michael', 'Brown', 'Father', '2009-03-25', 178.00, 75.00, 567890123, 543210987, '6045678901', '202 Cedar Blvd', 'Vancouver', 'BC', 'V6B2Z5'),
	(6, 'Sarah', 'Brown', 'Mother', '2011-08-14', 165.00, 63.00, 678901234, 432109876, '6046789012', '303 Maple Rd', 'Vancouver', 'BC', 'V6C3X3'),
	(7, 'David', 'Williams', 'Grandfather', '2008-01-22', 182.00, 88.00, 789012345, 321098765, '6047890123', '404 Elm St', 'Vancouver', 'BC', 'V6E4J2'),
	(8, 'Linda', 'Williams', 'Grandmother', '2009-06-18', 158.00, 53.00, 890123456, 210987654, '6048901234', '505 Birch Blvd', 'Vancouver', 'BC', 'V6G5B1'),
	(9, 'Patrick', 'Johnson', 'Mother', '2009-09-10', 173.00, 70.00, 901234567, 109876543, '6131234567', '606 Pine Rd', 'Ottawa', 'ON', 'K1A0B1'),
	(10, 'Megan', 'Johnson', 'Mother', '2009-12-02', 162.00, 58.00, 123456788, 987654310, '6132345678', '707 Oak Ln', 'Ottawa', 'ON', 'K2P1Z4'),
	(11, 'Tim', 'Davis', 'Partner', '2009-04-06', 180.00, 85.00, 234567889, 876543211, '6042345678', '808 Cedar St', 'Vancouver', 'BC', 'V7Y1A2'),
	(12, 'Sophie', 'Davis', 'Partner', '2009-02-18', 167.00, 62.00, 345678890, 765432112, '6043456789', '909 Pine Blvd', 'Vancouver', 'BC', 'V7X2K3'),
	(13, 'Lucas', 'Martinez', 'Friend', '2010-07-11', 172.00, 68.00, 456789901, 654321113, '4165678901', '100 Oak Blvd', 'Toronto', 'ON', 'M5C2V3'),
	(14, 'Emma', 'Martinez', 'Friend', '2012-11-19', 160.00, 55.00, 567890012, 543210114, '4166789012', '110 Maple St', 'Toronto', 'ON', 'M4W3P2'),
	(15, 'Thomas', 'Garcia', 'Tutor', '2008-08-22', 178.00, 75.00, 678901123, 432109115, '6133456789', '120 Pine St', 'Ottawa', 'ON', 'K1N9L2'),
	(16, 'Rachel', 'Garcia', 'Tutor', '2008-12-04', 165.00, 60.00, 789012234, 321098116, '6134567890', '130 Cedar Blvd', 'Ottawa', 'ON', 'K1N8P1'),
	(17, 'Edward', 'King', 'Other', '2009-03-13', 175.00, 78.00, 890123345, 210987117, '5145678901', '140 Oak Rd', 'Montréal', 'QC', 'H3B2Y4'),
	(18, 'Barbara', 'King', 'Other', '2008-01-25', 168.00, 65.00, 901234456, 109876118, '5146789012', '150 Maple Ln', 'Montréal', 'QC', 'H4C1R3'),
	(19, 'Aaron', 'Lee', 'Mother', '2009-05-17', 182.00, 80.00, 123456799, 987654119, '9051234567', '160 Cedar Ave', 'Toronto', 'ON', 'M3B2N5'),
	(20, 'Jessica', 'Lee', 'Mother', '2009-10-30', 158.00, 55.00, 234567810, 876543120, '9052345678', '170 Oak Blvd', 'Toronto', 'ON', 'M4V1X2'),
	(1, 'Chris', 'Taylor', 'Friend', '2010-03-09', 170.00, 72.00, 345678921, 765432123, '4167890123', '180 Birch St', 'Toronto', 'ON', 'M5B3A1'),
	(2, 'Alice', 'Taylor', 'Friend', '2010-06-21', 165.00, 59.00, 456789032, 654321124, '4168901234', '190 Pine Ave', 'Toronto', 'ON', 'M5C2P4'),
	(3, 'Oscar', 'Green', 'Father', '2011-10-13', 179.00, 82.00, 567890143, 543210125, '6041234567', '200 Oak Blvd', 'Vancouver', 'BC', 'V6B4Y2'),
	(4, 'Martha', 'Green', 'Grandmother', '2010-03-09', 163.00, 50.00, 678901254, 432109126, '6042345678', '210 Birch Rd', 'Vancouver', 'BC', 'V6C5G1'),
	(5, 'Joshua', 'Harris', 'Partner', '2011-09-30', 175.00, 78.00, 789012365, 321098137, '5143456789', '220 Cedar Ave', 'Montréal', 'QC', 'H2Z3B2'),
	(6, 'Rachel', 'Harris', 'Mother', '2012-12-10', 167.00, 63.00, 890123476, 210987138, '5144567890', '230 Maple Rd', 'Montréal', 'QC', 'H3C4R4'),
	(7, 'Jack', 'Scott', 'Grandfather', '2013-02-26', 178.00, 85.00, 901234587, 109876149, '6135678901', '240 Pine Ln', 'Ottawa', 'ON', 'K2A0W4'),
	(8, 'Patricia', 'Scott', 'Grandmother', '2010-05-05', 160.00, 55.00, 123456598, 987654150, '6136789012', '250 Oak Rd', 'Ottawa', 'ON', 'K1Y2M1'),
	(9, 'Samuel', 'Miller', 'Partner', '2010-11-07', 180.00, 74.00, 234567609, 876543161, '4167890123', '260 Cedar Blvd', 'Toronto', 'ON', 'M6J3S5'),
	(10, 'Jessica', 'Miller', 'Partner', '2009-04-15', 169.00, 62.00, 345678710, 765432162, '4168901234', '270 Maple St', 'Toronto', 'ON', 'M5V2K4');

INSERT INTO Payment(member_id_fk, date, effective_date, amount, method, installment)
VALUES
	(1, '2024-02-15', '2025-01-01', 25.00, 'Credit', 1),
	(1, '2024-03-15', '2025-01-01', 25.00, 'Credit', 2),
	(1, '2024-04-15', '2025-01-01', 25.00, 'Credit', 3),
	(1, '2024-05-15', '2025-01-01', 25.00, 'Credit', 4),
	(2, '2024-06-10', '2025-01-01', 120.00, 'Debit', 1),
	(3, '2024-07-12', '2025-01-01', 45.00, 'Cash', 1),
	(3, '2024-08-12', '2025-01-01', 45.00, 'Cash', 2),
	(3, '2024-09-12', '2025-01-01', 10.00, 'Cash', 3),
	(4, '2024-10-20', '2025-01-01', 200.00, 'Cash', 1),
	(5, '2024-11-05', '2025-01-01', 75.00, 'Credit', 1),
	(5, '2024-12-05', '2025-01-01', 25.00, 'Credit', 2),
	(6, '2024-01-15', '2025-01-01', 50.00, 'Cash', 1),
	(6, '2024-02-15', '2025-01-01', 50.00, 'Cash', 2),
	(7, '2024-03-18', '2025-01-01', 30.00, 'Debit', 1),
	(7, '2024-04-18', '2025-01-01', 30.00, 'Debit', 2),
	(7, '2024-05-18', '2025-01-01', 40.00, 'Debit', 3),
	(8, '2024-06-22', '2025-01-01', 250.00, 'Cash', 1),
	(9, '2024-07-30', '2025-01-01', 100.00, 'Credit', 1),
	(10, '2024-08-15', '2025-01-01', 100.00, 'Cash', 1),
	(11, '2024-09-05', '2025-01-01', 60.00, 'Debit', 1),
	(11, '2024-10-05', '2025-01-01', 40.00, 'Debit', 2),
	(12, '2024-11-18', '2025-01-01', 300.00, 'Cash', 1),
	(13, '2024-12-10', '2025-01-01', 90.00, 'Credit', 1),
	(14, '2024-01-20', '2025-01-01', 10.00, 'Cash', 1),
	(14, '2024-02-20', '2025-01-01', 10.00, 'Cash', 2),
	(14, '2024-03-20', '2025-01-01', 10.00, 'Cash', 3),
	(14, '2024-04-20', '2025-01-01', 10.00, 'Cash', 4),
	(15, '2024-05-10', '2025-01-01', 200.00, 'Credit', 1),
	(16, '2024-06-12', '2025-01-01', 100.00, 'Debit', 1),
	(17, '2024-07-14', '2025-01-01', 15.00, 'Credit', 1),
	(17, '2024-08-14', '2025-01-01', 15.00, 'Credit', 2),
	(17, '2024-09-14', '2025-01-01', 20.00, 'Credit', 3),
	(17, '2024-10-14', '2025-01-01', 50.00, 'Credit', 4),
	(18, '2024-11-25', '2025-01-01', 100.00, 'Cash', 1),
	(19, '2024-12-30', '2025-01-01', 300.00, 'Debit', 1),
	(20, '2024-01-02', '2025-01-01', 80.00, 'Debit', 1),
	(20, '2024-02-02', '2025-01-01', 20.00, 'Debit', 2),
	(21, '2024-03-09', '2025-01-01', 150.00, 'Credit', 1),
	(22, '2024-04-22', '2025-01-01', 200.00, 'Cash', 1),
	(23, '2024-05-05', '2025-01-01', 10.00, 'Cash', 1),
	(23, '2024-06-05', '2025-01-01', 10.00, 'Cash', 2),
	(23, '2024-07-05', '2025-01-01', 20.00, 'Cash', 3),
	(23, '2024-08-05', '2025-01-01', 60.00, 'Cash', 4),
	(24, '2024-09-15', '2025-01-01', 100.00, 'Credit', 1),
	(25, '2024-10-20', '2025-01-01', 180.00, 'Debit', 1),
	(26, '2024-11-11', '2025-01-01', 300.00, 'Debit', 1),
	(27, '2024-12-08', '2025-01-01', 25.00, 'Cash', 1),
	(27, '2024-01-08', '2025-01-01', 25.00, 'Cash', 2),
	(27, '2024-02-08', '2025-01-01', 25.00, 'Cash', 3),
	(27, '2024-03-08', '2025-01-01', 25.00, 'Cash', 4);