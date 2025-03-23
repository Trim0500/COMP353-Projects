using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Models;

namespace MYVCApp.Helpers
{
    public class TeamFormationMemberAdditionHelper
    {
        /// <summary>
        /// Takes in a team member and verifies that it meets all the criteria to be added to the team corresponding to its TeamFormationIdFk
        /// </summary>
        /// <param name="teammember">The member we are adding to its team</param>
        /// <param name="_context">The database context we are using</param>
        /// <returns>A list of strings corresponding to any errors that were encountered. If count zero, operation was successful. Otherwise, an error occured.</returns>
        public static async Task< List<string>> Validate(Teammember teammember, ApplicationDbContext _context)
        {
            List<string> errors = new List<string>();

            try
            {
                //Id of the team we are assigning to
                int teamFormationId = teammember.TeamFormationIdFk;

                //Gets the team we are trying to add this member to
                Teamformation? teamToAssignTo = await _context.Teamformations.Include(t => t.Teammembers).FirstOrDefaultAsync(t => t.Id == teamFormationId);

                //If it is null, flag as invalid operation
                if (teamToAssignTo == null)
                {
                    errors.Add(String.Format("Team {0} does not exist. Operation cancelled.", teamFormationId));
                }
                else //If the team exists:
                {
                    //Get the first member of the team to check their gender
                    Teammember? teamMemberInTeam = teamToAssignTo.Teammembers.FirstOrDefault(t => t.TeamFormationIdFk == teamFormationId);

                    //If member is not null, then check their gender.
                    if (teamMemberInTeam != null)
                    {
                        //Get the corresponding club member entries for the one we are trying to add and the one already on the team.
                        Clubmember? firstClubMemberInTeam = await _context.Clubmembers.FirstOrDefaultAsync(c => c.Cmn == teamMemberInTeam.CmnFk);
                        Clubmember? correspondingClubMemberEntry = await _context.Clubmembers.FirstOrDefaultAsync(c => c.Cmn == teammember.CmnFk);

                        //If they don't exist, throw and terminate operation.
                        if (firstClubMemberInTeam != null && correspondingClubMemberEntry != null)
                        {
                            //Teams can only be one gender.
                            if (firstClubMemberInTeam.Gender != correspondingClubMemberEntry.Gender)
                            {
                                errors.Add("Team members can be all male or all female, but not both.");
                            }
                        }
                        else
                        {
                            errors.Add("Expected club member data was not found. Operation cancelled.");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errors.Add("An error occured during validation: " + ExceptionFormatter.GetFullMessage(ex));
            }

            return errors;
        }
    }
}
