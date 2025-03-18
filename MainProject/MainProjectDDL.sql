CREATE DATABASE IF NOT EXISTS MainProject;

CREATE TABLE Location 
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    type varchar(10),
    name VARCHAR(50),
    postal_code VARCHAR(6),
    province CHAR(2),
    address VARCHAR(255),
    city VARCHAR(50),
    website_url VARCHAR(255),
    capacity INT
);
    
CREATE TABLE LocationPhone 
(
	location_id_fk INT,
    phone_number CHAR(10)
);

CREATE TABLE Personnel
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    dob DATE,
    social_sec_num CHAR(9) NOT NULL UNIQUE,
    med_card_num CHAR(12) UNIQUE,
    phone_number CHAR(10),
    city VARCHAR(50),
    province CHAR(2),
    postal_code CHAR(6),
    email VARCHAR(50),
    mandate VARCHAR(10)
);

CREATE TABLE PersonnelLocation
(
	personnel_id_fk INT,
    location_id_fk INT,
    start_date DATE,
    end_date DATE,
    role VARCHAR(50),
    PRIMARY KEY (personnel_id_fk, location_id_fk),
    FOREIGN KEY (personnel_id_fk) REFERENCES Personnel(id),
    FOREIGN KEY (location_id_fk) REFERENCES Location(id)
);

CREATE TABLE FamilyMember
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    dob DATE,
    email VARCHAR(50),
    social_sec_num CHAR(9) NOT NULL UNIQUE,
    med_card_num CHAR(12) UNIQUE,
    city VARCHAR(50),
    province VARCHAR(2),
    phone_number CHAR(10),
    postal_code CHAR(6)
);

CREATE TABLE FamilyMemberLocation
(
	location_id_fk INT,
    family_member_id_fk INT,
    start_date DATE,
    end_date DATE,
    FOREIGN KEY(location_id_fk) REFERENCES Location(id),
    FOREIGN KEY(family_member_id_fk) REFERENCES FamilyMember(id),
    PRIMARY KEY(location_id_fk, family_member_id_fk, start_date)
);

CREATE TABLE SecondaryFamilyMember
(
	primary_family_member_id_fk INT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    phone_number CHAR(10),
    relationship_to_primary VARCHAR(20),
    PRIMARY KEY(primary_family_member_id_fk, first_name, last_name),
    FOREIGN KEY(primary_family_member_id_fk) REFERENCES FamilyMember(id)
);

CREATE TABLE ClubMember
(
	cmn INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    dob DATE,
    email VARCHAR(50),
    height DECIMAL(5,2),
    weight DECIMAL(5,2),
    social_sec_num CHAR(9) NOT NULL UNIQUE,
    med_card_num CHAR(12) UNIQUE,
    phone_number CHAR(10),
    city VARCHAR(50),
    province VARCHAR(2),
    postal_code CHAR(6),
    address VARCHAR(255),
    progress_report TEXT NOT NULL,
    is_active BOOLEAN,
    family_member_id_fk INT,
    primary_relationship VARCHAR(20) NOT NULL,
    secondary_relationship VARCHAR(20),
    CONSTRAINT CHECK (primary_relationship IN ('Father', 'Mother', 'Grandfather', 'Grandmother', 'Uncle', 'Aunt', 'Tutor', 'Partner', 'Friend', 'Other')),
    CONSTRAINT CHECK (secondary_relationship IN ('Father', 'Mother', 'Grandfather', 'Grandmother', 'Uncle', 'Aunt', 'Tutor', 'Partner', 'Friend', 'Other'))
    FOREIGN KEY (family_member_id_fk) REFERENCES FamilyMember(id)
);

