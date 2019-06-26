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
            @"select * from dbo.Authors";

        public const string ALL_BOOKS_QUERY =
            @"select * from dbo.Books";

        public const string INSERT_AUTHOR_QUERY =
            @"insert into dbo.Authors output INSERTED.ID
            values (@FirstName, @LastName)";

        public const string INSERT_BOOK_QUERY =
            @"insert into dbo.Books output INSERTED.ID
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
                    var posFirstName = reader.GetOrdinal("FirstName");
                    var posLastName = reader.GetOrdinal("LastName");

                    var authors = new List<Author>();
                    while(reader.Read())
                    {
                        var author = new Author(reader.GetInt32(posId), reader.GetString(posFirstName), reader.GetString(posLastName));
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
                    command.Parameters.AddWithValue("@FirstName", author.FirstName);
                    command.Parameters.AddWithValue("@LastName", author.LastName);

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
                    command.Parameters.AddWithValue("@FirstName", book.Title);
                    command.Parameters.AddWithValue("@LastName", book.AuthorId);

                    int id = (int)command.ExecuteScalar();
                    book.Id = id;
                    return book;
                }
            }

        }
    }
}
