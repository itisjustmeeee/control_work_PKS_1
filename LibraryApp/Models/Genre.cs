using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название жанра обязательно")]
        [StringLength(100, ErrorMessage = "Название жанра не может превышать 100 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не может превышать 500 символов")]
        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}