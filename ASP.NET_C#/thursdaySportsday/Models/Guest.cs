using System;
using System.ComponentModel.DataAnnotations;

namespace thursdaySportsday.Models
{
    public class Guest
    {
        // DB Table Columns 
        [Key]
        public int GuestId { get; set; }
        // // // //
        public DateTime CreatedAt { get; set; }
        // // // //
        public DateTime UpdatedAt { get; set; }


        // // // //
        // DB Foreign Key(s) 
        public int SportId { get; set; }
        public Sport Sport { get; set; }

        // // // //
        public int UserId { get; set; }
        public User User { get; set; }
    }
}