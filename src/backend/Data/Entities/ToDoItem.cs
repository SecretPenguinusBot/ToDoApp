

using System;

namespace backend.Data.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
        public virtual Project Project { get; set; }
    }
}