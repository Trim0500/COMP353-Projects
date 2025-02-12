# Query 1
# ----------------------------------------
# Get complete details for every location in the system. 
# Details include address, city, province, postal code, phone number, web address, type (Head, Branch), 
# capacity, general manager name, number of personnel, and the number of club members associated with that location. 
# The results should be displayed sorted in ascending order by Province, then by city.

SELECT DISTINCT
    l.address,
    l.city,
    l.province,
    l.postal_code AS "postal code",
    l.website,
    l.type,
    l.capacity,
    ph.phone_number AS "phone number",
    ( -- Count of active personnel at this location
		SELECT COUNT(DISTINCT pr.personnel_id)
		FROM Personnel pr 
		JOIN PersonnelLocationDate pld ON pld.personnel_id_fk = pr.personnel_id
		WHERE pld.location_id_fk = l.location_id 
		AND pld.end_date IS NULL -- Condition for personnel to be considered active
	) AS "number of active personnel",
    ( -- Count of active members at this location
		SELECT COUNT(DISTINCT cm.member_id)
		FROM ClubMember cm
		JOIN FamilyMember fm ON fm.family_member_id = cm.family_member_id_fk
		WHERE fm.location_id_fk = l.location_id
		AND ( -- Condition for members to be considered active (payments from last year total to over $100), both 2024 and 2025 accepted
				SELECT SUM(p.amount) 
				FROM Payment p 
				WHERE p.member_id_fk = cm.member_id 
				AND (YEAR(p.effective_date) = 2024 OR YEAR(p.effective_date) = 2025)
				GROUP BY p.member_id_fk
			) >= 100.00
    ) AS "number of active members",
    -- NOTE: queries for general manager assume only one general manager per location
    ( -- General manager first name
		SELECT pr.first_name 
		FROM Personnel pr 
		JOIN PersonnelLocationDate pld ON pr.personnel_id = pld.personnel_id_fk
		WHERE pld.location_id_fk = l.location_id
		AND pr.role = "General Manager"
		LIMIT 1
	) AS "general manager first name",
    ( -- General manager last name
		SELECT pr.last_name 
		FROM Personnel pr 
		JOIN PersonnelLocationDate pld ON pr.personnel_id = pld.personnel_id_fk
		WHERE pld.location_id_fk = l.location_id
		AND pr.role = "General Manager" 
        LIMIT 1
	) AS "general manager last name"
FROM Location l
JOIN LocationPhone ph ON l.location_id = ph.location_id_fk
ORDER BY l.province ASC, l.city ASC;

# Query 2
# ----------------------------------------
# For a given location, provide a report that lists for every family member who is currently registered in the location, 
# the number of related active club members. Information includes family members’ first name, 
# last name, and the number of active club members that are associated with the family member.

SELECT 
	fm.first_name,
	fm.last_name,
    COUNT(cm.member_id)
FROM FamilyMember fm
JOIN ClubMember cm ON fm.family_member_id = cm.family_member_id_fk
WHERE fm.location_id_fk = 1 -- Arbitrary location ID
AND ( -- Subquery to filter for active members (sum of payments for the current year is at least $100), both 2024 and 2025 accepted
	SELECT SUM(p.amount) FROM Payment p 
    WHERE p.member_id_fk = cm.member_id 
    AND (YEAR(p.effective_date) = '2024' OR YEAR(p.effective_date) = '2025')
    GROUP BY p.member_id_fk
    ) >= 100.00
GROUP BY fm.family_member_id;


# Query 3
# ----------------------------------------
# For a given location, provide a report that displays information about the personnel
# who are currently operating in that location. The information includes first-name,
# last-name, date of birth, Social Security Number, Medicare card number, telephone
# number, address, city, province, postal code, email address, role (General manager,
# deputy manager, Coach, etc.) and mandate (Volunteer or Salaried).


SET @LOCATION_ID = 1;
SELECT 
	p.first_name, p.last_name, p.date_of_birth, p.social_sec_number, p.med_card_number, p.phone_number, p.address, p.city, p.province, p.postal_code, 
	p.email_address, p.role, p.mandate, pld.location_id_fk
FROM 
	Personnel as p 
	JOIN 
		PersonnelLocationDate as pld 
WHERE
	p.personnel_id = pld.personnel_id_fk AND 
    pld.end_date IS NULL AND
    pld.location_id_fk = @LOCATION_ID;

