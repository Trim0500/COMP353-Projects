-- Query 1
-- Create/Delete/Edit/Display a Location.
INSERT INTO Location (id, type, name, postal_code, province, address, city, website_url, capacity)
VALUE (); -- user specified

DELETE FROM Location
WHERE id = 0; -- user specified

UPDATE Location
SET name = 'user specified'
WHERE id = 0; -- user specified

SELECT * FROM Location;

-- Query 2
-- Create/Delete/Edit/Display a Personnel.
INSERT INTO Personnel (id, first_name, last_name, dob, social_sec_num, med_card_num, phone_number, city, province, postal_code, email, mandate)
VALUE (); -- user specified

DELETE FROM Personnel
WHERE id = 0; -- user specified

UPDATE Personnel
SET first_name = 'user specified', last_name = 'user specified'
WHERE id = 0; -- user specified

SELECT * FROM Personnel;

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

INSERT INTO ClubMember (first_name,last_name,dob,email,height,weight,social_sec_num,med_card_num,phone_number,city,province,postal_code,address,progress_report,is_active,family_member_id_fk,primary_relationship,secondary_relationship,gender)
VALUES ('Walhart','The Conq','2012-04-19','wconq@valmail.com',190.5,165.3,'987654321','CONW00020004','4383338256','Valm','VM','W4L1H3','42 Saber Av.','Some Skill...',0,1,"Other","Other",'M');

SELECT *
FROM ClubMember;

Update ClubMember
SET first_name = 'Hugh', last_name = 'Mungus'
WHERE cmn = 1;

Delete FROM ClubMember
WHERE cmn = 10;

-- Query 5: CRUD for TeamFormation
INSERT INTO TeamFormation VALUES (0, 'Super Spinners', 0, 0);

UPDATE TeamFormation
SET name = 'Frisbee Champs'
WHERE id = 0;

SELECT * FROM TeamFormation;

DELETE FROM TeamFormation WHERE id = 0;

-- Query 6: CRUD For Team Member

INSERT INTO TeamMember VALUES (16, 20, 'middle blocker', '2023-01-23 15:23:54');

UPDATE TeamMember SET role = 'defensive specialist'
WHERE cmn_fk = 20;

DELETE FROM TeamMember WHERE cmn_fk = 20;

-- Query 7
-- Get complete details for every location in the system. Details include address, city,
-- province, postal-code, phone number, web address, type (Head, Branch), capacity,
-- general manager name, and the number of club members associated with that location.
-- The results should be displayed sorted in ascending order by Province, then by city
SELECT DISTINCT
    l.id,
    l.address,
    l.city,
    l.province,
    l.postal_code AS "postal code",
    l.website_url AS "website",
    l.type,
    l.capacity,
    ph.phone_number AS "phone number",
    ( -- Count of active members at this location
	SELECT COUNT(DISTINCT cm.cmn)
	FROM ClubMember cm
	JOIN FamilyMember fm ON fm.id = cm.family_member_id_fk
        JOIN FamilyMemberLocation fml ON fm.id = fml.family_member_id_fk
	WHERE fml.location_id_fk = l.id
	AND ( -- Condition for members to be considered active
		SELECT SUM(p.amount) 
		FROM Payment p 
		WHERE p.cmn_fk = cm.cmn 
		AND YEAR(p.effectiveDate) = YEAR(now())
		GROUP BY p.cmn_fk
	) >= 100.00
    ) AS "number of active members",
    ( -- General manager first name
	SELECT pr.first_name 
	FROM Personnel pr 
	JOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk
	WHERE pl.location_id_fk = l.id
	AND pl.role = "General Manager"
	LIMIT 1
	) AS "general manager first name",
    ( -- General manager last name
	SELECT pr.last_name 
	FROM Personnel pr 
	JOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk
	WHERE pl.location_id_fk = l.id
	AND pl.role = "General Manager" 
        LIMIT 1
	) AS "general manager last name"
FROM Location l
JOIN LocationPhone ph ON l.id = ph.location_id_fk
ORDER BY l.province ASC, l.city ASC;

