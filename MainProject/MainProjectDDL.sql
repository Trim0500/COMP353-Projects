CREATE DATABASE IF NOT EXISTS MainProject;

CREATE TABLE Location 
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    type varchar(10),
    name VARCHAR(50),
    postal_code CHAR(6),
    province CHAR(2),
    address VARCHAR(255),
    city VARCHAR(50),
    website_url VARCHAR(255),
    capacity INT,
    CONSTRAINT CHECK (type IN ('Head','Branch'))
);
    
CREATE TABLE LocationPhone 
(
	location_id_fk INT,
    phone_number CHAR(10),
    FOREIGN KEY(location_id_fk) REFERENCES Location(id) ON DELETE CASCADE,
    PRIMARY KEY(location_id_fk, phone_number)
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
    mandate VARCHAR(10),
    CONSTRAINT CHECK (mandate IN ('Paid','Volunteer'))
);

CREATE TABLE PersonnelLocation
(
	personnel_id_fk INT,
    location_id_fk INT,
    start_date DATE,
    end_date DATE,
    role VARCHAR(50),
    PRIMARY KEY (personnel_id_fk, location_id_fk, start_date),
    FOREIGN KEY (personnel_id_fk) REFERENCES Personnel(id) ON DELETE CASCADE,
    FOREIGN KEY (location_id_fk) REFERENCES Location(id) ON DELETE CASCADE
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
    FOREIGN KEY(location_id_fk) REFERENCES Location(id) ON DELETE CASCADE,
    FOREIGN KEY(family_member_id_fk) REFERENCES FamilyMember(id) ON DELETE CASCADE,
    PRIMARY KEY(location_id_fk, family_member_id_fk, start_date)
);

CREATE TABLE SecondaryFamilyMember
(
	primary_family_member_id_fk INT UNIQUE,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    phone_number CHAR(10),
    relationship_to_primary VARCHAR(20),
    PRIMARY KEY(primary_family_member_id_fk),
    FOREIGN KEY(primary_family_member_id_fk) REFERENCES FamilyMember(id) ON DELETE CASCADE
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
    progress_report TEXT,
    is_active BOOLEAN NOT NULL,
    family_member_id_fk INT,
    primary_relationship VARCHAR(20) NOT NULL,
    secondary_relationship VARCHAR(20),
    gender CHAR(1) NOT NULL,
    FOREIGN KEY (family_member_id_fk) REFERENCES FamilyMember(id) ON DELETE CASCADE,
    CONSTRAINT CHECK (primary_relationship IN ('Father', 'Mother', 'Grandfather', 'Grandmother', 'Uncle', 'Aunt', 'Tutor', 'Partner', 'Friend', 'Other')),
    CONSTRAINT CHECK (secondary_relationship IN ('Father', 'Mother', 'Grandfather', 'Grandmother', 'Uncle', 'Aunt', 'Tutor', 'Partner', 'Friend', 'Other')),
    CONSTRAINT CHECK (gender IN ('M', 'F'))
);

CREATE TABLE Payment
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    amount DECIMAL(5, 2),
    paymentDate DATE,
    effectiveDate DATE,
    method VARCHAR(10),
    cmn_fk INT,
    FOREIGN KEY(cmn_fk) REFERENCES ClubMember(cmn) ON DELETE CASCADE,
    CONSTRAINT CHECK (method IN ('Cash','Debit','Credit'))
);

CREATE TABLE TeamFormation
(
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50),
    captain_id_fk INT,
    location_id_fk INT,
    FOREIGN KEY(captain_id_fk) REFERENCES ClubMember(cmn) ON DELETE CASCADE,
    FOREIGN KEY(location_id_fk) REFERENCES Location(id) ON DELETE CASCADE
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
    FOREIGN KEY(location_id_fk) REFERENCES Location(id) ON DELETE CASCADE,
    CONSTRAINT CHECK (event_type IN ('Game','Training'))
);

CREATE TABLE TeamSession
(
	team_formation_id_fk INT,
    session_id_fk INT,
    score INT,
    PRIMARY KEY(team_formation_id_fk, session_id_fk),
    FOREIGN KEY(team_formation_id_fk) REFERENCES TeamFormation(id) ON DELETE CASCADE,
    FOREIGN KEY(session_id_fk) REFERENCES Session(id) ON DELETE CASCADE,
    CONSTRAINT CHECK (score >= 0 AND score <= 100)
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

DELIMITER //
CREATE TRIGGER validate_location_insert
BEFORE INSERT ON Location FOR EACH ROW
BEGIN
	IF NEW.type = ANY (SELECT type FROM Location WHERE type = 'Head') THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Location]: A head location already exists, enter a new branch';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_location_update
