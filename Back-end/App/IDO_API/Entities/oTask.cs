namespace IDO_API.Entities
{
    public class oTask
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int statusId { get; set; }
        public int importanceId { get; set; }
        public int estimate { get; set; }
        public int position { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string Category { get; set; }
        public TaskStatus status { get; set; }
        public TaskImportance   importance { get; set; }
    }
}
