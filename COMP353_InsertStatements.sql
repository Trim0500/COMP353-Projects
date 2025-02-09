INSERT INTO Location(location_id, name, city, postal_code, type, address, province, capacity, website)
VALUES
	(1, "MYVC Pierrefonds", "Montréal", "H9H4V6", "Branch", "14895 Pierrefonds Boul", "QC", 250, "myvc.ca/location/pierrefonds"),
    (2, "MYVC DDO", "Montréal", "H9B1Z6", "Branch", "3237 Des Sources Blvd", "QC", 200, "myvc.ca/location/ddo"),
    (3, "MYVC Centre-Ville", "Montréal", "H3G2H7","Head", "1515 Rue Ste-Catherine W", "QC", 300, "myvc.ca/location/centreville"),
    (4, "MYVC Brossard", "Montréal", "J4W1M9", "Branch", "7250 Taschereau Blvd", "QC", 275, "myvc.ca/location/brossard"),
    (5, "MYVC Toronto", "Toronto", "M1L2L1", "Branch", "1880 Eglinton Ave E", "ON", 400, "myvc.ca/location/toronto"),
    (6, "MYVC Brampton", "Toronto", "L6Y1N7", "Branch", "499 Main Street South", "ON", 300, "myvc.ca/location/brampton"),
    (7, "MYVC Ottawa", "Ottawa", "L2H2E9", "Branch", "7555 Montrose Road", "ON", 225, "myvc.ca/location/ottawa"),
    (8, "MYVC Victoria", "Victoria", "V8W1E5", "Branch", "851 Broughton St", "BC", 200, "myvc.ca/location/victoria"),
    (9, "MYVC Vancouver", "Vancouver", "V5Z3X7", "Branch", "555 W 12th Ave", "BC", 215, "myvc.ca/location/vancouver"),
    (10, "MYVC Surrey", "Vancouver", "V3T2X3", "Branch", "10642 King George Blvd", "BC", 290, "myvc.ca/location/surrey");

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
    