DELIMITER //
CREATE TRIGGER validate_club_member
BEFORE INSERT ON ClubMember FOR EACH ROW
BEGIN
	DECLARE locationID INT;
    DECLARE iterationCount INT;
    DECLARE currentCount INT DEFAULT 0;
    
    SET locationID = (SELECT location_id_fk FROM FamilyMemberLocation WHERE family_member_id_fk = NEW.family_member_id_fk AND end_date IS NULL);
    
    SET iterationCount = (SELECT COUNT(1) FROM locationID);

	IF (year(now()) - year(NEW.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(NEW.dob, '%m%d')) < 11 OR
		year(now()) - year(NEW.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(NEW.dob, '%m%d')) >= 18) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[ClubMember]: Age must be >= 11 and < 18';
	END IF;
    
    WHILE currentCount < iterationCount DO
    BEGIN
		IF (SELECT SUM(ClubMemberCount) + 1 FROM (
													SELECT COUNT(1) AS ClubMemberCount FROM ClubMember 
													WHERE family_member_id_fk = NEW.family_member_id_fk AND is_active = 1
													UNION ALL
													SELECT COUNT(CM.cmn) AS ClubMemberCount FROM FamilyMemberLocation FML
													JOIN FamilyMember FM ON FML.family_member_id_fk = FM.family_member_id_fk AND FML.location_id_fk = locationID
													JOIN ClubMember CM ON FM.id = CM.family_member_id_fk AND CM.is_active = 1
													GROUP BY FML.location_id_fk
													LIMIT 1 OFFSET currentCount
													) AS LocationMemberCount
				) > (
						SELECT capacity FROM Location 
						WHERE id = locationID
                        LIMIT 1 OFFSET currentCount
				) THEN
			SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[ClubMember]: A location the respective family member is currently at is full';
		END IF;
        
        SET currentCount = currentCount + 1;
	END WHILE;
END //

CREATE TABLE ClubMemberLocation
(
	location_id_fk INT,
    cmn_fk INT,
    start_date DATE,
    end_date DATE,
    FOREIGN KEY(location_id_fk) REFERENCES Location(id),
    FOREIGN KEY(cmn_fk) REFERENCES ClubMember(cmn),
    PRIMARY KEY(location_id_fk, cmn_fk, start_date)
);

CREATE TABLE Payment
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    amount DECIMAL(5, 2),
    paymentDate DATE,
    effectiveDate DATE,
    method VARCHAR(10),
    cmn_fk INT,
    FOREIGN KEY(cmn_fk) REFERENCES ClubMember(cmn)
);

CREATE TABLE TeamFormation
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50),
    captain_id_fk INT,
    location_id_fk INT,
    FOREIGN KEY(captain_id_fk) REFERENCES ClubMember(cmn),
    FOREIGN KEY(location_id_fk) REFERENCES Location(id)
);

CREATE TABLE TeamMember
(
	team_formation_id_fk INT,
    cmn_fk INT,
    role VARCHAR(50),
    assignment_date_time DATETIME,
    PRIMARY KEY(team_formation_id_fk, cmn_fk),
    FOREIGN KEY(team_formation_id_fk) REFERENCES TeamFormation(id),
    FOREIGN KEY(cmn_fk) REFERENCES ClubMember(cmn)
);

CREATE TABLE Session
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    event_type VARCHAR(20),
    event_date_time DATETIME,
    location_id_fk INT,
    FOREIGN KEY(location_id_fk) REFERENCES Location(id)
);

CREATE TABLE TeamSession
(
	team_formation_id_fk INT,
    session_id_fk INT,
    score INT,
    PRIMARY KEY(team_formation_id_fk, session_id_fk),
    FOREIGN KEY(team_formation_id_fk) REFERENCES TeamFormation(id),
    FOREIGN KEY(session_id_fk) REFERENCES Session(id)
);

CREATE TABLE LogEmail
(
	recipient VARCHAR(50),
    delivery_date_time VARCHAR(50),
    sender VARCHAR(50),
    subject VARCHAR(255),
    body TEXT,
    PRIMARY KEY(recipient, delivery_date_time)
);

