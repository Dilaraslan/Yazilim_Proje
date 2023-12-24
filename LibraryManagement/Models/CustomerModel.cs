namespace LibraryManagement.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


        // Diğer müşteri bilgileri...
    }

}
