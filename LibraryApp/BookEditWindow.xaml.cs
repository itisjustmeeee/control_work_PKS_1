using System;
using System.Windows;
using System.Linq;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp
{
    public partial class BookEditWindow : Window
    {
        private readonly LibraryContext _context;
        private Book? _book;

        public BookEditWindow(LibraryContext context, Book? book)
        {
            InitializeComponent();
            _context = context;
            _book = book;

            cmbAuthor.ItemsSource = _context.Authors.ToList();
            cmbGenre.ItemsSource = _context.Genres.ToList();

            if (_book != null)
            {
                txtTitle.Text = _book.Title;
                cmbAuthor.SelectedValue = _book.AuthorId;
                txtYear.Text = _book.PublishYear.ToString();
                txtISBN.Text = _book.ISBN;
                cmbGenre.SelectedValue = _book.GenreId;
                txtStock.Text = _book.QuantityInStock.ToString();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_book == null)
                {
                    _book = new Book();
                    _context.Books.Add(_book);
                }

                _book.Title = txtTitle.Text;
                _book.AuthorId = (int)cmbAuthor.SelectedValue;
                _book.PublishYear = int.Parse(txtYear.Text);
                _book.ISBN = txtISBN.Text;
                _book.GenreId = (int)cmbGenre.SelectedValue;
                _book.QuantityInStock = int.Parse(txtStock.Text);

                _context.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}