BEFORE UPDATE ON Location FOR EACH ROW
BEGIN
	IF NEW.type != OLD.type AND NEW.type = ANY (SELECT type FROM Location WHERE type = 'Head') THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Location]: A head location already exists, enter a new branch';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_location_phone_insert
BEFORE INSERT ON LocationPhone FOR EACH ROW
BEGIN
	IF (NEW.location_id_fk,NEW.phone_number) = ANY (SELECT * FROM LocationPhone) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[LocationPhone]: This phone number was already assigned to this location, choose another phone number or change the location';
	ELSEIF NEW.location_id_fk != ANY (SELECT location_id_fk FROM LocationPhone WHERE phone_number = NEW.phone_number) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[LocationPhone]: Cannot enter the same phone number at diffeent locations';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_location_phone_update
BEFORE UPDATE ON LocationPhone FOR EACH ROW
BEGIN
	IF (NEW.location_id_fk,NEW.phone_number) != (OLD.location_id_fk,OLD.phone_number) AND (NEW.location_id_fk,NEW.phone_number) = ANY (SELECT * FROM LocationPhone) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[LocationPhone]: This phone number was already assigned to this location, choose another phone number or change the location';
	ELSEIF NEW.location_id_fk != OLD.location_id_fk AND NEW.location_id_fk != ANY (SELECT location_id_fk FROM LocationPhone WHERE phone_number = NEW.phone_number) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[LocationPhone]: Cannot enter the same phone number at diffeent locations';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_personnel_location_insert
BEFORE INSERT ON PersonnelLocation FOR EACH ROW
BEGIN
	IF (NEW.role NOT IN ('General Manager','Deputy Manager','Treasurer','Secretary','Admin','Coach','Assistant Coach') AND
		(SELECT type FROM Location WHERE id = NEW.location_id_fk) = 'Head') OR
        (NEW.role NOT IN ('Manager','Treasurer','Coach','Assistant Coach','Other') AND
		(SELECT type FROM Location WHERE id = NEW.location_id_fk) = 'Branch') THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[PersonnelLocation]: Unkown role for the given location';
	ELSEIF NEW.role IN (SELECT role FROM PersonnelLocation WHERE role = 'General Manager' AND end_date IS NULL) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[PersonnelLocation]: General Manager already exists, change the role';
	ELSEIF NEW.role IN (SELECT role FROM PersonnelLocation WHERE role = 'Manager' AND location_id_fk = NEW.location_id_fk AND end_date IS NULL) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[PersonnelLocation]: Manager for this location already exists, change the location or the role';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_personnel_location_update
BEFORE UPDATE ON PersonnelLocation FOR EACH ROW
BEGIN
	IF (NEW.role NOT IN ('General Manager','Deputy Manager','Treasurer','Secretary','Admin','Coach','Assistant Coach') AND
		(SELECT type FROM Location WHERE id = NEW.location_id_fk) = 'Head') OR
        (NEW.role NOT IN ('Manager','Treasurer','Coach','Assistant Coach','Other') AND
		(SELECT type FROM Location WHERE id = NEW.location_id_fk) = 'Branch') THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[PersonnelLocation]: Unkown role for the given location';
	ELSEIF NEW.role != OLD.role AND NEW.role IN (SELECT role FROM PersonnelLocation WHERE role = 'General Manager' AND end_date IS NULL) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[PersonnelLocation]: General Manager already exists, change the role';
	ELSEIF NEW.role != OLD.role AND NEW.role IN (SELECT role FROM PersonnelLocation WHERE role = 'Manager' AND location_id_fk = NEW.location_id_fk AND end_date IS NULL) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[PersonnelLocation]: Manager for this location already exists, change the location or the role';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_club_member_insert
BEFORE INSERT ON ClubMember FOR EACH ROW
BEGIN
	DECLARE age INT;
    
    SET age = year(now()) - year(NEW.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(NEW.dob, '%m%d'));

	IF (age < 11 OR age >= 18) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[ClubMember]: Age must be >= 11 and < 18';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_club_member_update
