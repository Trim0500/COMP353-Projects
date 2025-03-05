-- Query 3: CRUD for FamilyMember (Primary/Secondary)

ALTER TABLE FamilyMember AUTO_INCREMENT = 1;

INSERT INTO FamilyMember (first_name,last_name,dob,social_sec_num,med_card_num,city,province,phone_number,postal_code)
VALUES ('Albein "Alm"','Rudolph II','1992-03-14','123456789','RUDA00020003','Ram','ZO','4383323524','A1M0RG');

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

INSERT INTO ClubMember (first_name,last_name,dob,height,weight,social_sec_num,med_card_num,phone_number,city,province,postal_code,progress_report,is_active,family_member_id_fk,primary_relationship,secondary_relationship)
VALUES ('Walhart','The Conq','2012-04-19',190.5,165.3,'987654321','CONW00020004','4383338256','Valm','VM','W4L1H3','Some Skill...',1,1,"Other","Other");

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
    ) <= 3;