-- Query 8
-- For a given family member, get details of all the locations that she/he was/is associated
-- with, the secondary family member and all the club members associated with the
-- primary family member. Information includes first name, last name and phone number
-- of the secondary family member, and for every associated club member, the location
-- name, the club membership number, first-name, last-name, date of birth, Social
-- Security Number, Medicare card number, telephone number, address, city, province,
-- postal-code, and relationship with the secondary family member.
SELECT 
    l.name AS "location name",
    cm.cmn AS "club member number",
    cm.first_name AS "club member first name",
    cm.last_name AS "club member last name",
    cm.dob AS "club member birthday",
    cm.social_sec_num AS "club member social security number",
    cm.med_card_num AS "club member medical card number",
    cm.phone_number AS "club member phone number",
    cm.address AS "club member address",
    cm.city AS "club member city",
    cm.province AS "club member province",
    cm.postal_code AS "club member postal code",
    cm.secondary_relationship AS "club member secondary relationship",
    (
	SELECT sfm.first_name
        FROM SecondaryFamilyMember sfm
        WHERE sfm.primary_family_member_id_fk = fm.id
    ) AS "secondary family member first name",
    (
	SELECT sfm.last_name
        FROM SecondaryFamilyMember sfm
        WHERE sfm.primary_family_member_id_fk = fm.id
    )AS "secondary family member last name",
    (
	SELECT sfm.phone_number
        FROM SecondaryFamilyMember sfm
        WHERE sfm.primary_family_member_id_fk = fm.id
    )AS "secondary family member phone number",
    (
	SELECT sfm.relationship_to_primary
        FROM SecondaryFamilyMember sfm
        WHERE sfm.primary_family_member_id_fk = fm.id
    ) AS "secondary family member relationship to primary"
FROM FamilyMember fm
JOIN ClubMember cm ON cm.family_member_id_fk = fm.id
JOIN FamilyMemberLocation fml ON fml.family_member_id_fk = fm.id
JOIN Location l ON l.id = fml.location_id_fk
WHERE fm.first_name = "Paul" AND fm.last_name = "Denton"; -- User defined first and last name

-- Query 9
-- For a given location and week, get details of all the teams’ formations recorded in the
-- system. Details include, head coach first name and last name, start time of the training
-- or game session, address of the session, nature of the session (training or game), the
-- teams name, the score (if the session is in the future, then score will be null), and the
-- first name, last name and role (goalkeeper, defender, etc.) of every player in the team.
-- Results should be displayed sorted in ascending order by the start day then by the start
-- time of the session.
SELECT
    ( -- Head coach first name
	SELECT pr.first_name 
	FROM Personnel pr 
	JOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk
	WHERE pl.location_id_fk = l.id
	AND pl.role = "Coach"
	LIMIT 1
    ) AS "head coach first name",
    ( -- Head coach first name
	SELECT pr.last_name 
	FROM Personnel pr 
	JOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk
	WHERE pl.location_id_fk = l.id
	AND pl.role = "Coach"
	LIMIT 1
    ) AS "head coach last name",
    s.event_date_time,
    l.address, 
    s.event_type,
    tf.name as "team name",
    ts.score,
    (
	SELECT cm.first_name
        FROM ClubMember cm
        WHERE cm.cmn = tm.cmn_fk
    ) as "player first name",
    (
	SELECT cm.last_name
        FROM ClubMember cm
        WHERE cm.cmn = tm.cmn_fk
    ) as "player last name",
    tm.role
