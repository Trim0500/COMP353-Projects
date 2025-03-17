namespace MYVCApp.Helpers
{
    public class ExceptionFormatter
    {
        public static string GetFullMessage(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    if (ex.InnerException != null)
                    {
                        return ex.InnerException.Message;
                    }
                    else
                    {
                        return ex.Message;
                    }
                }
            }
            catch(Exception)
            {

            }
            return "-";
        }
    }
}
