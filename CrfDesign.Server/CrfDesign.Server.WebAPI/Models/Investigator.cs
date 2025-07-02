using Microsoft.AspNetCore.Identity;

namespace CrfDesign.Server.WebAPI.Models
{
    public class Investigator : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorNumber { get; set; }
        public string QuickLookId { get; set; }
    }
}
