using System;

namespace Engineer.Domain.Models.Todo
{
    public class ToDoDTO
    {
        //public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public bool IsCompleted { get; set; }

        public string Task { get; set; }
    }
}