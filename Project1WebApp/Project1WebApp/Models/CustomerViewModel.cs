using Project1.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1WebApp.Models
{
    public class CustomerViewModel
    {
        public int CustID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        
        public States State { get; set; }
        [Required]
        public int Zipcode { get; set; }
    }
}
