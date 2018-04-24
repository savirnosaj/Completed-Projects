using System.ComponentModel.DataAnnotations;

namespace thursdaySportsday.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string firstName { get; set; }

        // // // // //

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name")]        
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string lastName { get; set; } 

        // // // // //

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string emailAddress { get; set; }

        // // // // //

        [Required]
        [Display(Name = "Password")]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        // // // // //

        [Compare("password", ErrorMessage = "Password and Confirmation must match")]
        [DataType(DataType.Password)]        
        [Display(Name = "Confirm Password")]        
        public string confirmPassword { get; set; }
    }
}