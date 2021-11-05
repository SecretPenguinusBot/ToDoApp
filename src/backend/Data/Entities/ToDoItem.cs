

using System;
using Newtonsoft.Json;

namespace backend.Data.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Completed { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }
    }
}