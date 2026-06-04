using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; } = Status.Pending;
    }
}
