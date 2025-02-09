# Query 3
# ----------------------------------------
# For a given location, provide a report that displays information about the personnel
# who are currently operating in that location. The information includes first-name,
# last-name, date of birth, Social Security Number, Medicare card number, telephone
# number, address, city, province, postal code, email address, role (General manager,
# deputy manager, Coach, etc.) and mandate (Volunteer or Salaried).

SET @LocationId = 1;
SELECT 
	p.first_name, p.last_name, p.date_of_birth, p.social_sec_number, p.med_card_number, p.phone_number, p.address, p.city, p.province, p.postal_code, 
	p.email_address, p.role, p.mandate, pld.location_id_fk
FROM 
	Personnel as p JOIN PersonnelLocationDate as pld 
WHERE
	p.personnel_id = pld.personnel_id_fk AND 
    pld.end_date IS NULL AND
    pld.location_id_fk = CAST(@LocationId AS UNSIGNED);

# Query 4
# ----------------------------------------
# Get a detailed list of all club members registered in the system. The list should
# include the location name that the club member that is currently associated with,
# the membership number of the club member, first-name, last-name, age, city,
# province, and status (active or inactive). The results should be displayed sorted in
# ascending order by location name, then by age.
# ----------------------------------------

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
