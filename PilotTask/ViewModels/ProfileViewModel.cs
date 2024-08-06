using PilotTask.Validations;
using System.ComponentModel.DataAnnotations;

namespace PilotTask.ViewModels
{
    public class ProfileViewModel
    {
        public int ProfileId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [PastDate(ErrorMessage = "Date of Birth must be in the past.")]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailId { get; set; }
    }
}
