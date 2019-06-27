using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ado.net_example
{
    class DataProcessor
    {
        private IDataSource source;

        public DataProcessor(IDataSource dataSource)
        {
            source = dataSource;
        }

        public IEnumerable<Author> AllAuthors()
        {
            return source.AllAuthors();
        }

        public IEnumerable<Book> AllBooks()
        {
            return source.AllBooks();
        }

        public Author FindAuthorById(int authorId)
        {
            return source.AllAuthors().SingleOrDefault(a => a.Id == authorId);
        }

        public Book FindBookById(int bookId)
        {
            return source.AllBooks().SingleOrDefault(b => b.Id == bookId);
        }

        public bool DeleteAuthor(int authorId)
        {
            return source.DeleteAuthor(authorId);
        }

        public bool DeleteBook(int bookId)
        {
            return source.DeleteBook(bookId);
        }

        public Author InsertAuthor(Author author)
        {
            return source.InsertAuthor(author);
        }

        public Book InsertBook(Book book)
        {
            return source.InsertBook(book);
        }

    }
}
