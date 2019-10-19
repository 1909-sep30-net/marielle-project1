using System.ComponentModel;

namespace Project1WebApp.Models
{
    public class LocationViewModel
    {
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        public int LocID { get; set; }
    }
}