FROM TeamFormation tf
JOIN TeamMember tm ON tm.team_formation_id_fk = tf.id
JOIN Location l ON (l.id = tf.location_id_fk AND l.name = "MYVC HQ") -- User defined location name
JOIN Session s ON (s.location_id_fk = l.id AND (s.event_date_time >= '2025-03-04 00:00:00' OR s.event_date_time >= '2025-03-12 00:00:00')) -- User defined time period
JOIN TeamSession ts ON ts.session_id_fk = s.id
ORDER BY s.event_date_time ASC;

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
		SELECT COUNT(FML.location_id_fk)
		FROM FamilyMemberLocation FML
        Where CM.family_member_id_fk = FML.family_member_id_fk
        GROUP BY FML.family_member_id_fk
	) >= 3 AND
    (
		SELECT COUNT(1) FROM (
								SELECT COUNT(1)
								FROM Payment
								WHERE cmn_fk = CM.cmn
								GROUP BY effectiveDate
								HAVING SUM(amount) >= 100.00
                                ) AS FulfilledPayments
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
    SUM(
		CASE
			WHEN S.event_type = 'training' THEN 1
            ELSE 0
        END
    ) AS 'TrainingSessions',
	(SELECT SUM(SessionPlayerCount)
		FROM (
				SELECT COUNT(DISTINCT TM.cmn_fk) AS SessionPlayerCount
				FROM Session s
				JOIN TeamSession TS ON s.id = TS.session_id_fk
				JOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id
				JOIN TeamMember TM ON TF.id = TM.team_formation_id_fk
				WHERE s.location_id_fk = L.id AND s.event_type = 'training'
				GROUP BY s.id
			) AS TotalPlayersAtLocation
    ) AS TotalTrainingSessionPlayers,
    SUM(
		CASE
			WHEN S.event_type = 'game' THEN 1
            ELSE 0
        END
    ) AS 'GameSessions',
    (SELECT SUM(SessionPlayerCount)
		FROM (
				SELECT COUNT(DISTINCT TM.cmn_fk) AS SessionPlayerCount
				FROM Session s
				JOIN TeamSession TS ON s.id = TS.session_id_fk
				JOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id
				JOIN TeamMember TM ON TF.id = TM.team_formation_id_fk
				WHERE s.location_id_fk = L.id AND s.event_type = 'game'
				GROUP BY s.id
			) AS TotalPlayersAtLocation
	) AS TotalGameSessionPlayers
FROM Location L
JOIN Session S ON L.id = S.location_id_fk
WHERE date_format(S.event_date_time, '%Y-%m-%d') >= @startDate AND date_format(S.event_date_time, '%Y-%m-%d') <= @endDate
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
WHERE CM.is_active = 1 AND (SELECT COUNT(TM.cmn_fk)) < 1
ORDER BY LocationNames, CM.cmn;

-- Query 13:
-- Get a report on all active club members who have only been assigned as outside hitter in all the formation team sessions they have been assigned to. They must be assigned
-- to at least one formation session as an outside hitter. They should have never been assigned to any formation session with a role different than outside hitter. The list
-- should include the club member’s membership number, first name, last name, age, phone number, email and current location name. The results should be displayed sorted
-- in ascending order by location name then by club membership number.

SELECT cmn, first_name, last_name, Age, phone_number, email, LocationsList 
FROM
(
	SELECT ClubMember.cmn, ClubMember.first_name, ClubMember.last_name, year(now()) - year(ClubMember.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(ClubMember.dob, '%m%d')) AS 'Age', phone_number, email, family_member_id_fk AS FamilyMemberId 
	FROM
	(
		SELECT cmn_fk 
        FROM 
			(
				SELECT DISTINCT cmn_fk 
                FROM TeamMember WHERE ROLE = "outside hitter"
			) AS OutsideHitters 
			WHERE cmn_fk NOT IN 
            (
				SELECT DISTINCT cmn_fk 
				FROM TeamMember 
                WHERE ROLE != "outside hitter"
			)
		) AS OutsideHittersOnly
		JOIN (SELECT * FROM ClubMember WHERE is_active = 1) as ClubMember ON OutsideHittersOnly.cmn_fk = ClubMember.cmn
) AS ClubMemberReport 
JOIN 
(
	SELECT FML.FamilyMemberId, GROUP_CONCAT(L.LocationName SEPARATOR ', ') AS LocationsList 
	FROM 
		(
			(
				SELECT location_id_fk AS LocationId, family_member_id_fk as FamilyMemberId 
				FROM FamilyMemberLocation 
				WHERE end_date IS NULL
			) AS FML
			JOIN
			(
				SELECT id, name AS LocationName 
				FROM Location
			) AS L
			ON L.id = FML.LocationId
		) GROUP BY FamilyMemberId
) AS FamilyMemberLocationsList ON FamilyMemberLocationsList.FamilyMemberId = ClubMemberReport.FamilyMemberId
ORDER BY LocationsList ASC, cmn ASC;

/*USE THIS FOR QUERIES THAT NEED TO BE ORDERED BY CLUB MEMBER LOCATION*/
SELECT 
	FML.FamilyMemberId, GROUP_CONCAT(L.LocationName SEPARATOR ', ') 
    AS LocationsList 
FROM 
	(
		(
			SELECT location_id_fk AS LocationId, family_member_id_fk as FamilyMemberId FROM FamilyMemberLocation WHERE end_date IS NULL
		) 
		AS FML
		JOIN
		(
			SELECT id, name AS LocationName FROM Location
		) AS L
		ON L.id = FML.LocationId
	) 
GROUP BY FamilyMemberId;

-- Query 14
-- Get a report on all active club members who have been assigned at least once to every role throughout all the formation team game sessions. The club member must be
-- assigned to at least one formation game session as an outside hitter, opposite, setter, middle blocker, libero, defensive specialist, and serving specialist. The list should
-- include the club member’s membership number, first name, last name, age, phone number, email and current location name. The results should be displayed sorted in
-- ascending order by location name then by club membership number.
SELECT cmn, first_name, last_name, year(now()) - year(ClubMemberReport.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(ClubMemberReport.dob, '%m%d')) AS 'Age', phone_number, email, LocationsList
FROM 
	(
		SELECT * 
        FROM 
		(
			SELECT cmn_fk 
            FROM
			(
				SELECT DISTINCT cmn_fk FROM TeamMember AS oh WHERE role = "outside hitter"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS op WHERE role = "opposite"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS s WHERE role = "setter"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS mb WHERE role = "middle blocker"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS l WHERE role = "libero"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS ds WHERE role = "defensive specialist"
				UNION ALL
				SELECT DISTINCT cmn_fk FROM TeamMember AS ss WHERE role = "serving specialist"
			) AS UnionAll GROUP BY cmn_fk HAVING COUNT(*) = 7
		) AS AllRolesPlayers
	JOIN (SELECT * FROM ClubMember) AS CM ON CM.cmn = AllRolesPlayers.cmn_fk AND CM.is_active = 1
) AS ClubMemberReport
JOIN 
(SELECT 
	FML.FamilyMemberId, GROUP_CONCAT(L.LocationName SEPARATOR ', ') 
    AS LocationsList 
FROM 
	(
		(
			SELECT location_id_fk AS LocationId, family_member_id_fk as FamilyMemberId FROM FamilyMemberLocation WHERE end_date IS NULL
		) AS FML
		JOIN
		(
			SELECT id, name AS LocationName FROM Location
		) AS L
		ON L.id = FML.LocationId
	) 
GROUP BY FamilyMemberId) AS FamilyMemberReport ON FamilyMemberReport.FamilyMemberId = ClubMemberReport.cmn_fk;

-- Query 15
-- For the given location, get the list of all family members who have currently active club members associated with them and are also captains for the same location.
-- Information includes first name, last name, and phone number of the family member. A family member is considered to be a captain if she/he is assigned as a captain to at
-- least one team formation session in the same location.
SET @LocationId = 1;

#Family members associated with the given location that have active club members
SELECT CaptainReport.first_name, CaptainReport.last_name, CaptainReport.phone_number FROM
(
	SELECT family_member_id_fk, first_name, last_name, phone_number, med_card_num, social_sec_num FROM
	(
		SELECT DISTINCT FMSL.family_member_id_fk 
		FROM
		(
			SELECT * 
			FROM FamilyMemberLocation 
			WHERE location_id_fk = @LocationId
		) AS FMSL 
		JOIN 
		(
			SELECT * 
			FROM ClubMember 
			WHERE is_active = 1
		) AS CM ON FMSL.family_member_id_fk = CM.family_member_id_fk
	) AS FMWithLocationActiveCM
	JOIN
	(
		SELECT * 
		FROM FamilyMember
	) AS FM ON FMWithLocationActiveCM.family_member_id_fk = FM.id
) AS FamilyMemberReport
JOIN
#Captains of teams from that location
(
	SELECT TFSL.id, TFSL.name, CM.first_name, CM.last_name, phone_number, med_card_num, social_sec_num 
	FROM 
	(
		SELECT * 
		FROM TeamFormation 
		WHERE location_id_fk = @LocationId
	) AS TFSL 
	JOIN 
	(
		SELECT * 
		FROM ClubMember
	) AS CM ON CM.cmn = TFSL.captain_id_fk
) AS CaptainReport ON FamilyMemberReport.first_name = CaptainReport.first_name 
					AND FamilyMemberReport.last_name = CaptainReport.last_name 
                    AND FamilyMemberReport.med_card_num = CaptainReport.med_card_num
                    AND FamilyMemberReport.social_sec_num = CaptainReport.social_sec_num;

-- Query 16: Get a report of all active club members who have never lost a game in which they
--	     played. A club member is considered to win a game if she/he has been assigned to a
--	     game session and is assigned to the team that has a score higher than the score of the
--	     other team. The club member must be assigned to at least one formation game session.
--	     Results should include the club member’s membership number, first name, last name,
--	     age, phone number, email and current location name. The results should be displayed
--	     sorted in ascending order by location name then by club membership number.

DELIMITER //

DROP function if exists has_only_won;

CREATE function has_only_won(cmn int) returns int
begin

DECLARE result int;

select count(*) into result
from (
    select team_formation_id_fk from TeamMember
    where cmn_fk = cmn
    except
    select TF.id from TeamFormation TF
    join TeamSession TS on TF.id = TS.team_formation_id_fk
    where TS.score = 100
    group by TF.id
) as test;

return result;

end//

SELECT DISTINCT
    cmn,
    first_name,
    last_name,
    year(now()) - year(CM.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(CM.dob, '%m%d')) AS 'Age',
    phone_number,
    email,
    (
	SELECT GROUP_CONCAT(L.name ORDER BY L.name SEPARATOR ', ')
        FROM Location L
        JOIN FamilyMemberLocation FML ON L.id = FML.location_id_fk
        WHERE FML.family_member_id_fk = CM.family_member_id_fk
        GROUP BY FML.family_member_id_fk
    ) AS 'LocationNames'	
from ClubMember CM
join TeamMember TM on CM.cmn = TM.cmn_fk
WHERE has_only_won(cmn) < 1
ORDER BY 'LocationNames' ASC, CM.cmn ASC;

-- Query 17: Get a report of all the personnel who were treasurer of the club at least once or is currently a treasurer of the club.
-- 				The report should include the treasurer’s first name, last name, start date as a treasurer and last date as treasurer. If last date as treasurer is null means that the personnel is the current treasurer of the club.
-- 				 Results should be displayed sorted in ascending order by first name then by last name then by start date as a treasurer.

SELECT P.first_name, P.last_name, PL.start_date AS 'start date', PL.end_date AS 'end date'
FROM Personnel P
JOIN PersonnelLocation PL ON P.id = PL.personnel_id_fk
WHERE PL.role = 'treasurer'
ORDER BY P.first_name ASC, P.last_name ASC, 'start date' ASC;

-- Query 18: Get a report on all club members who were deactivated by the system because they became over 18 years old.
-- 				Results should include the club member’ first name, last name, telephone number, email address, deactivation date, last location name and last role when deactivated. 
-- 				Results should be displayed sorted in ascending order by location name, then by role, then by first name then by last name.

SELECT CM.first_name, CM.last_name, CM.phone_number, CM.email, DATE_ADD(dob, INTERVAL 18 YEAR), 
(
	SELECT GROUP_CONCAT(L.name ORDER BY L.name SEPARATOR ', ')
    	FROM Location L
    	JOIN FamilyMemberLocation FML ON L.id = FML.location_id_fk
    	WHERE FML.family_member_id_fk = CM.family_member_id_fk
    	GROUP BY FML.family_member_id_fk
) AS 'last location name', 
(
	SELECT TM.role FROM TeamMember TM 
		WHERE CM.cmn = TM.cmn_fk AND TM.assignment_date_time = 
        (
        SELECT MAX(assignment_date_time)
			FROM TeamMember TM2
            WHERE TM.cmn_fk = TM2.cmn_fk
        )
) AS 'latest role'
FROM ClubMember CM
ORDER BY 'last location name' ASC, 'latest role' ASC, CM.first_name ASC, CM.last_name ASC;

select * from FamilyMember;
select * from ClubMember;
