using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
        public string Title { get; set; } = string.Empty;

        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;

        [Required(ErrorMessage = "Год издания обязателен")]
        [Range(1500, 2100, ErrorMessage = "Год издания должен быть в диапазоне 1500–2100")]
        public int PublishYear { get; set; }

        [Required(ErrorMessage = "ISBN обязателен")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN должен быть длиной 10–13 символов")]
        public string ISBN { get; set; } = string.Empty;

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;

        [Required(ErrorMessage = "Количество на складе обязательно")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
        public int QuantityInStock { get; set; }
    }
}