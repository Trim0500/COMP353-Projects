using Microsoft.AspNetCore.Mvc;
using MYVCApp.Contexts;

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

        [HttpGet]
        [Route("ComplexQueries")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("ComplexQueries/{number}")]
        public async Task<IActionResult> Query(int number)
        {
            if (number > MAX_INDEX || number < MIN_INDEX)
            {
                return NotFound();
            }

            //var result = await _context.From

            throw new NotImplementedException();
        }
    }
}
