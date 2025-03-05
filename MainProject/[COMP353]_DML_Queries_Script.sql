-- Query 3: CRUD for FamilyMember (Primary/Secondary)

ALTER TABLE FamilyMember AUTO_INCREMENT = 1;

Insert Into FamilyMember (first_name,last_name,dob,social_sec_num,med_card_num,city,province,phone_number,postal_code)
Values ('Albein "Alm"','Rudolph II','1992-03-14','123456789','RUDA00020003','Ram','ZO','4383323524','A1M0RG');

Select *
From FamilyMember;

Update FamilyMember
Set first_name = 'Pin', last_name = 'Gas'
Where id = 1;

Delete From FamilyMember
Where id = 10;

Insert Into SecondaryFamilyMember
Values (1,'Anthiese "Celica"','Lima V','4383327245');

Select *
From SecondaryFamilyMember;

Update SecondaryFamilyMember
Set first_name = 'Dick', last_name = 'Tree'
Where primary_family_member_id_fk = 1;

Delete From SecondaryFamilyMember
Where primary_family_member_id_fk = 10;

-- Query 4: CRUD for ClubMember

ALTER TABLE ClubMember AUTO_INCREMENT = 1;

Insert Into ClubMember (first_name,last_name,dob,height,weight,social_sec_num,med_card_num,phone_number,city,province,postal_code,progress_report,is_active,family_member_id_fk,primary_relationship,secondary_relationship)
Values ('Walhart','The Conq','2012-04-19',190.5,165.3,'987654321','CONW00020004','4383338256','Valm','VM','W4L1H3','Some Skill...',1,1,"Other","Other");

Select *
From SecondaryFamilyMember;

Update SecondaryFamilyMember
Set first_name = 'Dick', last_name = 'Tree'
Where id = 1;

Delete From SecondaryFamilyMember
Where id = 10;
