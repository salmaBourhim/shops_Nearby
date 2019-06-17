using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projectTest.Models
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required,
        StringLength(100, ErrorMessage = "The Password must be at least {2} characters long.", MinimumLength = 6),
        DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),
        Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match.")]
        public string ConfirmPassword { get; set; }
    }
}