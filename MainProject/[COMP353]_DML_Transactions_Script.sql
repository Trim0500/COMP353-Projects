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
