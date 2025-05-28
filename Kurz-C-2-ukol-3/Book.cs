using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurz_C_2_ukol_3
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate
        {
            get => publishedDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Datum vydání nemůže být v budoucnosti.");
                publishedDate = value;
            }
        }
        private DateTime publishedDate;

        // Vlastnost s validací
        private int pages;
        public int Pages
        {
            get => pages;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Počet stran musí být kladné číslo.");
                pages = value;
            }
        }

        public Book(string title, string author, DateTime publishedDate, int pages)
        {
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Pages = pages;
        }

        public override string ToString()
        {
            return $"Kniha: {Title}, autor: {Author}, vydáno: {PublishedDate:d.M.yyyy}, stran: {Pages}";
        }

    }
}
