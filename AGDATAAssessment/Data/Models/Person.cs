using System.ComponentModel.DataAnnotations;

namespace AGDATAAssessment.Data.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(250, ErrorMessage = "Name can not be more than 250 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required")]
        [MaxLength(250, ErrorMessage = "Address Line 1 can not be more than 250 characters")]
        public string AddressLine1 { get; set; }

        [MaxLength(250, ErrorMessage = "Address Line 1 can not be more than 250 characters")]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City can not be more than 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(50, ErrorMessage = "State can not be more than 50 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [MaxLength(10, ErrorMessage = "Zip can not be more than 10 characters")]
        public string Zip { get; set; }
    }
}
