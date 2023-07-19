namespace IDO_API.Request_Params
{
    public class EditTaskParams
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int ImportanceId { get; set; }
        public int Estimate { get; set; }
        public int Position { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }
}
