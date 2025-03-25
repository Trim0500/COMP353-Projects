using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q8Record
    {
        [Column("location name")]
        public string? LocationName { get; set; }

        [Column("club member number")]
        public int? ClubMemberNumber { get; set; }

        [Column("club member first name")]
        public string? ClubMemberFirstName { get; set; }

        [Column("club member last name")]
        public string? ClubMemberLastName { get; set; }

        [Column("club member birthday")]
        public DateTime? ClubMemberBirthday { get; set; }

        [Column("club member social security number")]
        public string? ClubMemberSocialSecNum { get; set; }

        [Column("club member medical card number")]
        public string? ClubMemberMedCardNum { get; set; }

        [Column("club member phone number")]
        public string? ClubMemberPhoneNumber { get;set; }

        [Column("club member address")]
        public string? ClubMemberAddress { get; set; }

        [Column("club member city")]
        public string? ClubMemberCity { get; set; }

        [Column("club member postal code")]
        public string? ClubMemberPostalCode { get; set; }

        [Column("club member secondary relationship")]
        public string? ClubMemberSecondaryRelationship { get; set; }

        [Column("secondary family member first name")]
        public string? SecondaryFamilyMemberFirstName { get; set; }

        [Column("secondary family member last name")]
        public string? SecondaryFamilyMemberLastName { get; set; }

        [Column("secondary family member phone number")]
        public string? SecondaryFamilyMemberPhoneNumber { get; set; }

        [Column("secondary family member relationship to primary")]
        public string? SecondaryFamilyMemberRelationshipToPrimary { get; set; }
    }
}
