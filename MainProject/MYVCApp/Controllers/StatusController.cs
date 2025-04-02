using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYVCApp.Contexts;
using MYVCApp.Helpers;
using MYVCApp.Models;

namespace MYVCApp.Controllers
{
    /// <summary>
    /// Handles retrieving and displaying connection status/info.
    /// </summary>
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Instantiates controller with injected DbContext.
        /// </summary>
        /// <param name="context_">Injected DbContext.</param>
        public StatusController(ApplicationDbContext context_)
        {
            _context = context_;
        }

        /// <summary>
        /// Retrieves status view for the application.
        /// </summary>
        /// <returns>Status view containing connection info, database/hostname.</returns>
        [HttpGet]
        [Route("Status")]
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
