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
    
    

	