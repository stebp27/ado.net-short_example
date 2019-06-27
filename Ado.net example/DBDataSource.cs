using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Ado.net_example
{
    class DBDataSource : IDataSource
    {
        public const string CONNECTION_STRING=
            @"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=AdoExampleDB;Integrated Security=True;";

        public const string ALL_AUTHORS_QUERY =
            @"select Id, FullName from dbo.Authors";

        public const string ALL_BOOKS_QUERY =
            @"select Id, Title, AuthorId from dbo.Books";

        public const string INSERT_AUTHOR_QUERY =
            @"insert into dbo.Authors (FullName) output INSERTED.Id
            values (@FullName)";

        public const string INSERT_BOOK_QUERY =
            @"insert into dbo.Books (Title,AuthorId) output INSERTED.Id
            values (@Title, @AuthorId)";

        public const string DELETE_AUTHOR_QUERY =
            @"delete from dbo.Authors
            where Id = @Id";

        public const string DELETE_BOOK_QUERY =
            @"delete from dbo.Books
            where Id = @Id";

        public IEnumerable<Author> AllAuthors()
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(ALL_AUTHORS_QUERY, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    var posId = reader.GetOrdinal("Id");
                    var posFullName = reader.GetOrdinal("FullName");

                    var authors = new List<Author>();
                    while(reader.Read())
                    {
                        var author = new Author(reader.GetInt32(posId), reader.GetString(posFullName));
                        authors.Add(author);
                    }
                    return authors;
                }
            }        
        }

        public IEnumerable<Book> AllBooks()
        {
            using(SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(ALL_BOOKS_QUERY, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    var posId = reader.GetOrdinal("Id");
                    var posTitle = reader.GetOrdinal("Title");
                    var posAuthorId = reader.GetOrdinal("AuthorId");

                    var books = new List<Book>();
                    while(reader.Read())
                    {
                        var book = new Book(reader.GetInt32(posId), reader.GetString(posTitle), reader.GetInt32(posAuthorId));
                        books.Add(book);
                    }
                    return books;
                }
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DELETE_AUTHOR_QUERY, connection))
                {
                    command.Parameters.AddWithValue("@Id", authorId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DELETE_BOOK_QUERY, connection))
                {
                    command.Parameters.AddWithValue("@Id", bookId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public Author InsertAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(INSERT_AUTHOR_QUERY, connection))
                {
                    command.Parameters.AddWithValue("@FullName", author.FullName);

                    int id = (int)command.ExecuteScalar();
                    author.Id = id;
                    return author;
                }
            }
        }

        public Book InsertBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(INSERT_BOOK_QUERY, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@AuthorId", book.AuthorId);

                    int id = (int)command.ExecuteScalar();
                    book.Id = id;
                    return book;
                }
            }

        }
    }
}
