using System;

namespace Ado.net_example
{
    class Program
    {
        static void Main(string[] args)
        {
            var UI = new UserInterface(new DataProcessor(new DBDataSource()));
            UI.MainMenu();
        }
    }
}
