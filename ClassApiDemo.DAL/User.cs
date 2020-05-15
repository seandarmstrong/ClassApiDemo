using System;
using System.Collections.Generic;
using System.Text;

namespace ClassApiDemo.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
