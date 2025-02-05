create database if not exists WarmUpProject;

Create Table Personnel(
	personnel_id int auto_increment primary key,
    first_name varchar(50),
    last_name varchar(50),
    date_df_birth date,
    social_sec_number int not null,
    phone_number int,
    city varchar(50),
    province varchar(2),
    postal_code varchar(6),
    email_address varchar(50),
    role varchar(50),
    mandate varchar(50)
);

Create Table Location(
	location_id int auto_increment primary key,
    name varchar(50),
    city varchar(50),
    postal_code varchar(6),
    type varchar(50),
    address varchar(100),
    province varchar(2),
    capacity int
);

Create Table LocationPhone(
	location_id_fk int primary key,
    phone_number int,
    foreign key(location_id_fk) references Location(location_id)
);

Create Table PersonnelLocationDate(
	personnel_id_fk int,
    location_id_fk int,
    start_date date,
    end_date date,
    primary key (personnel_id_fk, location_id_fk, start_date),
    foreign key(personnel_id_fk) references Personnel(personnel_id),
    foreign key(location_id_fk) references Location(location_id)
);

Create Table FamilyMember(
	family_member_id int auto_increment primary key,
	first_name varchar(50),
    last_name varchar(50),
    date_of_birth date,
    social_sec_number int not null,
    med_card_number int not null,
    phone_number int,
    address varchar(100),
    city varchar(50),
    province varchar(2),
    postal_code varchar(6),
    email_address varchar(50),
    location_id_fK int,
    foreign key(location_id_fK) references Location(location_id)
);

Create Table ClubMember(
	member_id int auto_increment primary key,
    family_memeber_id_fk int,
    relationship_type varchar(20),
    first_name varchar(50),
    last_name varchar(50),
    date_of_birth date,
    height float(5,2),
    weight float(5,2),
    social_sec_number int not null,
    med_card_number int not null,
    phone_number int,
    address varchar(100),
    city varchar(50),
    province varchar(2),
    postal_code varchar(6),
    foreign key(family_memeber_id_fk) references FamilyMember(family_member_id)
);

Create Table Payment(
	payment_id int auto_increment primary key,
    member_id_fk int,
    date date,
    amount float(5,2),
    method varchar(10),
    effective_date date,
    installment int,
    foreign key(member_id_fk) references ClubMember(member_id)
);
