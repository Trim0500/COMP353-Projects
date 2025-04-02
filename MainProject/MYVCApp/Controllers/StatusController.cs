using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context_)
        {
            _context = context_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var conn = _context.Database.GetDbConnection();
            string database = conn.Database;
            string dataSource = conn.DataSource;

            StatusViewModel svm = new StatusViewModel();
            svm.DataSource = dataSource;
            svm.Database = database;
            svm.IsConnectionOkay = false;

            try
            {
                conn.Open();
                svm.IsConnectionOkay = true;
            }
            catch (Exception ex)
            {
                svm.ErrorMessage = ExceptionFormatter.GetFullMessage(ex);
            }

            return View(svm);
        }
    }
}
