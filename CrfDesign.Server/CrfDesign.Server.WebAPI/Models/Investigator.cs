using Microsoft.AspNetCore.Identity;
using System;

namespace CrfDesign.Server.WebAPI.Models
{
    public class Investigator : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DoctorNumber { get; set; }
        public string QuickLookId { get; set; }


        // Uncommented Properties
        public DateTime DateOfBirth { get; set; }
        public int SiteId { get; set; } // FK to SiteRepository
        public int ExperimentId { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
