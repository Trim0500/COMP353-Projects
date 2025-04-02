using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;
using MYVCApp.Models.ComplexQueryModels;
using System.Reflection;

namespace MYVCApp.Controllers
{
    public class ComplexQueriesController : Controller
    {
        private ApplicationDbContext _context;
        public ComplexQueriesController(ApplicationDbContext context_) 
        {
            _context = context_;
        }

        /// <summary>
        /// Base list for Complex Queries.
        /// </summary>
        /// <returns>List view for Complex queries.</returns>
        [HttpGet]
        [Route("ComplexQueries")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Executes a specific query number (8-17)
        /// </summary>
        /// <param name="number">The number of the complex query to execute.</param>
        /// <returns>A view containing the list of outputs with warnings/success/error messages.</returns>
        [HttpGet]
        [Route("ComplexQueries/{number}")]
        public async Task<IActionResult> Query(int number)
        {
            if (!ComplexQueryHelper.QUERIES.ContainsKey(number))
            {
                return NotFound();
            }

            Tuple<string, FormattableString> entry = ComplexQueryHelper.QUERIES.GetValueOrDefault(number);

            string queryDesc = entry.Item1;
            FormattableString sql = entry.Item2;

            IEnumerable<object> result = null;

            try
            {
                switch (number) 
                { 
                    case 7:
                        result = await _context.Database.SqlQuery<Q7Record>(sql).ToListAsync();
                        break;

                    case 8:
                        result = await _context.Database.SqlQuery<Q8Record>(sql).ToListAsync();
                        break;

                    case 9:
                        result = await _context.Database.SqlQuery<Q9Record>(sql).ToListAsync();
                        break;

                    case 10:
                        result = await _context.Database.SqlQuery<Q10Record>(sql).ToListAsync();
                        break;

                    case 11:
                        result = await _context.Database.SqlQuery<Q11Record>(sql).ToListAsync();
                        break;

                    case 12:
                        result = await _context.Database.SqlQuery<Q12Record>(sql).ToListAsync();
                        break;

                    case 13:
                        result = await _context.Database.SqlQuery<Q13Record>(sql).ToListAsync();
                        break;

                    case 14:
                        result = await _context.Database.SqlQuery<Q14Record>(sql).ToListAsync();
                        break;

                    case 15:
                        result = await _context.Database.SqlQuery<Q15Record>(sql).ToListAsync();
                        break;

                    case 16:
                        await _context.Database.ExecuteSqlAsync($"DROP FUNCTION IF EXISTS has_only_won;\r\n\r\nCREATE FUNCTION has_only_won(cmn INT) RETURNS INT\r\nREADS SQL DATA\r\nBEGIN\r\n    DECLARE result INT;\r\n\r\n    SELECT COUNT(*) INTO result\r\n    FROM (\r\n        SELECT team_formation_id_fk \r\n        FROM TeamMember \r\n        WHERE cmn_fk = cmn\r\n        AND team_formation_id_fk NOT IN (\r\n            SELECT TF.id \r\n            FROM TeamFormation TF\r\n            JOIN TeamSession TS ON TF.id = TS.team_formation_id_fk\r\n            WHERE TS.score = 100\r\n            GROUP BY TF.id\r\n        )\r\n    ) AS test;\r\n\r\n    RETURN result;\r\nEND;");
                        result = await _context.Database.SqlQuery<Q16Record>(sql).ToListAsync();
                        break;

                    case 17:
                        result = await _context.Database.SqlQuery<Q17Record>(sql).ToListAsync();
                        break;

                    case 18:
                        result = await _context.Database.SqlQuery<Q18Record>(sql).ToListAsync();
                        break;

                }

                TempData["Query"] = ComplexQueryHelper.QUERIES.GetValueOrDefault(number).Item1;
                TempData["QueryNumber"] = number;

                if (result.Count() != 0)
                {
                    TempData[TempDataHelper.Success] = String.Format("Success - {0} rows returned", result.Count());
                }
                else
                {
                    TempData[TempDataHelper.Warning] = String.Format("Query successful but no records were found.");
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error: " + ExceptionFormatter.GetFullMessage(ex);
                result = new List<string>();
            }

            return View("Result", result);
        }
    }
}
