using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_example
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }

        public Book(string title, int authorId)
        {
            Title = title;
            AuthorId = authorId;
        }

        public Book(int id, string title, int authorId)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
        }

        public override string ToString()
        {
            return $"ID: {Id}; Title: {Title}; AuthorId: {AuthorId}\n";
        }
    }
}
