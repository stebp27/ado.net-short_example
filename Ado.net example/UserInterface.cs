using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ado.net_example
{
    class UserInterface
    {
        private DataProcessor processor;
        const string MENU_MESSAGE = "Welcome. Please press:\n" +
            "'a' + enter to display all authors\n" +
            "'b' + enter to display all books\n" +
            "'f' + enter to find an author by id\n" +
            "'t' + enter to find a book by id\n" +
            "'i' + enter to insert a new author\n" +
            "'n' + enter to insert a new book\n" +
            "'d' + enter to delete an author\n" +
            "'r' + enter to delete a book\n" +
            "'q' + enter to quit";

        public UserInterface(DataProcessor processor)
        {
            this.processor = processor;
        }

        public void MainMenu()
        {
            Console.WriteLine(MENU_MESSAGE);
            string input = ReadAnswer();

            switch (input[0])
            {
                case 'a':
                    ShowAuthors();
                    break;
                case 'b':
                    ShowBooks();
                    break;
                case 'f':
                    FindAuthorById();
                    break;
                case 't':
                    FindBookById();
                    break;
                case 'i':
                    InsertAuthor();
                    break;
                case 'n':
                    InsertBook();
                    break;
                case 'd':
                    DeleteAuthor();
                    break;
                case 'r':
                    DeleteBook();
                    break;
                case 'q':
                    return;
                default:
                    Console.WriteLine("Not valid input, please try again");
                    break;
            }
            MainMenu();
        }

        public void ShowAuthors()
        {
            IEnumerable<Author> authors = processor.AllAuthors();
            if (authors.Any())
            {
                foreach (var author in authors)
                {
                    author.ToString();
                }
            }
            else
            {
                Console.WriteLine("There are no authors.");
            }
        }

        public void ShowBooks()
        {
            IEnumerable<Book> books = processor.AllBooks();
            if (books.Any())
            {
                foreach (var book in books)
                {
                    book.ToString();
                }
            }
            else
            {
                Console.WriteLine("There are no books.");
            }
        }

        public void FindAuthorById()
        {
            Console.WriteLine("Please enter the Id of the author you want to search: ");
            int id = int.Parse(Console.ReadLine());
            Author author = processor.FindAuthorById(id);
            if(author != null)
            {
                author.ToString();
            }
            else
            {
                Console.WriteLine("Author not found.");
            }
        }

        public void FindBookById()
        {
            Console.WriteLine("Please enter the Id of the book you want to search: ");
            int id = int.Parse(Console.ReadLine());
            Book book = processor.FindBookById(id);
            if (book != null)
            {
                book.ToString();
            }
            else
            {
                Console.WriteLine("Book not found.\n");
            }
        }

        public void InsertAuthor()
        {
            Console.WriteLine("Please enter the new author's full name: ");
            string fullName = Console.ReadLine();
            Author author = processor.InsertAuthor(new Author(fullName));
            author.ToString();
        }

        public void InsertBook()
        {
            Console.WriteLine("Please enter the new book's title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Please enter the new book's author's id or press 'l' + enter to se author's list: ");
            string input = Console.ReadLine();
            if(input == "l")
            {
                ShowAuthors();
                Console.WriteLine("Please enter the new book's author's id: ");
                input = Console.ReadLine();
            }
            int authorId = int.Parse(input);
            Book book = processor.InsertBook(new Book(title, authorId));
            book.ToString();
        }
        public void DeleteAuthor()
        {
            Console.WriteLine("Please enter the Id of the author you want to delete or 'l' + enter to display all authors' list");
            string input = Console.ReadLine();
            if (input == "l")
            {
                ShowAuthors();
                Console.WriteLine("Please enter the Id of the author you want to delete: ");
                input = Console.ReadLine();
            }
            int id = int.Parse(input);
            bool deleted = processor.DeleteAuthor(id);
            if (deleted)
            {
                Console.WriteLine("Author deleted");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }

        public void DeleteBook()
        {
            Console.WriteLine("Please enter the Id of the book you want to delete or 'l' + enter to display all books' list");
            string input = Console.ReadLine();
            if (input == "l")
            {
                ShowBooks();
                Console.WriteLine("Please enter the Id of the book you want to delete: ");
                input = Console.ReadLine();
            }
            int id = int.Parse(input);
            bool deleted = processor.DeleteBook(id);
            if(deleted)
            {
                Console.WriteLine("Book deleted");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

        }

            private string ReadAnswer(string prompt = "")
        {
            Console.Write(prompt);
            return Console.ReadLine().ToLower();
        }
    }
}
