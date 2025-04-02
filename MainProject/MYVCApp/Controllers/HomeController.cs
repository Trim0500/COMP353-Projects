using Microsoft.AspNetCore.Mvc;
using MYVCApp.Models;
using System.Diagnostics;

namespace MYVCApp.Controllers
{
    /// <summary>
    /// Handles the home pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Instantiates controller with injected logger.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the main menu for the application.
        /// </summary>
        /// <returns>The main menu for the application including the buttons that take you to the various entities.</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Privacy policy.
        /// </summary>
        /// <returns>The view containing the privacy policy.</returns>
        [HttpGet]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Used when an error occurs.
        /// </summary>
        /// <returns>Error view with corresponding ErrorViewModel</returns>
        [HttpGet]
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}