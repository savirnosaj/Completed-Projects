using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace thursdaySportsday.Models
{
    public class Sport
    {
        // Checks Validations from Add.cshtml form. 
        // DB Table Columns 
        [Key]
        public int SportId { get; set; }
        // //
        
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "No special characters.")]
        [Display(Name = "Sport")]
        public string SportChoice { get; set; }
        // // // // //

        [Required]
        [Display(Name = "Day of Activity")]
        [DataType(DataType.Date)]
        public DateTime PlayDate { get; set; }

        // // // // //        

        public DateTime CreatedAt { get; set; }
        // // // // //
        public DateTime UpdatedAt { get; set; }
        // // // // //


        // DB Foreign Key(s) 
        public int UserId { get; set; }
        public User User { get; set; }


        // Creating new List of Guest. 
        public List<Guest> Guests { get; set; }

        // Creating new object of Guest list. 
        public Sport()
        {
            Guests = new List<Guest>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

    }
}