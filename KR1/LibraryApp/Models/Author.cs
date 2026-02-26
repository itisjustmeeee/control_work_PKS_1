using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(100, ErrorMessage = "Имя не может превышать 100 символов")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия автора обязательна")]
        [StringLength(100, ErrorMessage = "Фамилия не может превышать 100 символов")]
        public string LastName { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "Страна не может превышать 100 символов")]
        public string? Country { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}