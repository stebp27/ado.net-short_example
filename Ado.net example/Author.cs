using System;
using System.Collections.Generic;
using System.Text;

namespace Ado.net_example
{
    class Author
    {
        public Author(int id, string firstName, string lastName)
        {
            Id = id;

            FirstName = firstName;

            LastName = lastName;

        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
