using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace ConsoleApp
{
    static class BookList
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book("The Great Gatsby", "F. Scott Fitzgerald", 218, "9780743273565"),
            new Book("The Tin Drum", "Günter Grass", 592, "9780547339108"),
            new Book("Trump: The Art of the Deal", "Donald J. Trump", 400, "9780399594496")
        };
    }
}
