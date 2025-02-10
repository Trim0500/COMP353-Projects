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
