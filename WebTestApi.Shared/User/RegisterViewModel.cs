﻿
using System.ComponentModel.DataAnnotations;

namespace WebTestApi.User
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
    }
}
