namespace LibraryManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AssignBookViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Book Title is required")]
        [Display(Name = "Book Title")]
        public string BookTitle { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }

        [NotMapped]
        public CustomerModel Customer { get; set; }

        [NotMapped]
        public LibraryModel Book { get; set; }
    


    /* public int Id { get; set; }

     [Display(Name = "Customer Name")]
     public string CustomerName
     {
         get { return this.Customer?.Name; }
     }

     [Display(Name = "Book Title")]
     public string BookTitle
     {
         get { return this.Book?.Title; }
     }

     [Display(Name = "Customer")]
     public int CustomerId { get; set; }
     public CustomerModel Customer { get; set; }

     [Display(Name = "Book")]
     public int BookId { get; set; }
     public LibraryModel Book { get; set; }

     // Diğer müşteri bilgileri...
     public List<CustomerModel> Customers { get; set; }
     public List<LibraryModel> Books { get; set; }*/
}

}
