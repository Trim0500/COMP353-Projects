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