BEFORE UPDATE ON ClubMember FOR EACH ROW
BEGIN
	DECLARE age INT;
    
    SET age = year(now()) - year(NEW.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(NEW.dob, '%m%d'));

	IF (age < 11 OR age >= 18) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[ClubMember]: Age must be >= 11 and < 18';
	END IF;
END //
	
DELIMITER //
CREATE TRIGGER validate_session_insert
BEFORE INSERT ON Session FOR EACH ROW
BEGIN
	IF (NEW.event_type,NEW.event_date_time,NEW.location_id_fk) IN (SELECT event_type, event_date_time, location_id_fk FROM Session) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Session]: Cannot add in the same event at the specified location, change the values';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_session_update
BEFORE UPDATE ON Session FOR EACH ROW
BEGIN
	IF (NEW.event_type,NEW.event_date_time,NEW.location_id_fk) IN (SELECT event_type, event_date_time, location_id_fk FROM Session) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Session]: Cannot add in the same event at the specified location, change the values';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_payment_insert
BEFORE INSERT ON Payment FOR EACH ROW
BEGIN
	IF year(NEW.effectiveDate) < year(NEW.paymentDate) OR year(NEW.effectiveDate) - year(NEW.paymentDate) > 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Payment]: The selected effective year is invalid, make sure that the effective year is the same or next immediate year';
	ELSEIF (
			SELECT SUM(amount) + NEW.amount FROM Payment
            WHERE cmn_fk = NEW.cmn_fk AND effectiveDate = NEW.effectiveDate
            GROUP BY cmn_fk, effectiveDate
            HAVING COUNT(paymentDate) = 3
			) < 100 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Payment]: The last payment installment for this user and membership year does not reach 100, raise the amount';
    ELSEIF (SELECT COUNT(1) FROM Payment WHERE cmn_fk = NEW.cmn_fk AND effectiveDate = NEW.effectiveDate) = 4 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Payment]: This user has already made all the payments they need';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_payment_update
BEFORE UPDATE ON Payment FOR EACH ROW
BEGIN
	IF (NEW.effectiveDate != OLD.effectiveDate OR NEW.paymentDate != OLD.paymentDate) AND (year(NEW.effectiveDate) < year(NEW.paymentDate) OR year(NEW.effectiveDate) - year(NEW.paymentDate) > 1) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[Payment]: The selected effective year is invalid, make sure that the effective year is the same or next immediate year';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_team_member_insert
BEFORE INSERT ON TeamMember
FOR EACH ROW
BEGIN
    DECLARE num_members INT;
    DECLARE team_gender CHAR(1);
    DECLARE new_member_gender CHAR(1);
    DECLARE recent_assignment_exists INT;
    
    -- Validate team is not full (8 players maximum)
    SELECT COUNT(*) INTO num_members
    FROM TeamMember
    WHERE team_formation_id_fk = NEW.team_formation_id_fk;
    IF num_members >= 8 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Team is full.';
    END IF;
    
    SELECT cm.gender INTO new_member_gender
    FROM ClubMember cm
    WHERE cm.cmn = NEW.cmn_fk;
    
    -- Validate new team member references an existing club member
    IF new_member_gender IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Club member does not exist and thus cannot be assigned to a team.';
    END IF;
    
    SELECT cm.gender INTO team_gender
    FROM TeamMember tm
    JOIN ClubMember cm ON tm.cmn_fk = cm.cmn
    WHERE tm.team_formation_id_fk = NEW.team_formation_id_fk
    LIMIT 1;
    
    -- Validate new member gender matches the gender of the team
    IF team_gender IS NOT NULL AND team_gender <> new_member_gender THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Gender mismatch, cannot add member.';
    END IF;
    
    -- Validate new member is not already on another team
    SELECT COUNT(*) INTO recent_assignment_exists
    FROM TeamMember
    WHERE cmn_fk = NEW.cmn_fk
    AND assignment_date_time >= NOW() - INTERVAL 3 HOUR;
    IF recent_assignment_exists > 0 THEN
	SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Member is already on a team. Please wait until 3 hours have elapsed before assigning to a new team.';
    END IF;
END; //

