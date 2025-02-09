# Query 7
# ----------------------------------------
# For a given club member, give details of all payments for the membership fees.
# Information includes date of payment, amount of payment, and year of payment. The results should be displayed sorted in ascending order by date.
# ----------------------------------------

Set @PaymentMemberID = 1;

Select 
	date as 'PaymentDate',
    amount as 'PaymentAmount',
    format(date, 'yyyy') as 'PaymentYear'
From Payment
Where member_id_fk = @PaymentMemberID
Order By date;
