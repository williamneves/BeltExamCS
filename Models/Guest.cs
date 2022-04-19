using System;
using System.ComponentModel.DataAnnotations;

namespace DojoMeetUP.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        
        // Joined User
        public int JoinedUserId { get; set; }
        public User JoinedUser { get; set; }
        
        // Meet Event
        public int EventId { get; set; }
        public Meet Event { get; set; }
        
        public DateTime CreatedAt { get; set; }

    }
}