# Query 4
# ----------------------------------------
# Get a detailed list of all club members registered in the system. The list should
# include the location name that the club member that is currently associated with,
# the membership number of the club member, first-name, last-name, age, city,
# province, and status (active or inactive). The results should be displayed sorted in
# ascending order by location name, then by age.
# ----------------------------------------
SELECT 
	Location.name, MemberPaymentSQ.member_id, MemberPaymentSQ.first_name, MemberPaymentSQ.last_name, 
    MemberPaymentSQ.age, MemberPaymentSQ.city, MemberPaymentSQ.province,
	CASE WHEN (YearAmountPaid>=100 AND MemberPaymentSQ.age <=18) THEN 1 ELSE 0 END AS IsActive
FROM
	(
		SELECT 
			location_id_fk, member_id, first_name, last_name, age, city, province, YearAmountPaid
		FROM 
			(
				SELECT 
					fm.location_id_fk, cm.member_id, cm.first_name, cm.last_name, TIMESTAMPDIFF(YEAR, cm.date_of_birth, CURDATE()) AS age, cm.city, cm.province 
				FROM 
					ClubMember AS cm 
				JOIN 
					FamilyMember fm 
				ON 
					cm.family_member_id_fk = fm.family_member_id
			) AS MemberSQ
		JOIN 
			(
				SELECT 
					member_id_fk, SUM(amount) as YearAmountPaid
				FROM
					Payment
				WHERE 
					YEAR(effective_date) = YEAR(CURDATE())
				GROUP BY member_id_fk
			) AS PaymentSQ
		ON 
			member_id = member_id_fk
	) AS MemberPaymentSQ
JOIN 
	Location
ON
	location_id = MemberPaymentSQ.location_id_fK
ORDER BY
	Location.name ASC, age ASC;
    
# Query 5
# ----------------------------------------
# For a given family member, give details of every club member associated with that family member
# Information includes member id, first and last name, date of birth, socia security number, medicare card number
# phone number, address, city, province, postal code, relationship type and status

SELECT
	member_id,
    first_name,
    last_name,
    date_of_birth,
    social_sec_number,
    med_card_number,
    phone_number,
    address,
    city,
    province,
    postal_code,
    relationship_type,
    (SELECT
    CASE
	WHEN sum(amount) >= 100 AND effective_date > '2024-01-01' THEN "active"
        ELSE "inactive"
    END
    FROM Payment
    JOIN ClubMember ON Payment.member_id_fk = Wunderfizz.member_id
    GROUP BY Payment.member_id_fk
    ) AS 'status'
FROM ClubMember AS Wunderfizz
WHERE family_member_id_fk = 3;

# Query 6
# ----------------------------------------
# For a given location select family members that have active clum members associated with them and who are personnels at that same location
# Information includes first name, last name and phone number
# ----------------------------------------

Set @location = 10;

SELECT DISTINCT
	FM.first_name,
	FM.last_name,
	FM.phone_number
FROM FamilyMember as FM
Join Personnel On Personnel.social_sec_number = FM.social_sec_number
Join (
	SELECT Payment.member_id_fk as 'MemberIDFK', ClubMember.family_member_id_fk as 'FamilyMemberIDFK', sum(amount) as 'ClubMemberAmount' from Payment 
	JOIN ClubMember ON Payment.member_id_fk = ClubMember.member_id 
	WHERE effective_date > '2024-01-01'
	GROUP BY Payment.member_id_fk, ClubMember.family_member_id_fk
) X on FM.family_member_id = X.FamilyMemberIDFK and X.ClubMemberAmount >= 100.00
WHERE FM.location_id_fk = @location;	

# Query 7
# ----------------------------------------
# For a given club member, give details of all payments for the membership fees.
# Information includes date of payment, amount of payment, and year of payment. The results should be displayed sorted in ascending order by date.
# ----------------------------------------

set @PaymentMemberID = 12;

select 
	C.first_name as "MemberFirstName",
    C.last_name as "MemberLastName",
    P.date as 'PaymentDate',
    P.amount as 'PaymentAmount',
    date_format(P.date, '%Y') as 'PaymentYear'
from Payment P
join ClubMember C on P.member_id_fk = C.member_id
where member_id_fk = @PaymentMemberID
order by date;

# Query 8
# ----------------------------------------
# Get the sum of membership fees paid and the sum of donations that are collected by the club in the year 2024.
# ----------------------------------------

select 
	C.first_name as "MemberFirstName",
    C.last_name as "MemberLastName",
	case
		when sum(P.amount) > 100.00 then 100.00
        else sum(P.amount)
	end as 'MembershipFeesAmount',
    case
		when sum(P.amount) > 100.00 then sum(P.amount) - 100.00
        else 0.00
	end as 'DonationsAmount'
from Payment P
join ClubMember C on P.member_id_fk = C.member_id
where date_format(effective_date, '%Y') = '2024'
group by member_id_fk;
