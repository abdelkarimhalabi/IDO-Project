﻿namespace IDO_API.Entities
{
    public class oTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public int ImportanceId { get; set; }
        public int Estimate { get; set; }
        public int Position { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public TaskStatus Status { get; set; }
        public TaskImportance   Importance { get; set; }
    }
}
