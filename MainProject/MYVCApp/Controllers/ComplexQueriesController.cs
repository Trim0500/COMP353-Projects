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
        private int MIN_INDEX = 7;
        private int MAX_INDEX = 21;
        

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

            FormattableString sql = ComplexQueryHelper.QUERIES.GetValueOrDefault(number).Item2;

            IEnumerable<object> result = null;

            try
            {
                result = await _context.Database.SqlQuery<Q7Record>(sql).ToListAsync();

                TempData["Query"] = ComplexQueryHelper.QUERIES.GetValueOrDefault(number).Item1;

                if (result.Count() != 0)
                {
                    TempData[TempDataHelper.Success] = String.Format("Success - {0} rows returned", result.Count());
                }
                else
                {
                    TempData[TempDataHelper.Warning] = String.Format("Success - No Results Found");
                }
            }
            catch(Exception ex)
            {
                TempData[TempDataHelper.Error] = "Error: " + ExceptionFormatter.GetFullMessage(ex);
            }

            return View("Result", result);
        }
    }
}
