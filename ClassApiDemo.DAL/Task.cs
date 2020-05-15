using System;

namespace ClassApiDemo.DAL
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
