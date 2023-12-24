using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    
    public class LibraryModel
    {
        
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public string? Photo { get; set; } // Fotoğraf için özellik
        public string? Address { get; set; } // Kitap adresi için özellik
    }
}

