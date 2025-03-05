CREATE DATABASE IF NOT EXISTS WarmUpProject;

CREATE TABLE Personnel(
	personnel_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    social_sec_number INT NOT NULL UNIQUE,
    med_card_number INT NOT NULL UNIQUE,
    phone_number CHAR(10),
    address VARCHAR(50),
    city VARCHAR(50),
    province VARCHAR(2),
    postal_code VARCHAR(6),
    email_address VARCHAR(50),
    role VARCHAR(50),
    mandate VARCHAR(50)
);

CREATE TABLE Location(
	location_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50),
    city VARCHAR(50),
    postal_code VARCHAR(6),
    type VARCHAR(50),
    address VARCHAR(100),
    province VARCHAR(2),
    capacity INT,
    website VARCHAR(50)
);

CREATE TABLE LocationPhone(
	location_id_fk INT,
    phone_number CHAR(10),
    PRIMARY KEY(location_id_fk, phone_number),
    FOREIGN KEY(location_id_fk) REFERENCES Location(location_id)
);

CREATE TABLE PersonnelLocationDate(
	personnel_id_fk INT,
    location_id_fk INT,
    start_date DATE,
    end_date DATE,
    PRIMARY KEY (personnel_id_fk, location_id_fk, start_date),
    FOREIGN KEY(personnel_id_fk) REFERENCES Personnel(personnel_id),
    FOREIGN KEY(location_id_fk) REFERENCES Location(location_id)
);

CREATE TABLE FamilyMember(
	family_member_id int PRIMARY KEY AUTO_INCREMENT,
	first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth date,
    social_sec_number INT NOT NULL UNIQUE,
    med_card_number INT NOT NULL UNIQUE,
    phone_number char(10),
    address VARCHAR(100),
    city VARCHAR(50),
    province VARCHAR(2),
    postal_code VARCHAR(6),
    email_address VARCHAR(50),
    location_id_fK INT,
    FOREIGN KEY(location_id_fK) REFERENCES Location(location_id)
);

CREATE TABLE ClubMember(
	member_id int PRIMARY KEY AUTO_INCREMENT,
    family_member_id_fk INT,
    relationship_type VARCHAR(20),
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    height FLOAT(5,2),
    weight FLOAT(5,2),
    social_sec_number INT NOT NULL UNIQUE,
    med_card_number INT NOT NULL UNIQUE,
    phone_number CHAR(10),
    address VARCHAR(100),
    city VARCHAR(50),
    province VARCHAR(2),
    postal_code VARCHAR(6),
    join_date DATE,
    progress_report TEXT,
    FOREIGN KEY(family_member_id_fk) REFERENCES FamilyMember(family_member_id)
);

CREATE TABLE Payment(
	payment_id int PRIMARY KEY AUTO_INCREMENT,
    member_id_fk INT,
    date DATE,
    amount FLOAT(5,2),
    method VARCHAR(10),
    effective_date DATE,
    installment INT,
    FOREIGN KEY(member_id_fk) REFERENCES ClubMember(member_id)
);

-- Query 12: Get a report on all active club members who have never been assigned to any formation team session.
-- 				The list should include the club member’s membership number, first name, last name, age, date of joining the club, phone number, email and current location name.
-- 				The results should be displayed sorted in ascending order by location name then by club membership number.

SELECT 
	CM.cmn AS 'MembershipNumber',
    CM.first_name AS 'MemberFirstName',
    CM.last_name AS 'MemberLastName',
    year(now()) - year(CM.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(CM.dob, '%m%d')) AS 'Age',
    (
		SELECT MAX(paymentDate)
		FROM Payment
        WHERE cmn_fk = CM.cmn
        GROUP BY effectiveDate
        ORDER BY effectiveDate
        LIMIT 1
    ) AS 'JoinDate',
    CM.phone_number AS 'MemberPhone',
    -- email,
    (
		SELECT GROUP_CONCAT(L.name ORDER BY L.name SEPARATOR ', ')
        FROM Location L
        JOIN FamilyMemberLocation FML ON L.id = FML.location_id_fk
        WHERE FML.family_member_id_fk = CM.family_member_id_fk
        GROUP BY FML.family_member_id_fk
    ) AS 'LocationNames'
FROM ClubMember CM
LEFT JOIN TeamMember TM ON CM.cmn = TM.cmn_fk
WHERE CM.is_active = 1
HAVING COUNT(TM.cmn_fk) = 0
ORDER BY LocationNames, CM.cmn;
