using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_example
{
    class Book
    {
        public Book(int id, string title, int authorId)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}; Title: {Title}; Author: {AuthorId}";
        }
    }
}
