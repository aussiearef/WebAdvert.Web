using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class ConfirmModel
    {
        [Required(ErrorMessage ="Email is required.")]
        [Display(Name="Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Code is requred.")]
        [Display(Name ="Code")]
        public string Code { get; set; }
    }
}
