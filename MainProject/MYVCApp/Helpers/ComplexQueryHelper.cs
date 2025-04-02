using MYVCApp.Models.ComplexQueryModels;

namespace MYVCApp.Helpers
{
    public class ComplexQueryHelper
    {
        public static readonly Dictionary<int, Tuple<string, FormattableString>> QUERIES = new Dictionary<int, Tuple<string, FormattableString>>()
        {
            {
                7,
                new Tuple<string, FormattableString>
                (
                    "Get complete details for every location in the system. Details include address, city,\r\nprovince, postal-code, phone number, web address, type (Head, Branch), capacity,\r\ngeneral manager name, and the number of club members associated with that location.\r\nThe results should be displayed sorted in ascending order by Province, then by city.",
                    $"SELECT DISTINCT\r\n    l.id,\r\n    l.address,\r\n    l.city,\r\n    l.province,\r\n    l.postal_code AS \"postal code\",\r\n    l.website_url AS \"website\",\r\n    l.type,\r\n    l.capacity,\r\n    ph.phone_number AS \"phone number\",\r\n    ( -- Count of active members at this location\r\n\tSELECT COUNT(DISTINCT cm.cmn)\r\n\tFROM ClubMember cm\r\n\tJOIN FamilyMember fm ON fm.id = cm.family_member_id_fk\r\n        JOIN FamilyMemberLocation fml ON fm.id = fml.family_member_id_fk\r\n\tWHERE fml.location_id_fk = l.id\r\n\tAND ( -- Condition for members to be considered active\r\n\t\tSELECT SUM(p.amount) \r\n\t\tFROM Payment p \r\n\t\tWHERE p.cmn_fk = cm.cmn \r\n\t\tAND (YEAR(p.effectiveDate) = 2024 OR YEAR(p.effectiveDate) = 2025)\r\n\t\tGROUP BY p.cmn_fk\r\n\t) >= 100.00\r\n    ) AS \"number of active members\",\r\n    ( -- General manager first name\r\n\tSELECT pr.first_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"General Manager\"\r\n\tLIMIT 1\r\n\t) AS \"general manager first name\",\r\n    ( -- General manager last name\r\n\tSELECT pr.last_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"General Manager\" \r\n        LIMIT 1\r\n\t) AS \"general manager last name\"\r\nFROM Location l\r\nJOIN LocationPhone ph ON l.id = ph.location_id_fk\r\nORDER BY l.province ASC, l.city ASC;"
                )
            },

            {
                8,
                new Tuple<string, FormattableString>
                (
                    "For a given family member, get details of all the locations that she/he was/is associated\r\nwith, the secondary family member and all the club members associated with the\r\nprimary family member. Information includes first name, last name and phone number\r\nof the secondary family member, and for every associated club member, the location\r\nname, the club membership number, first-name, last-name, date of birth, Social\r\n\r\nSecurity Number, Medicare card number, telephone number, address, city, province,\r\npostal-code, and relationship with the secondary family member.",
                    $"SELECT \r\n    l.name AS \"location name\",\r\n    cm.cmn AS \"club member number\",\r\n    cm.first_name AS \"club member first name\",\r\n    cm.last_name AS \"club member last name\",\r\n    cm.dob AS \"club member birthday\",\r\n    cm.social_sec_num AS \"club member social security number\",\r\n    cm.med_card_num AS \"club member medical card number\",\r\n    cm.phone_number AS \"club member phone number\",\r\n    cm.address AS \"club member address\",\r\n    cm.city AS \"club member city\",\r\n    cm.province AS \"club member province\",\r\n    cm.postal_code AS \"club member postal code\",\r\n    cm.secondary_relationship AS \"club member secondary relationship\",\r\n    (\r\n\tSELECT sfm.first_name\r\n        FROM SecondaryFamilyMember sfm\r\n        WHERE sfm.primary_family_member_id_fk = fm.id\r\n    ) AS \"secondary family member first name\",\r\n    (\r\n\tSELECT sfm.last_name\r\n        FROM SecondaryFamilyMember sfm\r\n        WHERE sfm.primary_family_member_id_fk = fm.id\r\n    )AS \"secondary family member last name\",\r\n    (\r\n\tSELECT sfm.phone_number\r\n        FROM SecondaryFamilyMember sfm\r\n        WHERE sfm.primary_family_member_id_fk = fm.id\r\n    )AS \"secondary family member phone number\",\r\n    (\r\n\tSELECT sfm.relationship_to_primary\r\n        FROM SecondaryFamilyMember sfm\r\n        WHERE sfm.primary_family_member_id_fk = fm.id\r\n    ) AS \"secondary family member relationship to primary\"\r\nFROM FamilyMember fm\r\nJOIN ClubMember cm ON cm.family_member_id_fk = fm.id\r\nJOIN FamilyMemberLocation fml ON fml.family_member_id_fk = fm.id\r\nJOIN Location l ON l.id = fml.location_id_fk\r\nWHERE fm.first_name = \"Paul\" AND fm.last_name = \"Denton\"; -- User defined first and last name"
                )
            },

            {
                9,
                new Tuple<string, FormattableString>
                (
                    "For a given location and week, get details of all the teams’ formations recorded in the\r\nsystem. Details include, head coach first name and last name, start time of the training\r\nor game session, address of the session, nature of the session (training or game), the\r\nteams name, the score (if the session is in the future, then score will be null), and the\r\nfirst name, last name and role (goalkeeper, defender, etc.) of every player in the team.\r\nResults should be displayed sorted in ascending order by the start day then by the start\r\ntime of the session.",
                    $"SELECT\r\n    ( -- Head coach first name\r\n\tSELECT pr.first_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"Coach\"\r\n\tLIMIT 1\r\n    ) AS \"head coach first name\",\r\n    ( -- Head coach first name\r\n\tSELECT pr.last_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"Coach\"\r\n\tLIMIT 1\r\n    ) AS \"head coach last name\",\r\n    s.event_date_time,\r\n    l.address, \r\n    s.event_type,\r\n    tf.name as \"team name\",\r\n    (\r\n\tSELECT ts.score\r\n        FROM TeamSession ts\r\n        WHERE ts.team_formation_id_fk = tf.id\r\n        AND ts.session_id_fk = s.id\r\n    ) as \"score\",\r\n    (\r\n\tSELECT cm.first_name\r\n        FROM ClubMember cm\r\n        WHERE cm.cmn = tm.cmn_fk\r\n    ) as \"player first name\",\r\n    (\r\n\tSELECT cm.last_name\r\n        FROM ClubMember cm\r\n        WHERE cm.cmn = tm.cmn_fk\r\n    ) as \"player last name\",\r\n    tm.role\r\nFROM TeamFormation tf\r\nJOIN Location l ON (l.id = tf.location_id_fk AND l.name = \"MYVC HQ\") -- User defined location name\r\nJOIN Session s ON (s.location_id_fk = l.id AND (s.event_date_time >= '2023-01-01 00:00:00' OR s.event_date_time >= '2026-01-01 00:00:00')) -- User defined time period (temp 3 year span, should be one week)\r\nJOIN TeamMember tm ON tm.team_formation_id_fk = tf.id\r\nORDER BY s.event_date_time ASC;"
                )
            },

            {
                10,
                new Tuple<string, FormattableString>
                (
                    "Get details of club members who are currently active and have been associated with at\r\nleast three different locations and are members for at most three years. Details include\r\nClub membership number, first name and last name. Results should be displayed sorted\r\nin ascending order by club membership number.",
                    $"SELECT\r\n\tCM.cmn AS 'MembershipNumber',\r\n    CM.first_name AS 'MemberFirstName',\r\n    CM.last_name AS 'MemberLastName'\r\nFROM ClubMember CM\r\nWHERE CM.is_active = 1 AND\r\n\t(\r\n\t\tSELECT COUNT(FML.location_id_fk)\r\n\t\tFROM FamilyMemberLocation FML\r\n        Where CM.family_member_id_fk = FML.family_member_id_fk\r\n        GROUP BY FML.family_member_id_fk\r\n\t) >= 3 AND\r\n    (\r\n\t\tSELECT COUNT(1) FROM (\r\n\t\t\t\t\t\t\t\tSELECT COUNT(1)\r\n\t\t\t\t\t\t\t\tFROM Payment\r\n\t\t\t\t\t\t\t\tWHERE cmn_fk = CM.cmn\r\n\t\t\t\t\t\t\t\tGROUP BY effectiveDate\r\n\t\t\t\t\t\t\t\tHAVING SUM(amount) >= 100.00\r\n                                ) AS FulfilledPayments\r\n    ) <= 3\r\nORDER BY CM.cmn;"
                )
            },

            {
                11,
                new Tuple<string, FormattableString>
                (
                    "For a given period of time, give a report on the teams’ formations for all the locations.\r\nFor each location, the report should include the location name, the total number of\r\ntraining sessions, the total number of players in the training sessions, the total number\r\nof game sessions, the total number of players in the game sessions. Results should only\r\ninclude locations that have at least two game sessions. Results should be displayed\r\nsorted in descending order by the total number of game sessions. For example, the\r\nperiod of time could be from Jan. 1\r\n\r\nst, 2025 to Mar. 31st, 2025.",
                    $"\r\n\r\nSELECT\r\n\tL.name AS 'LocationName',\r\n    SUM(\r\n\t\tCASE\r\n\t\t\tWHEN S.event_type = 'training' THEN 1\r\n            ELSE 0\r\n        END\r\n    ) AS 'TrainingSessions',\r\n\t(SELECT SUM(SessionPlayerCount)\r\n\t\tFROM (\r\n\t\t\t\tSELECT COUNT(DISTINCT TM.cmn_fk) AS SessionPlayerCount\r\n\t\t\t\tFROM Session s\r\n\t\t\t\tJOIN TeamSession TS ON s.id = TS.session_id_fk\r\n\t\t\t\tJOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id\r\n\t\t\t\tJOIN TeamMember TM ON TF.id = TM.team_formation_id_fk\r\n\t\t\t\tWHERE s.location_id_fk = L.id AND s.event_type = 'training'\r\n\t\t\t\tGROUP BY s.id\r\n\t\t\t) AS TotalPlayersAtLocation\r\n    ) AS TotalTrainingSessionPlayers,\r\n    SUM(\r\n\t\tCASE\r\n\t\t\tWHEN S.event_type = 'game' THEN 1\r\n            ELSE 0\r\n        END\r\n    ) AS 'GameSessions',\r\n    (SELECT SUM(SessionPlayerCount)\r\n\t\tFROM (\r\n\t\t\t\tSELECT COUNT(DISTINCT TM.cmn_fk) AS SessionPlayerCount\r\n\t\t\t\tFROM Session s\r\n\t\t\t\tJOIN TeamSession TS ON s.id = TS.session_id_fk\r\n\t\t\t\tJOIN TeamFormation TF ON TS.team_formation_id_fk = TF.id\r\n\t\t\t\tJOIN TeamMember TM ON TF.id = TM.team_formation_id_fk\r\n\t\t\t\tWHERE s.location_id_fk = L.id AND s.event_type = 'game'\r\n\t\t\t\tGROUP BY s.id\r\n\t\t\t) AS TotalPlayersAtLocation\r\n\t) AS TotalGameSessionPlayers\r\nFROM Location L\r\nJOIN Session S ON L.id = S.location_id_fk\r\nWHERE date_format(S.event_date_time, '%Y-%m-%d') >= '2025-01-01' AND date_format(S.event_date_time, '%Y-%m-%d') <= '2025-03-31'\r\nGROUP BY L.id, L.name\r\nHAVING GameSessions >= 2\r\nORDER BY GameSessions DESC;"
                )
            },

            {
                12,
                new Tuple<string, FormattableString>
                (
                    "Get a report on all active club members who have never been assigned to any formation\r\nteam session. The list should include the club member’s membership number, first\r\nname, last name, age, date of joining the club, phone number, email and current location\r\nname. The results should be displayed sorted in ascending order by location name then\r\nby club membership number.",
                    $"SELECT \r\n\tCM.cmn AS 'MembershipNumber',\r\n    CM.first_name AS 'MemberFirstName',\r\n    CM.last_name AS 'MemberLastName',\r\n    year(now()) - year(CM.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(CM.dob, '%m%d')) AS 'Age',\r\n    (\r\n\t\tSELECT MAX(paymentDate)\r\n\t\tFROM Payment\r\n        WHERE cmn_fk = CM.cmn\r\n        GROUP BY effectiveDate\r\n        ORDER BY effectiveDate\r\n        LIMIT 1\r\n    ) AS 'JoinDate',\r\n    CM.phone_number AS 'MemberPhone',\r\n    CM.email as 'MemberEmail',\r\n    (\r\n\t\tSELECT GROUP_CONCAT(L.name ORDER BY L.name SEPARATOR ', ')\r\n        FROM Location L\r\n        JOIN FamilyMemberLocation FML ON L.id = FML.location_id_fk\r\n        WHERE FML.family_member_id_fk = CM.family_member_id_fk\r\n        GROUP BY FML.family_member_id_fk\r\n    ) AS 'LocationNames'\r\nFROM ClubMember CM\r\nLEFT JOIN TeamMember TM ON CM.cmn = TM.cmn_fk\r\nWHERE CM.is_active = 1 AND (SELECT COUNT(TM.cmn_fk)) < 1\r\nORDER BY LocationNames, CM.cmn;"
                )
            },

            {
                13,
                new Tuple<string, FormattableString>
                (
                    "Get a report on all active club members who have only been assigned as outside hitter\r\nin all the formation team sessions they have been assigned to. They must be assigned\r\nto at least one formation session as an outside hitter. They should have never been\r\nassigned to any formation session with a role different than outside hitter. The list\r\nshould include the club member’s membership number, first name, last name, age,\r\nphone number, email and current location name. The results should be displayed sorted\r\nin ascending order by location name then by club membership number.",
                    $"SELECT cmn, first_name, last_name, Age, phone_number, email, LocationsList \r\nFROM\r\n(\r\n\tSELECT ClubMember.cmn, ClubMember.first_name, ClubMember.last_name, year(now()) - year(ClubMember.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(ClubMember.dob, '%m%d')) AS 'Age', phone_number, email, family_member_id_fk AS FamilyMemberId \r\n\tFROM\r\n\t(\r\n\t\tSELECT cmn_fk \r\n        FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT DISTINCT cmn_fk \r\n                FROM TeamMember WHERE ROLE = \"outside hitter\"\r\n\t\t\t) AS OutsideHitters \r\n\t\t\tWHERE cmn_fk NOT IN \r\n            (\r\n\t\t\t\tSELECT DISTINCT cmn_fk \r\n\t\t\t\tFROM TeamMember \r\n                WHERE ROLE != \"outside hitter\"\r\n\t\t\t)\r\n\t\t) AS OutsideHittersOnly\r\n\t\tJOIN (SELECT * FROM ClubMember WHERE is_active = 1) as ClubMember ON OutsideHittersOnly.cmn_fk = ClubMember.cmn\r\n) AS ClubMemberReport \r\nJOIN \r\n(\r\n\tSELECT FML.FamilyMemberId, GROUP_CONCAT(L.LocationName SEPARATOR ', ') AS LocationsList \r\n\tFROM \r\n\t\t(\r\n\t\t\t(\r\n\t\t\t\tSELECT location_id_fk AS LocationId, family_member_id_fk as FamilyMemberId \r\n\t\t\t\tFROM FamilyMemberLocation \r\n\t\t\t\tWHERE end_date IS NULL\r\n\t\t\t) AS FML\r\n\t\t\tJOIN\r\n\t\t\t(\r\n\t\t\t\tSELECT id, name AS LocationName \r\n\t\t\t\tFROM Location\r\n\t\t\t) AS L\r\n\t\t\tON L.id = FML.LocationId\r\n\t\t) GROUP BY FamilyMemberId\r\n) AS FamilyMemberLocationsList ON FamilyMemberLocationsList.FamilyMemberId = ClubMemberReport.FamilyMemberId\r\nORDER BY LocationsList ASC, cmn ASC;"
                )
            },

            {
                14,
                new Tuple<string, FormattableString>
                (
                    "Get a report on all active club members who have been assigned at least once to every\r\nrole throughout all the formation team game sessions. The club member must be\r\nassigned to at least one formation game session as an outside hitter, opposite, setter,\r\nmiddle blocker, libero, defensive specialist, and serving specialist. The list should\r\ninclude the club member’s membership number, first name, last name, age, phone\r\nnumber, email and current location name. The results should be displayed sorted in\r\nascending order by location name then by club membership number.",
                    $"SELECT cmn, first_name, last_name, year(now()) - year(ClubMemberReport.dob) - (DATE_FORMAT(now(), '%m%d') < DATE_FORMAT(ClubMemberReport.dob, '%m%d')) AS 'Age', phone_number, email, LocationsList\r\nFROM \r\n\t(\r\n\t\tSELECT * \r\n        FROM \r\n\t\t(\r\n\t\t\tSELECT cmn_fk \r\n            FROM\r\n\t\t\t(\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS oh WHERE role = \"outside hitter\"\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS s WHERE role = \"setter\"\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS mb WHERE role = \"middle blocker\"\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS l WHERE role = \"libero\"\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS ds WHERE role = \"defensive specialist\"\r\n\t\t\t\tUNION ALL\r\n\t\t\t\tSELECT DISTINCT cmn_fk FROM TeamMember AS ss WHERE role = \"serving specialist\"\r\n\t\t\t) AS UnionAll GROUP BY cmn_fk HAVING COUNT(*) = 6\r\n\t\t) AS AllRolesPlayers\r\n\tJOIN (SELECT * FROM ClubMember) AS CM ON CM.cmn = AllRolesPlayers.cmn_fk\r\n) AS ClubMemberReport\r\nJOIN \r\n(SELECT \r\n\tFML.FamilyMemberId, GROUP_CONCAT(L.LocationName SEPARATOR ', ') \r\n    AS LocationsList \r\nFROM \r\n\t(\r\n\t\t(\r\n\t\t\tSELECT location_id_fk AS LocationId, family_member_id_fk as FamilyMemberId FROM FamilyMemberLocation WHERE end_date IS NULL\r\n\t\t) AS FML\r\n\t\tJOIN\r\n\t\t(\r\n\t\t\tSELECT id, name AS LocationName FROM Location\r\n\t\t) AS L\r\n\t\tON L.id = FML.LocationId\r\n\t) \r\nGROUP BY FamilyMemberId) AS FamilyMemberReport ON FamilyMemberReport.FamilyMemberId = ClubMemberReport.cmn_fk;\r\n;"
                )
            },

            {
                15,
                new Tuple<string, FormattableString>
                (
                    "For the given location, get the list of all family members who have currently active\r\nclub members associated with them and are also captains for the same location.\r\nInformation includes first name, last name, and phone number of the family member.\r\nA family member is considered to be a captain if she/he is assigned as a captain to at\r\nleast one team formation session in the same location.",
                    $"SELECT CaptainReport.first_name, CaptainReport.last_name, CaptainReport.phone_number FROM\r\n(\r\n\tSELECT family_member_id_fk, first_name, last_name, phone_number, med_card_num, social_sec_num FROM\r\n\t(\r\n\t\tSELECT DISTINCT FMSL.family_member_id_fk \r\n\t\tFROM\r\n\t\t(\r\n\t\t\tSELECT * \r\n\t\t\tFROM FamilyMemberLocation \r\n\t\t\tWHERE location_id_fk = '1'\r\n\t\t) AS FMSL \r\n\t\tJOIN \r\n\t\t(\r\n\t\t\tSELECT * \r\n\t\t\tFROM ClubMember \r\n\t\t\tWHERE is_active = 1\r\n\t\t) AS CM ON FMSL.family_member_id_fk = CM.family_member_id_fk\r\n\t) AS FMWithLocationActiveCM\r\n\tJOIN\r\n\t(\r\n\t\tSELECT * \r\n\t\tFROM FamilyMember\r\n\t) AS FM ON FMWithLocationActiveCM.family_member_id_fk = FM.id\r\n) AS FamilyMemberReport\r\nJOIN\r\n#Captains of teams from that location\r\n(\r\n\tSELECT TFSL.id, TFSL.name, CM.first_name, CM.last_name, phone_number, med_card_num, social_sec_num \r\n\tFROM \r\n\t(\r\n\t\tSELECT * \r\n\t\tFROM TeamFormation \r\n\t\tWHERE location_id_fk = '1'\r\n\t) AS TFSL \r\n\tJOIN \r\n\t(\r\n\t\tSELECT * \r\n\t\tFROM ClubMember\r\n\t) AS CM ON CM.cmn = TFSL.captain_id_fk\r\n) AS CaptainReport ON FamilyMemberReport.first_name = CaptainReport.first_name \r\n\t\t\t\t\tAND FamilyMemberReport.last_name = CaptainReport.last_name \r\n                    AND FamilyMemberReport.med_card_num = CaptainReport.med_card_num\r\n                    AND FamilyMemberReport.social_sec_num = CaptainReport.social_sec_num;\r\n"
                )
            },

            {
                16,
                new Tuple<string, FormattableString>
                (
                    "Get a report of all active club members who have never lost a game in which they\r\nplayed. A club member is considered to win a game if she/he has been assigned to a\r\ngame session and is assigned to the team that has a score higher than the score of the\r\nother team. The club member must be assigned to at least one formation game session.\r\nThe list should include the club member’s membership number, first name, last name,\r\nage, phone number, email and current location name. The results should be displayed\r\nsorted in ascending order by location name then by club membership number.",
                    $"SELECT DISTINCT\r\n    cmn,\r\n    first_name,\r\n    last_name,\r\n    phone_number,\r\n    email\r\nfrom ClubMember CM\r\njoin TeamMember TM on CM.cmn = TM.cmn_fk\r\nWHERE has_only_won(cmn) < 1;" //TODO
                )
            },

            {
                17,
                new Tuple<string, FormattableString>
                (
                    "Get a report of all the personnel who were treasurer of the club at least once or is\r\ncurrently a treasurer of the club. The report should include the treasurer’s first name,\r\nlast name, start date as a treasurer and last date as treasurer. If last date as treasurer is\r\nnull means that the personnel is the current treasurer of the club. Results should be\r\ndisplayed sorted in ascending order by first name then by last name then by start date\r\nas a treasurer.",
                    $"SELECT P.first_name, P.last_name, PL.start_date AS 'start date', PL.end_date AS 'end date'\r\nFROM Personnel P\r\nJOIN PersonnelLocation PL ON P.id = PL.personnel_id_fk\r\nWHERE PL.role = 'treasurer'\r\nORDER BY P.first_name ASC, P.last_name ASC, 'start date' ASC;"
                )
            },

            {
                18,
                new Tuple<string, FormattableString>
                (
                    "Get a report on all club members who were deactivated by the system because they\r\nbecame over 18 years old. Results should include the club member’ first name, last\r\nname, telephone number, email address, deactivation date, last location name and last\r\nrole when deactivated. Results should be displayed sorted in ascending order by\r\nlocation name, then by role, then by first name then by last name.",
                    $"SELECT CM.first_name, CM.last_name, CM.phone_number, CM.email, DATE_ADD(dob, INTERVAL 18 YEAR), \r\n(\r\n\tSELECT GROUP_CONCAT(L.name ORDER BY L.name SEPARATOR ', ')\r\n    \tFROM Location L\r\n    \tJOIN FamilyMemberLocation FML ON L.id = FML.location_id_fk\r\n    \tWHERE FML.family_member_id_fk = CM.family_member_id_fk\r\n    \tGROUP BY FML.family_member_id_fk\r\n) AS 'last location name', \r\n(\r\n\tSELECT TM.role FROM TeamMember TM \r\n\t\tWHERE CM.cmn = TM.cmn_fk AND TM.assignment_date_time = \r\n        (\r\n        SELECT MAX(assignment_date_time)\r\n\t\t\tFROM TeamMember TM2\r\n            WHERE TM.cmn_fk = TM2.cmn_fk\r\n        )\r\n) AS 'latest role'\r\nFROM ClubMember CM\r\nORDER BY 'last location name' ASC, 'latest role' ASC, CM.first_name ASC, CM.last_name ASC;"
                )
            }
        };
    }
}