DELIMITER //
CREATE TRIGGER validate_team_member_update
BEFORE UPDATE ON TeamMember
FOR EACH ROW
BEGIN
    DECLARE num_members INT;
    DECLARE team_gender CHAR(1);
    DECLARE new_member_gender CHAR(1);
    DECLARE recent_assignment_exists INT;
    
    -- Validate team is not full (8 players maximum)
    SELECT COUNT(*) INTO num_members
    FROM TeamMember
    WHERE team_formation_id_fk = NEW.team_formation_id_fk;
    IF NEW.team_formation_id_fk != OLD.team_formation_id_fk AND num_members >= 8 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Team is full.';
    END IF;
    
    SELECT cm.gender INTO new_member_gender
    FROM ClubMember cm
    WHERE cm.cmn = NEW.cmn_fk;
    
    -- Validate new team member references an existing club member
    IF NEW.cmn_fk != OLD.cmn_fk AND new_member_gender IS NULL THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Club member does not exist and thus cannot be assigned to a team.';
    END IF;
    
    SELECT cm.gender INTO team_gender
    FROM TeamMember tm
    JOIN ClubMember cm ON tm.cmn_fk = cm.cmn
    WHERE tm.team_formation_id_fk = NEW.team_formation_id_fk
    LIMIT 1;
    
    -- Validate new member gender matches the gender of the team
    IF NEW.team_formation_id_fk != OLD.team_formation_id_fk AND team_gender IS NOT NULL AND team_gender <> new_member_gender THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Gender mismatch, cannot add member.';
    END IF;
    
    -- Validate new member is not already on another team
    SELECT COUNT(*) INTO recent_assignment_exists
    FROM TeamMember
    WHERE cmn_fk = NEW.cmn_fk
    AND assignment_date_time >= NOW() - INTERVAL 3 HOUR;
    IF NEW.cmn_fk != OLD.cmn_fk AND recent_assignment_exists > 0 THEN
	SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = '[TeamMember]: Member is already on a team. Please wait until 3 hours have elapsed before assigning to a new team.';
    END IF;
END; //
	
DELIMITER ;

DELIMITER //
CREATE TRIGGER validate_team_session_insert
BEFORE INSERT ON TeamSession FOR EACH ROW
BEGIN
	IF (SELECT COUNT(*) FROM TeamSession WHERE session_id_fk = NEW.session_id_fk) > 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[TeamSession]: This session already has 2 teams recorded, change the session for this team';
	END IF;
END //

DELIMITER //
CREATE TRIGGER validate_team_session_update
BEFORE UPDATE ON TeamSession FOR EACH ROW
BEGIN
	IF NEW.session_id_fk != OLD.session_id_fk AND (SELECT COUNT(*) FROM TeamSession WHERE session_id_fk = NEW.session_id_fk) > 1 THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = '[TeamSession]: This session already has 2 teams recorded, change the session for this team';
	END IF;
END //

-- *************** Procedures and event schedulers *******************

