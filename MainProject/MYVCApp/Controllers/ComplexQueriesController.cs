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

        public IActionResult Index()
        {
            return View();
        }

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
