using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Core.Areas.Dashboard.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "First_Name_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
       public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "Last_Name_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
        public string LastName { get; set; }
        [Required(ErrorMessageResourceName = "Email_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "Password_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}