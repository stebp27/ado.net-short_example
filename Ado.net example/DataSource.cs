using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_example
{
    public interface IDataSource
    {
        IEnumerable<Book> AllBooks();
        IEnumerable<Author> AllAuthors();
        Book InsertBook(Book book);
        Author InsertAuthor(Author author);
        bool DeleteBook(int bookId);
        bool DeleteAuthor(int authorId);

    }
}
