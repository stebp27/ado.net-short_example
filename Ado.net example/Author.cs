using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_example
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public Author(string fullName)
        {
            FullName = fullName;
        }

        public Author(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {FullName}\n";
        }
    }
}
