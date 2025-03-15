using MYVCApp.Models;

namespace MYVCApp.Helpers
{
    public class TempDataHelper
    {
        public const string BASE = "TempData:";
        public static string Success
        {
            get
            {
                return BASE + "Success";
            }
        }

        public static string Warning
        {
            get
            {
                return BASE + "Warning";
            }
        }

        public static string Error
        {
            get
            {
                return BASE + "Error";
            }
        }
    }
}
