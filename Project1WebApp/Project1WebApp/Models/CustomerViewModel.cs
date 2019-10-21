using Project1.BusinessLogic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class CustomerViewModel
    {
        public int CustID { get; set; }

        [Required]
        [DisplayName("First Name"), RegularExpression(@"\s*[A-z]+\s*?([A-z]\s*)")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name"), RegularExpression(@"\s*[A-z]+\s*?([A-z]\s*)")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\s*\d+\s[A-z0-9]+\s*?(\s[A-z])*")]
        public string Street { get; set; }

        [Required]
        [RegularExpression(@"\s*[A-z]+\s*?(\s[A-z])*")]
        public string City { get; set; }

        public States State { get; set; }

        [Required]
        [RegularExpression(@"\d{5}")]
        public int Zipcode { get; set; }
    }
}