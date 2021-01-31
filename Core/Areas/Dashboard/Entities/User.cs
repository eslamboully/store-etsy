using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Dashboard.Entities
{
    public class User
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
        [Required(ErrorMessageResourceName = "Phone_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
        public string Phone { get; set; }
        [Required(ErrorMessageResourceName = "Address_Required",ErrorMessageResourceType = typeof(SharedResource)),MaxLength(180)]
        public string Address { get; set; }
        [Range(1,3)]
        public Int16 Type { get; set; }

        public virtual List<Shipping> Shippings { get; set; }
    }
}