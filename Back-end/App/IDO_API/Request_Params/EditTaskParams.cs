namespace IDO_API.Request_Params
{
    public class EditTaskParams
    {
        public int id { get; set; }
        public int statusId { get; set; }
        public int importanceId { get; set; }
        public int estimate { get; set; }
        public int position { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string Category { get; set; }
    }
}
