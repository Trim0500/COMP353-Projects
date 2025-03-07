-- Query 3: CRUD for FamilyMember (Primary/Secondary)

ALTER TABLE FamilyMember AUTO_INCREMENT = 1;

INSERT INTO FamilyMember (first_name,last_name,dob,email,social_sec_num,med_card_num,city,province,phone_number,postal_code)
VALUES ('Albein "Alm"','Rudolph II','1992-03-14','alm.rudolph@rigelnet.com','123456789','RUDA00020003','Ram','ZO','4383323524','A1M0RG');

SELECT *
FROM FamilyMember;

Update FamilyMember
SET first_name = 'Pin', last_name = 'Gas'
WHERE id = 1;

Delete FROM FamilyMember
WHERE id = 10;

INSERT INTO SecondaryFamilyMember
VALUES (1,'Anthiese "Celica"','Lima V','4383327245');

SELECT *
FROM SecondaryFamilyMember;

Update SecondaryFamilyMember
SET first_name = 'Dick', last_name = 'Tree'
WHERE primary_family_member_id_fk = 1;

Delete FROM SecondaryFamilyMember
WHERE primary_family_member_id_fk = 10;

-- Query 4: CRUD for ClubMember

ALTER TABLE ClubMember AUTO_INCREMENT = 1;

INSERT INTO ClubMember (first_name,last_name,dob,email,height,weight,social_sec_num,med_card_num,phone_number,city,province,postal_code,address,progress_report,is_active,family_member_id_fk,primary_relationship,secondary_relationship)
VALUES ('Walhart','The Conq','2012-04-19','wconq@valmail.com',190.5,165.3,'987654321','CONW00020004','4383338256','Valm','VM','W4L1H3','42 Saber Av.','Some Skill...',1,1,"Other","Other");

SELECT *
FROM ClubMember;

Update ClubMember
SET first_name = 'Hugh', last_name = 'Mungus'
WHERE cmn = 1;

Delete FROM ClubMember
WHERE cmn = 10;

-- Query 10: Get details of club members who are currently active and have been associated with at least three different locations and are members for at most three years.
-- 				Details include Club membership number, first name and last name.
-- 				Results should be displayed sorted in ascending order by club membership number.

SELECT
	CM.cmn AS 'MembershipNumber',
    CM.first_name AS 'MemberFirstName',
    CM.last_name AS 'MemberLastName'
FROM ClubMember CM
WHERE CM.is_active = 1 AND
	(
		SELECT COUNT(1)
		FROM FamilyMemberLocation FML
        Where CM.family_member_id_fk = FML.family_member_id_fk
        GROUP BY FML.family_member_id_fk
	) >= 3 AND
    (
		Select COUNT(1)
        FROM Payment
        WHERE cmn_fk = CM.cmn
        GROUP BY effectiveDate
        HAVING SUM(amount) >= 100.00
    ) <= 3
ORDER BY CM.cmn;

-- Query 11: For a given period of time, give a report on the teams’ formations for all the locations.
-- 				For each location, the report should include the location name, the total number of training sessions, the total number of players in the training sessions, the total number of game sessions, the total number of players in the game sessions.
-- 				Results should only include locations that have at least two game sessions.
-- 				Results should be displayed sorted in descending order by the total number of game sessions.
-- 				For example, the period of time could be from Jan. 1 st, 2025 to Mar. 31st, 2025.

SET @startDate = '2025-01-01';

SET @endDate = '2025-03-31';

SELECT
	L.name AS 'LocationName',
    COUNT(
		CASE
			WHEN S.event_type = 'training' THEN 1
            ELSE 0
        END
    ) AS 'TrainingSessions',
    (
		SELECT COUNT(DISTINCT TM.cmn_fk)
		FROM Session s
        JOIN TeamSession TS ON s.id = TS.session_id_fk
        JOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id
        JOIN TeamMember TM ON TF.id = TM.team_formation_id_fk
        WHERE s.location_id_fk = L.id AND s.event_type = 'training'
        GROUP BY s.id
	) AS 'PlayersInTrainingSession',
    COUNT(
		CASE
			WHEN S.event_type = 'game' THEN 1
            ELSE 0
        END
    ) AS 'GameSessions',
    (
		SELECT COUNT(DISTINCT TM.cmn_fk)
		FROM Session s
        JOIN TeamSession TS ON s.id = TS.session_id_fk
        JOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id
        JOIN TeamMember TM ON TF.id = TM.team_formation_id_fk
        WHERE s.location_id_fk = L.id AND s.event_type = 'game'
        GROUP BY s.id
	) AS 'PlayersInGameSession'
FROM Location L
JOIN Session S ON L.id = S.location_id_fk
WHERE date_format(S.event_date_time, '%Y-%M-%d') >= @startDate AND date_format(S.event_date_time, '%Y-%M-%d') <= @endDate
GROUP BY L.id, L.name
HAVING GameSessions >= 2
ORDER BY GameSessions DESC;

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
    CM.email as 'MemberEmail',
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
