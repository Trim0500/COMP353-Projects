using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public class StatusViewModel
    {
        //for the scaffolder, ignore
        [Key]
        public int Id { get; set; }
        public string Database { get; set; } = string.Empty;
        public string DataSource { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsConnectionOkay {  get; set; } = false;

    }
}
