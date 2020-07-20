using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigoErp.Service.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}
