using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp
{
    public partial class MainWindow : Window
    {
        private readonly LibraryContext _context;

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("MainWindow создан");
            var app = (App)Application.Current;
            _context = app.ServiceProvider.GetRequiredService<LibraryContext>();

            LoadData();
            LoadFilters();
        }

        private void LoadData()
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (GenreFilter.SelectedValue is int genreId && genreId > 0)
                query = query.Where(b => b.GenreId == genreId);

            if (AuthorFilter.SelectedValue is int authorId && authorId > 0)
                query = query.Where(b => b.AuthorId == authorId);

            BooksGrid.ItemsSource = query.ToList();
        }

        private void LoadFilters()
        {
            GenreFilter.ItemsSource = _context.Genres.ToList();
            AuthorFilter.ItemsSource = _context.Authors.ToList();
        }

        private void GenreFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void AuthorFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            GenreFilter.SelectedIndex = -1;
            AuthorFilter.SelectedIndex = -1;
            LoadData();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookEditWindow(_context, null);
            if (window.ShowDialog() == true)
                LoadData();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is Book selected)
            {
                var window = new BookEditWindow(_context, selected);
                if (window.ShowDialog() == true)
                    LoadData();
            }
            else
                MessageBox.Show("Выберите книгу");
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is Book selected)
            {
                _context.Books.Remove(selected);
                _context.SaveChanges();
                LoadData();
            }
        }
    }
}