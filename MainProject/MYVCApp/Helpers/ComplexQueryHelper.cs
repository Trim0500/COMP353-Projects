using MYVCApp.Models.ComplexQueryModels;

namespace MYVCApp.Helpers
{
    public class ComplexQueryHelper
    {
        public static readonly Dictionary<int, Tuple<string, FormattableString, Type>> QUERIES = new Dictionary<int, Tuple<string, FormattableString, Type>>()
        {
            {
                7,
                new Tuple<string, FormattableString, Type>
                (
                    "Get complete details for every location in the system. Details include address, city,\r\nprovince, postal-code, phone number, web address, type (Head, Branch), capacity,\r\ngeneral manager name, and the number of club members associated with that location.\r\nThe results should be displayed sorted in ascending order by Province, then by city.",
                    $"SELECT DISTINCT\r\n    l.id,\r\n    l.address,\r\n    l.city,\r\n    l.province,\r\n    l.postal_code AS \"postal code\",\r\n    l.website_url AS \"website\",\r\n    l.type,\r\n    l.capacity,\r\n    ph.phone_number AS \"phone number\",\r\n    ( -- Count of active members at this location\r\n\tSELECT COUNT(DISTINCT cm.cmn)\r\n\tFROM ClubMember cm\r\n\tJOIN FamilyMember fm ON fm.id = cm.family_member_id_fk\r\n        JOIN FamilyMemberLocation fml ON fm.id = fml.family_member_id_fk\r\n\tWHERE fml.location_id_fk = l.id\r\n\tAND ( -- Condition for members to be considered active\r\n\t\tSELECT SUM(p.amount) \r\n\t\tFROM Payment p \r\n\t\tWHERE p.cmn_fk = cm.cmn \r\n\t\tAND (YEAR(p.effectiveDate) = 2024 OR YEAR(p.effectiveDate) = 2025)\r\n\t\tGROUP BY p.cmn_fk\r\n\t) >= 100.00\r\n    ) AS \"number of active members\",\r\n    ( -- General manager first name\r\n\tSELECT pr.first_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"General Manager\"\r\n\tLIMIT 1\r\n\t) AS \"general manager first name\",\r\n    ( -- General manager last name\r\n\tSELECT pr.last_name \r\n\tFROM Personnel pr \r\n\tJOIN PersonnelLocation pl ON pr.id = pl.personnel_id_fk\r\n\tWHERE pl.location_id_fk = l.id\r\n\tAND pl.role = \"General Manager\" \r\n        LIMIT 1\r\n\t) AS \"general manager last name\"\r\nFROM Location l\r\nJOIN LocationPhone ph ON l.id = ph.location_id_fk\r\nORDER BY l.province ASC, l.city ASC;",
                    typeof(Q7Record)
                )
            }
        };
    }
}
