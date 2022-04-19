using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;


namespace DojoMeetUP.Models
{
    public class User
    {
        [Key] public int UserId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [MinLength(2, ErrorMessage = "*Needs to be at least 2 characters")]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }
        

        [Required(ErrorMessage = "*Required")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.Password)]
        // Regex for password requirements:
        // ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "*Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character")]
        [Display(Name = "Password", Prompt = "Password")]

        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "*Required")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        public string Confirm { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Relationships
        
        // User has many meets created
        public List<Meet> CreatedMeets { get; set; }
        
        // User has many meets joined
        public List<Guest> JoinedMeets { get; set; }

        // public bool WindowsOpen(Meet meet = null)
        // {
        //     if (this.JoinedMeets.Count == 0)
        //     {
        //         return true;
        //     }
        //
        //     DateTime startTime;
        //     DateTime endTime;
        //     
        //
        //
        // };
        
    }
}