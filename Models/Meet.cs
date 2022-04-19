using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoMeetUP.Models
{
    public class Meet
    {
        [Key]
        public int MeetId { get; set; }
        
        [Required(ErrorMessage = "Please enter a name for your meetup")]
        [Display(Name = "Meet Title")]
        [MinLength(3, ErrorMessage = "Meet title must be at least 3 characters long")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Please enter a description for your meetup")]
        [MinLength(10, ErrorMessage = "Meet title must be at least 10 characters long")]
        [Display(Name = "Meet Description", Prompt = "Type a description of your meet")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Date is needed")]
        [Display(Name = "Meet Date")]
        [DataType(DataType.DateTime, ErrorMessage = "This date is invalid")]
        [CustomDateRange]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Meet Duration")]
        
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int DurationTime { get; set; }
        
        [Required(ErrorMessage = "Choose a type")]
        [Display(Name="Duration Type")]
        [MinLength(3, ErrorMessage = "You need to choose a duration type")]
        public string DurationType { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Relationships
        // Many meets to one user
        public int UserId { get; set; }
        public User Creator { get; set; }
        
        // Guests
        public List<Guest> Guests { get; set; }
        
        
        // public DateTime GetStartTime()
        // {
        //     DateTime startTime = Date;
        //     switch(DurationType)
        //     {
        //         case "hours":
        //             startTime.AddHours(DurationTime);
        //             break;
        //         case "days":
        //             startTime.AddDays(DurationTime);
        //             break;
        //     }
        //     return startTime;
        // }
        public DateTime GetEndTime()
        {
            DateTime endTime = Date;
            Console.WriteLine(endTime);
                    Console.WriteLine(DurationType);
                    Console.WriteLine(DurationTime);
            switch(DurationType)
            {
                case "Hour":
                    endTime = endTime.AddHours((double)DurationTime);
            Console.WriteLine(endTime);
                    break;
                case "Day":
                    endTime= endTime.AddDays((double)DurationTime);
            Console.WriteLine(endTime);
                    break;
            }
            return endTime;
        }
    }
}