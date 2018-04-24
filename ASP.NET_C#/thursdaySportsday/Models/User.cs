using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace thursdaySportsday.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set ;}
        // // // // 
        public string FirstName { get; set; }
        // // // //
        public string LastName { get; set; }
        // // // // 
        public string EmailAddress { get; set; }
        // // // //
        public string Password { get; set; }
        // // // //
        public DateTime CreatedAt { get; set; }
        // // // //
        public DateTime UpdatedAt { get; set; }
        // // // //

        public List<Guest> Guests { get; set; }

        public User()
        {
            Guests = new List<Guest>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;            
        }
    }
}