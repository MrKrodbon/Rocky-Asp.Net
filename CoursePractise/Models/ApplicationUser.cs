using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursePractise.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }



    }
}
