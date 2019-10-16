using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeModels
{
    public class Book
    {
        public int BId { get; set; }
        public string BName { get; set; }

        public List<Library> Libraries { get; set; }
        public List<User> Users { get; set; }

    }
}