DELIMITER //
CREATE PROCEDURE WeeklyEmailBlast()
-- Procedure to log weekly emails informing players of the sessions they will be participating in this upcoming week.
BEGIN
	DECLARE done INT DEFAULT 0;
	DECLARE current_cmn INT;
	DECLARE current_sid INT;
	DECLARE cur_clubmem CURSOR FOR SELECT cmn FROM ClubMember;
	DECLARE cur_session CURSOR FOR SELECT id FROM session;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	IF (SELECT COUNT(*) FROM Session WHERE event_date_time > NOW() AND event_date_time < NOW() + INTERVAL 7 DAY) > 0 THEN    
		OPEN cur_session;
		per_session_loop: LOOP
			FETCH cur_session INTO current_sid;
			IF done THEN
				LEAVE per_session_loop;
			END IF;
			
			IF (SELECT COUNT(*) FROM Session WHERE id = current_sid AND (event_date_time > NOW() AND event_date_time < NOW() + INTERVAL 7 DAY)) = 0 THEN
				ITERATE per_session_loop;
			END IF;
			
			OPEN cur_clubmem;
			per_clubmem_loop: LOOP
				FETCH cur_clubmem INTO current_cmn;
				IF done THEN
					SET done = 0;
					LEAVE per_clubmem_loop;
				END iF;
				
				IF (
				SELECT COUNT(*) FROM TeamMember tm
				JOIN TeamFormation tf ON tm.team_formation_id_fk = tf.id
				JOIN TeamSession ts ON tf.id = ts.team_formation_id_fk
				JOIN Session s ON ts.session_id_fk = s.id
				WHERE tm.cmn_fk = current_cmn
				AND s.id = current_sid
				) > 0 THEN
					INSERT INTO LogEmail (recipient, delivery_date_time, sender, subject, body)
					VALUES 
					((SELECT email FROM ClubMember where cmn = current_cmn),
					NOW(),
					"noreply@myvc.ca",
					CONCAT("MYVC ", (SELECT event_date_time FROM Session WHERE id = current_sid), " ", (SELECT name FROM TeamFormation tf JOIN TeamSession ts ON tf.id = ts.team_formation_id_fk JOIN TeamMember tm ON tm.team_formation_id_fk = tf.id WHERE ts.session_id_fk = current_sid AND tm.cmn_fk = current_cmn), " ", (SELECT event_type FROM Session WHERE id = current_sid)),
					CONCAT("Hello ", 
							(SELECT first_name FROM ClubMember WHERE cmn = current_cmn), 
							" ", 
							(SELECT first_name FROM ClubMember WHERE cmn = current_cmn), 
							", this email is a reminder to inform you that you will be playing at ", 
							(SELECT address FROM Location l JOIN Session s ON l.id = s.location_id_fk WHERE s.id = current_sid),
							" as ",
							(SELECT role FROM TeamMember tm JOIN TeamFormation tf ON tm.team_formation_id_fk = tf.id JOIN TeamSession ts ON tf.session_id_fk = ts.session_id_fk WHERE tm.cmn_fk = current_cmn AND ts.session_id_fk = current_sid),
							". The team captain is ",
							(SELECT first_name FROM FamilyMember fm JOIN TeamFormation tf ON fm.id = tf.captain_id_fk JOIN TeamSession ts ON tf.id = ts.team_formation_id_fk WHERE ts.session_id_fk = current_sid),
							" ",
							(SELECT first_name FROM FamilyMember fm JOIN TeamFormation tf ON fm.id = tf.captain_id_fk JOIN TeamSession ts ON tf.id = ts.team_formation_id_fk WHERE ts.session_id_fk = current_sid),
							" (",
							(SELECT email FROM FamilyMember fm JOIN TeamFormation tf ON fm.id = tf.captain_id_fk JOIN TeamSession ts ON tf.id = ts.team_formation_id_fk WHERE ts.session_id_fk = current_sid),
							"). Session type: ",
							(SELECT event_type FROM Session WHERE id = current_sid),
							". Don't forget to have fun!"));
				END IF;	
			END LOOP;
			CLOSE cur_clubmem;
		END LOOP;
		CLOSE cur_session;
	END IF;
END //
DELIMITER ;

DELIMITER //
CREATE EVENT WeeklyGameScheduleEmail
ON SCHEDULE EVERY 1 WEEK
STARTS TIMESTAMP '2025-03-30 00:00:00'
DO
CALL WeeklyEmailBlast();
//
DELIMITER ;

DELIMITER //
CREATE PROCEDURE PurgeTheElderly()
-- Procedure to update the status of every club member who has turned 18
BEGIN
	DECLARE done INT DEFAULT 0;
    DECLARE current_cmn INT;
    DECLARE current_dob DATE;
    DECLARE cur CURSOR FOR SELECT cmn FROM ClubMember;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    
    OPEN cur;
    
    clubmem_loop: LOOP
	FETCH cur INTO current_cmn;
        
        IF done THEN
		LEAVE clubmem_loop;
	END IF;
        
        SET current_dob = (SELECT dob FROM ClubMember WHERE cmn = current_cmn);
        IF TIMESTAMPDIFF(YEAR, current_dob, NOW()) >= 18 THEN
			INSERT INTO LogEmail (recipient, delivery_date_time, sender, subject, body)
            VALUE
            ((SELECT email FROM ClubMember WHERE cmn = current_cmn),
            NOW(),
            "noreply@myvc.ca",
            "MYVC Notice of Membership Deactivation",
            CONCAT("Hello ", (SELECT first_name FROM ClubMember WHERE cmn = current_cmn), ", this is a notice that your membership with the Montreal Youth Volleyball Club has been terminated as you are now over our eligible age limit. We hope you had a great time with us and wish you all the best in your future volleyball endeavors!"));
            UPDATE ClubMember
            SET is_active = 0
            WHERE cmn = current_cmn;
        END IF;
	END LOOP;
	CLOSE cur;
END //
DELIMITER ;

DELIMITER //
CREATE EVENT MonthlyMembershipPurgeEmail
ON SCHEDULE EVERY 1 MONTH
STARTS TIMESTAMP '2025-04-1 00:00:00'
DO
CALL PurgeTheElderly();
//
DELIMITER ;
