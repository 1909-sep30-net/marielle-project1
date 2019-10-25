using System.ComponentModel;

namespace Project1WebApp.Models
{
    public class SearchViewModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}