INSERT INTO Personnel(personnel_id, first_name, last_name, date_of_birth,
social_sec_number, med_card_number, phone_number, address, city, province, postal_code, email_address, role,
mandate)
VALUES 
(1, "Ana", "Amari", "1978-05-03", 184817362, 482163928, "5143729845", "1825 Saint-Denis St", "Montréal", "QC", "H2X3K4", "aamari@gmail.com", "Captain", "Treasurer"),
(2, "Elizabeth", "Caledonia", "1995-06-17", 284721039, 183614736, "4387781256", "6780 Sherbrooke St W", "Montréal", "QC", "H4B1P8", "ecaledonia@gmail.com", "General Manager", "Salaried"),
(3, "Jean-Baptiste", "Augustin", "1992-08-25", 582947182, 193753713, "5146498802", "9122 Wellington St", "Montréal", "QC", "H4G1X3", "jbaugustin@gmail.com", "Deputy Manager", "Salaried"),
(4, "Brigitte", "Lindholm", "1995-06-17", 119847274, 189371731, "5143929345", "3425 Park Ave", "Montréal", "QC", "H2X2H6", "blindholm@gmail.com", "Secretary", "Salaried"),
(5, "Jesse", "McCree", "1990-11-05", 817462648, 182373611, "4382783256", "12 Crescent St", "Montréal", "QC", "H2X1H5", "jmccree@gmail.com", "Administrator", "Salaried"),
(6, "Hana", "Song", "1998-01-15", 173649123, 111111111, "5145923467", "2948 St Catherine St", "Montréal", "QC", "H4W1T5", "hsong@gmail.com", "Assistant Coach", "Volunteer"),
(7, "Akande", "Ogundimu", "1980-12-30", 198471637, 293481828, "4385923467", "1234 Rue Sainte-Catherine Ouest", "Montréal", "QC", "H4W1A5", "aogundimu@gmail.com", "Manager", "Salaried"),
(8, "Genji", "Shimada", "1998-01-15", 837467122, 192784111, "5147411802", "7788 Rue Jean-Talon Est", "Montréal", "QC", "H1S1K9", "gshimada@gmail.com", "Coach", "Salaried"),
(9, "Hanzo", "Shimada", "1978-02-11", 184736182, 991847326, "4382256229", "5678 Boulevard Saint-Laurent", "Montréal", "QC", "H2T1S8", "hshimada@gmail.com", "Manager", "Salaried"),
(10, "Illari", "Ruiz", "1985-03-13", 124918463, 185381737, "4381994822", "9101 Avenue du Parc", "Montréal", "QC", "H2N1Y7", "iruiz@gmail.com", "Coach", "Volunteer"),
(11, "Odessa", "Stone", "1977-12-12", 18383182, 183843000, "5149293838", "2345 Rue Sherbrooke Est", "Montréal", "QC", "H1V3G8", "ostone@gmail.com", "Manager", "Salaried"),
(12, "Jamison", "Fawkes", "1993-03-02", 948282182, 134432191, "5141221299", "6789 Boulevard Pie-IX", "Montréal", "QC", "H2C3Y1", "jfawkes@gmail.com", "Coach", "Salaried"),
(13, "Teo", "Minh", "1997-04-12", 188463817, 582753811, "4165923467", "123 Yonge Street", "Toronto", "ON", "M5C1W4", "tminh@gmail.com", "Manager", "Volunteer"),
(14, "Kiriko", "Yamagami", "1989-04-04", 948284644, 857173111, "416991334", "456 Bloor Street West", "Toronto", "ON" , "M5S1X8", "kyamagami@gmail.com", "Coach", "Salaried"),
(15, "Niran", "Pruksamanee", "1979-05-12", 194858194, 591848392, "4163859192", "789 Bay Street", "Toronto", "ON", "M5G2C8", "npruksamanee@gmail.com", "Assistant Coach", "Salaried"),
(16, "Lucio Correia", "dos Santos", "1997-04-09", 818433919, 473817837, "4160100303", "1011 Queen Street East", "Toronto", "ON", "M4M1K3", "ldossantos@gmail.com", "Manager", "Salaried"),
(17, "Mauga", "Ho'okano", "1987-02-21", 736481928, 847626333, "4160019488", "3498 Dundas Street West", "Toronto", "ON", "M6P1Y2", "mhookano@gmail.com", "Coach", "Volunteer"),
(18, "Mei-Ling", "Zhou", "1997-02-12", 728472811, 947190483, "4168183737", "6789 Finch Avenue East", "Toronto", "ON", "M1B1T1", "mzhou@gmail.com", "Administrator", "Salaried"),
(19, "Angela", "Ziegler", "1986-02-08", 958929485, 195838929, "3436461828", "100 Wellington Street", "Ottawa", "ON", "K1A0A6", "aziegler@gmail.com", "Manager", "Salaried"),
(20, "Moira", "O'Deorain", "1969-02-06", 737481737, 814784777, "6138580393", "230 Bank Street", "Ottawa", "ON", "K1M5M1", "modeorain@gmail.com", "Assistant Coach", "Volunteer"),
(21, "Orisa", "Oladele", "1999-10-10", 782389541, 858585929, "3439948575", "780 Rideau Street", "Ottawa", "ON", "K2P1X4", "ooladele@gmail.com", "Captain", "Salaried"),
(22, "Fareeha", "Amari", "1991-08-29", 746182833, 815473722, "7780203931", "737 Government Street", "Victoria", "BC", "VLV2L2", "famari@gmail.com", "Manager", "Salaried"),
(23, "Ramattra", "Tekhartha", "1990-04-26", 958385418, 905837261, "7788382812", "948 Douglas Street", "Victoria", "BC", "V8Y2P7", "rtekhartha@gmail.com", "Coach", "Volunteer"),
(24, "Mako", "Rutledge", "1984-10-30", 828481848, 717483812, "7781113456", "544 Fort Street", "Victoria", "BC", "V8W1P9", "mrutledge@gmail.com", "Administrator", "Salaried"),
(25, "Siebren", "de Kuiper", "1967-11-11", 184756381, 195747184, "6041938457", "384 Granville Street", "Vancouver", "BC", "V9Y4E3", "sdekuiper@gmail.com", "Manager", "Volunteer"),
(26, "Vivian", "Chase", "1979-04-06", 848184818, 192548291, "6049184818", "900 Broadway Street", "Vancouver", "BC", "V7A9R4", "vchase@gmail.com", "Coach", "Salaried"),
(27, "Jack", "Morrison", "1965-09-03", 537628374, 184757381, "6041138858", "650 Cambie Street", "Vancouver", "BC", "V9Y2E1", "jmorrison@gmail.com", "Administrator", "Salaried"),
(28, "Olivia", "Colomar", "1999-12-06", 904929474, 585858111, "6049482838", "7219 Main Street", "Vancouver", "BC", "V7C1Y7", "ocolomar@gmail.com", "Manager", "Salaried"),
(29, "Satya", "Vaswani", "2000-10-02", 384172444, 123476533, "6040103949", "5667 Scott Road", "Vancouver", "BC", "V3W5M8", "svaswani@gmail.com", "Coach", "Volunteer"),
(30, "Torbjörn", "Lindholm", "1967-01-14", 163749271, 195473311, "6044329191", "19 Westminster", "Vancouver", "BC", "V3J16X", "tlindholm@gmail.com", "Assistant Coach", "Salaried");
select * from Personnel;