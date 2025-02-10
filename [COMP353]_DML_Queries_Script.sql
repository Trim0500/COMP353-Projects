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
	location_id_fk, member_id, first_name, last_name, age, city, province
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
WHERE
	YearAmountPaid >= 100 AND age <=18;

# Query 7
# ----------------------------------------
# For a given club member, give details of all payments for the membership fees.
# Information includes date of payment, amount of payment, and year of payment. The results should be displayed sorted in ascending order by date.
# ----------------------------------------

set @PaymentMemberID = 1;

select 
	date as 'PaymentDate',
    amount as 'PaymentAmount',
    format(date, 'yyyy') as 'PaymentYear'
from Payment
where member_id_fk = @PaymentMemberID
order by date;

# Query 8
# ----------------------------------------
# Get the sum of membership fees paid and the sum of donations that are collected by the club in the year 2024.
# ----------------------------------------

select 
	case
		when sum(amount) > 100.00 then 100.00
        else sum(amount)
	end as 'MembershipFeesAmount',
    case
		when sum(amount) > 100.00 then sum(amount) - 100.00
        else 0.00
	end as 'DonationsAmount'
from Payment
where format(effective_date, 'yyyy') = '2024'
group by member_id_fk;
